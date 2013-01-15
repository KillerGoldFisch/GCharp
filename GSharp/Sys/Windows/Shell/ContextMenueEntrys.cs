using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using Microsoft.Win32;


namespace GSharp.Sys.Windows.Shell {
    [RegistryPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]
    class ContextMenueEntrys {
        private const string MenuName = "Folder\\shell\\NewMenuOption";
        public const string Command = "Folder\\shell\\NewMenuOption\\command";

        private void CheckSecurity() {

            //check registry permissions
            RegistryPermission regPerm;
            regPerm = new RegistryPermission(RegistryPermissionAccess.Write, "HKEY_CLASSES_ROOT\\" + MenuName);
            regPerm.AddPathList(RegistryPermissionAccess.Write, "HKEY_CLASSES_ROOT\\" + Command);
            regPerm.Demand();

        }

        public static void AddMenu(string Path, string Name) {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            Exception extmp = null;
            try {
                regmenu = Registry.ClassesRoot.CreateSubKey(MenuName);
                if (regmenu != null)
                    regmenu.SetValue("", Name);
                regcmd = Registry.ClassesRoot.CreateSubKey(Command);
                if (regcmd != null)
                    regcmd.SetValue("", Path + " \"%1\"");

            } catch (Exception ex) {
                extmp = ex;
            } finally {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
            if (extmp != null)
                throw extmp;
        }

        public static void RemoveMenu() {
            RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
            if (reg != null) {
                reg.Close();
                Registry.ClassesRoot.DeleteSubKey(Command);
            }
            reg = Registry.ClassesRoot.OpenSubKey(MenuName);
            if (reg != null) {
                reg.Close();
                Registry.ClassesRoot.DeleteSubKey(MenuName);
            }
        }
    }
}
