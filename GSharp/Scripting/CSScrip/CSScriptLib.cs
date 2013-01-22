#region Licence...
//-----------------------------------------------------------------------------
// Date:	10/11/04    Time: 3:00p
// Module:	CSScriptLib.cs
// Classes:	CSScript
//			AppInfo
//
// This module contains the definition of the CSScript class. Which implements 
// compiling C# script engine (CSExecutor). Can be used for hosting C# script engine
// from any CLR application
//
// Written by Oleg Shilo (oshilo@gmail.com)
// Copyright (c) 2004. All rights reserved.
//
// Redistribution and use of this code in source and binary forms, with or without 
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
// 2. Neither the name of an author nor the names of the contributors may be used 
//    to endorse or promote products derived from this software without specific 
//    prior written permission.
// 3. This code may be used in compiled form in any way you desire. This
//	  file may be redistributed unmodified by any means PROVIDING it is 
//    not sold for profit without the authors written consent, and 
//    providing that this notice and the authors name is included. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR 
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT 
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//	Caution: Bugs are expected!
//----------------------------------------------
#endregion

using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace GSharp.Scripting.CSScript
{
	public delegate void PrintDelegate(string msg);
	/// <summary>
	/// Class CSScript which is implements class library for CSExecutor.
	/// </summary>
	public class CSScript
	{
		public CSScript()
		{
			rethrow = false;
		}
		/// <summary>
		/// Force caught exceptions to be rethrown.
		/// </summary>
		static public bool Rethrow
		{
			get {return rethrow;}
			set {rethrow = value;}
		}
		/// <summary>
		/// Invokes CSExecutor (C# script engine)
		/// </summary>
        static public void Execute(GSharp.Scripting.CSScript.PrintDelegate print, string[] args)
		{
			csscript.AppInfo.appName = new FileInfo(Application.ExecutablePath).Name;
			CSExecutor exec = new CSExecutor();
			exec.Rethrow = Rethrow;
            exec.Execute(args, new PrintDelegate(print != null ? print : new GSharp.Scripting.CSScript.PrintDelegate(DefaultPrint)));
		}
		/// <summary>
		/// Invokes CSExecutor (C# script engine)
		/// </summary>
        public void Execute(GSharp.Scripting.CSScript.PrintDelegate print, string[] args, bool rethrow)
		{
			csscript.AppInfo.appName = new FileInfo(Application.ExecutablePath).Name;
			CSExecutor exec = new CSExecutor();
			exec.Rethrow = rethrow;
            exec.Execute(args, new PrintDelegate(print != null ? print : new GSharp.Scripting.CSScript.PrintDelegate(DefaultPrint)));
		}
		/// <summary>
		/// Compiles script into assembly with CSExecutor
		/// </summary>
		/// <param name="scriptFile">The name of script file to be compiled.</param>
		/// <param name="assemblyFile">The name of compiled assembly. If set to null a temnporary file name will be used.</param>
		/// <param name="debugBuild">true if debug information should be included in assembly; otherwise, false.</param>
		/// <returns></returns>
		static public string Compile(string scriptFile, string assemblyFile, bool debugBuild)
		{
			CSExecutor exec = new CSExecutor();
			exec.Rethrow = true;
			return exec.Compile(scriptFile, assemblyFile, debugBuild);
		}
		/// <summary>
		/// Compiles script into assembly with CSExecutor and loads it in current AppDomain
		/// </summary>
		/// <param name="scriptFile">The name of script file to be compiled.</param>
		/// <param name="assemblyFile">The name of compiled assembly. If set to null a temnporary file name will be used.</param>
		/// <param name="debugBuild">true if debug information should be included in assembly; otherwise, false.</param>
		/// <returns></returns>
		static public Assembly Load(string scriptFile, string assemblyFile, bool debugBuild)
		{
			CSExecutor exec = new CSExecutor();
			exec.Rethrow = true;
			string outputFile = exec.Compile(scriptFile, assemblyFile, debugBuild);

			AssemblyName asmName = AssemblyName.GetAssemblyName(outputFile);
			return AppDomain.CurrentDomain.Load(asmName);
		}
		/// <summary>
		/// Default implementation of displaying application messages.
		/// </summary>
		static void DefaultPrint(string msg)
		{
			//do nothing
		}
		static bool rethrow;
	}
}

namespace csscript
{
	delegate void PrintDelegate(string msg);
	/// <summary>
	/// Repository for application specific data
	/// </summary>
	class AppInfo
	{
		public static string appName = "CSScriptLibrary";
		public static bool appConsole = false;
		public static string appLogo
		{
			get { return "C# Script execution engine. Version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()+".\nCopyright (C) 2004 Oleg Shilo.\n";}
		}
		public static string appLogoShort
		{
			get { return "C# Script execution engine. Version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()+".\n";}
		}
		public static string appParams = "[/nl]:";
		public static string appParamsHelp = "nl	-	No logo mode: No banner will be shown at execution time.\n";
	}
}
