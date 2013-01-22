/*
 * Please leave this Copyright notice in your code if you use it
 * Written by Decebal Mihailescu [http://www.codeproject.com/script/articles/list_articles.asp?userid=634640]
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;
using System.Security.Principal;
using System.ComponentModel;

namespace GSharp.Sys.Windows.User {
    public class ImpersonateInteractiveUser : IDisposable {

        IntPtr _userTokenHandle = IntPtr.Zero;


        WindowsImpersonationContext _impersonatedUser;
        //IntPtr _hpiu = IntPtr.Zero;
        bool _bimpersonate;
        IntPtr _hSaveWinSta;
        IntPtr _hSaveDesktop;
        IntPtr _hWinSta;
        IntPtr _hDesktop;

        public IntPtr UserTokenHandle {
            [DebuggerStepThrough]
            get { return _userTokenHandle; }

        }
        public IntPtr HDesktop {
            [DebuggerStepThrough]
            get { return _hDesktop; }
        }

        public ImpersonateInteractiveUser(IntPtr hWnd, bool bimpersonate) {

            if (hWnd == IntPtr.Zero || !Win32API.IsWindow(hWnd) || !Win32API.IsWindowVisible(hWnd)) {
                throw new ApplicationException(string.Format("{0} is an Invalid window", hWnd));
            }
            _bimpersonate = bimpersonate;
            int procId;
            Win32API.GetWindowThreadProcessId(hWnd, out procId);
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcessById((int)procId);
            ImpersonateUsingProcess(proc);
        }

        public ImpersonateInteractiveUser(System.Diagnostics.Process proc, bool bimpersonate) {
            _bimpersonate = bimpersonate;
            ImpersonateUsingProcess(proc);
        }

        private void ImpersonateUsingProcess(System.Diagnostics.Process proc) {
            IntPtr hToken = IntPtr.Zero;

            Win32API.RevertToSelf();

            if (Win32API.OpenProcessToken(proc.Handle, TokenPrivilege.TOKEN_ALL_ACCESS, ref hToken) != 0) {

                try {

                    SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                    sa.Length = Marshal.SizeOf(sa);
                    bool result = Win32API.DuplicateTokenEx(hToken, Win32API.GENERIC_ALL_ACCESS, ref sa,
                    (int)SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, (int)TOKEN_TYPE.TokenPrimary, ref _userTokenHandle);
                    if (IntPtr.Zero == _userTokenHandle) {

                        Win32Exception ex = new Win32Exception(Marshal.GetLastWin32Error());
                        throw new ApplicationException(string.Format("Can't duplicate the token for {0}:\n{1}", proc.ProcessName, ex.Message), ex);
                    }


                    //EventLog.WriteEntry("Screen Monitor", string.Format("Before impersonation: owner = {0} Windows ID Name = {1} token = {2}", WindowsIdentity.GetCurrent().Owner, WindowsIdentity.GetCurrent().Name, WindowsIdentity.GetCurrent().Token), EventLogEntryType.SuccessAudit, 1, 1);

                    if (!ImpersonateDesktop()) {
                        Win32Exception ex = new Win32Exception(Marshal.GetLastWin32Error());
                        throw new ApplicationException(ex.Message, ex);
                    }

                } finally {
                    Win32API.CloseHandle(hToken);
                }
            } else {
                string s = String.Format("OpenProcess Failed {0}, privilege not held", Marshal.GetLastWin32Error());
                throw new Exception(s);
            }
        }

        bool ImpersonateDesktop() {

            _hSaveWinSta = Win32API.GetProcessWindowStation();
            if (_hSaveWinSta == IntPtr.Zero)
                return false;
            _hSaveDesktop = Win32API.GetThreadDesktop(Win32API.GetCurrentThreadId());
            if (_hSaveDesktop == IntPtr.Zero)
                return false;
            if (_bimpersonate) {
                WindowsIdentity newId = new WindowsIdentity(_userTokenHandle);
                _impersonatedUser = newId.Impersonate();

            }

            _hWinSta = Win32API.OpenWindowStation("WinSta0", false, Win32API.MAXIMUM_ALLOWED);
            if (_hWinSta == IntPtr.Zero)
                return false;
            if (!Win32API.SetProcessWindowStation(_hWinSta))
                return false;
            _hDesktop = Win32API.OpenDesktop("Default", 0, true, Win32API.MAXIMUM_ALLOWED);

            if (_hDesktop == IntPtr.Zero) {
                Win32API.SetProcessWindowStation(_hSaveWinSta);
                Win32API.CloseWindowStation(_hWinSta);
                return false;
            }
            if (!Win32API.SetThreadDesktop(_hDesktop))
                return false;
            return true;
        }

        public int CreateProcessAsUser(string app, string cmd) {
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            try {
                SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                sa.Length = Marshal.SizeOf(sa);
                STARTUPINFO si = new STARTUPINFO();
                si.cb = Marshal.SizeOf(si);
                si.lpDesktop = String.Empty;
                if (app != null && app.Length == 0)
                    app = null;
                if (cmd != null && cmd.Length == 0)
                    cmd = null;
                if (!Win32API.CreateProcessAsUser(
                _userTokenHandle,
                app,
                cmd,
                ref sa, ref sa,
                false, 0, IntPtr.Zero,
                @"C:\", ref si, ref pi
                )) {
                    int error = Marshal.GetLastWin32Error();
                    Win32Exception ex = new Win32Exception(error);
                    string message = String.Format("CreateProcessAsUser Error: {0}", ex.Message);
                    throw new ApplicationException(message, ex);
                }
            } catch (Exception ex) {
                EventLog.WriteEntry("Screen Monitor", ex.Message, EventLogEntryType.Error, 1, 1);
                throw;
            } finally {
                if (pi.hProcess != IntPtr.Zero)
                    Win32API.CloseHandle(pi.hProcess);
                if (pi.hThread != IntPtr.Zero)
                    Win32API.CloseHandle(pi.hThread);
            }
            return pi.dwProcessID;
        }
        bool UndoDesktop() {
            if (_impersonatedUser != null) {
                _impersonatedUser.Undo();
                _impersonatedUser.Dispose();
            }
            if (_hSaveWinSta != IntPtr.Zero)
                Win32API.SetProcessWindowStation(_hSaveWinSta);
            if (_hSaveDesktop != IntPtr.Zero)
                Win32API.SetThreadDesktop(_hSaveDesktop);
            if (_hWinSta != IntPtr.Zero)
                Win32API.CloseWindowStation(_hWinSta);
            if (_hDesktop != IntPtr.Zero)
                Win32API.CloseDesktop(_hDesktop);
            return true;
        }
        #region IDisposable Members
        public void Dispose() {
            //if (!Win32API.UnloadUserProfile(WindowsIdentity.GetCurrent().Token, _hpiu))
            // throw new Win32Exception(Marshal.GetLastWin32Error());
            //Marshal.FreeHGlobal(_hpiu);
            UndoDesktop();


            if (_userTokenHandle != IntPtr.Zero)
                Win32API.CloseHandle(_userTokenHandle);
            _userTokenHandle = IntPtr.Zero;
            _impersonatedUser = null;
        }
        #endregion
    }

}
