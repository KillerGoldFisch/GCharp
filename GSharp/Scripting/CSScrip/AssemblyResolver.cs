#region Licence...
//-----------------------------------------------------------------------------
// Date:	17/10/04    Time: 2:33p 
// Module:	AssemblyResolver.cs
// Classes:	AssemblyResolver
//
// This module contains the definition of the AssemblyResolver class. Which implements 
// some mothods for simplified Assembly navigation
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
using System.Reflection;
using System.Collections;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace GSarp.Scripting.CSScript
{
	/// <summary>
	/// Class for resolving assembly name to assembly file
	/// </summary>
	public class AssemblyResolver
	{
		#region Class public data...
		/// <summary>
		/// File to be excluded from assembly search
		/// </summary>
		static public string ignoreFileName = "";
		#endregion
		#region Class public methods...
		/// <summary>
		/// Resolves assembly name to assembly file
		/// </summary>
		/// <param name="assemblyName">The name of assembly</param>
		/// <param name="workingDir">The name of directory where local assemblies are expected to be</param>
		/// <returns></returns>
		static public Assembly ResolveAssembly(string assemblyName, string workingDir)
		{
			//try file with name AssemblyDisplayName + .dll 
			string[] asmFileNameTokens = assemblyName.Split(", ".ToCharArray(), 5);
			
			string asmFileName = Path.Combine(workingDir, asmFileNameTokens[0])+ ".dll";
			if (ignoreFileName != Path.GetFileName(asmFileName) && File.Exists(asmFileName))
			{
				try
				{
					AssemblyName asmName = AssemblyName.GetAssemblyName(asmFileName);
					if (asmName != null && asmName.FullName == assemblyName)
					{
						return Assembly.LoadFrom(asmFileName);
					}
				}
				catch{}
			}

			//try all dll files (in script folder) which contain namespace as a part of file name
			ArrayList asm_ALIKE_Files = new ArrayList(Directory.GetFileSystemEntries(workingDir, string.Format("*{0}*.dll", asmFileNameTokens[0])));
			foreach(string asmFile in asm_ALIKE_Files)	
			{
				try
				{
					if (ignoreFileName != Path.GetFileName(asmFile))
					{
						AssemblyName asmName = AssemblyName.GetAssemblyName(asmFile);
						if (asmName != null && asmName.FullName == assemblyName)
						{
							return Assembly.LoadFrom(asmFile);
						}
					}
				}
				catch{}
			}

			//try all the rest of dll files in script folder
			string[] asmFiles = Directory.GetFileSystemEntries(workingDir, "*.dll");
			foreach(string asmFile in asmFiles)	
			{
				if (asm_ALIKE_Files.Contains(asmFile))
					continue;
				try
				{
					if (ignoreFileName != Path.GetFileName(asmFile))
					{
						AssemblyName asmName = AssemblyName.GetAssemblyName(asmFile);
						if (asmName != null && asmName.FullName == assemblyName)
						{
							return Assembly.LoadFrom(asmFile);
						}
					}
				}
				catch{}
			}
			return null;
		}
		/// <summary>
		/// Resolves namespace into array of assembly locations (local and GAC ones).
		/// </summary>
		static public string[] FindAssembly(string nmSpace, string workingDir)
		{
			ArrayList retval = new ArrayList();
			string[] asmLocations = FindLocalAssembly(nmSpace, workingDir);
		
			if (asmLocations.Length != 0)	
			{
				foreach(string asmLocation in asmLocations)	//local assemblies
				{
					retval.Add(asmLocation);
				}
			}
			else
			{	
				string[] asmGACLocations = FindGlobalAssembly(nmSpace); //global assemblies
				foreach(string asmGACLocation in asmGACLocations)
				{
					retval.Add(asmGACLocation);
				}
			}
			return (string[])retval.ToArray(typeof(string));
		}
		/// <summary>
		/// Resolves namespace into array of local assembly locations.
		/// (Currently it returns only one assembly location but in future 
		/// it can be extended to collect all assemblies with the same namespace)
		/// </summary>
		static public string[] FindLocalAssembly(string refNamespace, string workingDir) 
		{
			ArrayList retval = new ArrayList();

			//try to predict assembly file name on the base of namespace
			string asesemblyLocation = String.Format("{0}\\{1}.dll", workingDir, refNamespace );
			
			if(ignoreFileName != Path.GetFileName(asesemblyLocation) && File.Exists(asesemblyLocation))
			{
				retval.Add(asesemblyLocation);
				return (string[])retval.ToArray(typeof(string));
			} 
						
			//try all dll files (in script folder) which contain namespace as a part of file name
			string tmp = string.Format("*{0}*.dll", refNamespace);
			ArrayList asm_ALIKE_Files = new ArrayList(Directory.GetFileSystemEntries(workingDir, string.Format("*{0}*.dll", refNamespace)));
			foreach(string asmFile in asm_ALIKE_Files)	
			{
				if (ignoreFileName != Path.GetFileName(asmFile) && IsNamespaceDefinedInAssembly(asmFile, refNamespace))
				{
					retval.Add(asmFile);
					return (string[])retval.ToArray(typeof(string));
				}
			}

			//try all the rest of dll files in script folder
			string[] asmFiles = Directory.GetFileSystemEntries(workingDir, "*.dll");
			foreach(string asmFile in asmFiles)	
			{
				if (asm_ALIKE_Files.Contains(asmFile))
					continue;

				if (ignoreFileName != Path.GetFileName(asmFile) && IsNamespaceDefinedInAssembly(asmFile, refNamespace))
				{
					retval.Add(asmFile);
					return (string[])retval.ToArray(typeof(string));
				}
			}
			return (string[])retval.ToArray(typeof(string));
		}

		/// <summary>
		/// Resolves namespace into array of global assembly (GAC) locations.
		/// </summary>
		static public string[] FindGlobalAssembly(String namespaceStr) 
		{
			ArrayList retval = new ArrayList();
			AssemblyEnum asmEnum = new AssemblyEnum(namespaceStr);
			String asmName;
			while ((asmName = asmEnum.GetNextAssembly()) != null)
			{
				string asmLocation = AssemblyCache.QueryAssemblyInfo(asmName);
				retval.Add(asmLocation);
			}
			return (string[])retval.ToArray(typeof(string));
		}
		#endregion
		/// <summary>
		/// Search for namespace into local assembly file.
		/// </summary>
		static private bool IsNamespaceDefinedInAssembly(string asmFileName, string namespaceStr) 
		{
			if (File.Exists(asmFileName))
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(asmFileName);
					if (assembly != null)	
					{
						foreach (Module m in assembly.GetModules())
						{
							foreach (Type t in m.GetTypes())
							{
								if (namespaceStr == t.Namespace)
								{
									return true;
								}
							}
						}	
					}
				}
				catch {}
			}
			return false;
		}
	}
}
