using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.JScript;
using Microsoft.VisualBasic;
using System.Reflection;
using System.CodeDom.Compiler;
//using Microsoft.FSharp.Compiler.CodeDom;

namespace GSharp.Scripting
{
    public class ScriptEngine
    {
        public enum Languages { VBasic, CSharp, JScript, FSharp };
        public Languages Language;
        private static int count = 0;
        private static AppDomain domain = null;
        private object evaluator = null;
        private Type evaluatorType = null;
        private CodeDomProvider compiler;
        public CompilerParameters Parameters = new CompilerParameters();
        private CompilerResults results;
        Assembly assembly;
        private string source;
        string variables, variables1;
        string code;
        public string[] Messages = null;


        public ScriptEngine()
        {
            new ScriptEngine(Languages.CSharp);
        }

        public ScriptEngine(Languages language) {
            Language = language;
            code = "";
            variables = "";
            //domain = AppDomain.CurrentDomain;
            if (domain == null) {
                domain = AppDomain.CreateDomain("MyScriptEngine");
                //foreach (System.Reflection.AssemblyName an in System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                //    Assembly d = domain.Load(an);
                //    Console.WriteLine(an.ToString());
                //}
            }

            Parameters.GenerateInMemory = true;
            /*Parameters.ReferencedAssemblies.Add("System.Core.dll");
            Parameters.ReferencedAssemblies.Add("System.dll");
            Parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");*/

            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => a.Location);

            Parameters.ReferencedAssemblies.AddRange(assemblies.ToArray());
        }

        public void Unload()
        {
            if (domain != null)
            {
                AppDomain.Unload(domain);
                domain = null;
            }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public void AddVariable(string VariableName)
        {
            switch (Language)
            {
                case Languages.CSharp:
                    variables += "dynamic " + VariableName + ";\r\n";
                    variables += "public void Set" + VariableName + "(dynamic x) { this." + VariableName + " = x; }\r\n";
                    variables += "public dynamic Get" + VariableName + "() { return this." + VariableName + "; }\r\n";
                    break;
                case Languages.JScript:
                    variables += "var " + VariableName + " : double;\r\n";
                    variables += "public function Set" + VariableName + "(x) { " + VariableName + " = x; }\r\n";
                    variables += "public function Get" + VariableName + "() : String { return " + VariableName + "; }\r\n";
                    break;
                case Languages.FSharp:
                    variables += "    let mutable " + VariableName + " = 0.0\r\n";
                    variables1 += "    member x.Get" + VariableName + " = " + VariableName + "\r\n";
                    variables1 += "    member x.Set" + VariableName + " v = " + VariableName + " <- v\r\n";
                    break;
                default: // VBasic
                    variables += "Dim " + VariableName + " As Double\r\n";
                    variables += "Public Sub Set" + VariableName + "(AVal As Double)\r\n" + VariableName + " = AVal\r\nEnd Sub\r\n";
                    variables += "Public Function Get" + VariableName + " As Double\r\nReturn " + VariableName + "\r\nEnd Function\r\n";
                    break;
            }
        }

        public void SetVariable(string VariableName, dynamic Value)
        {
            object o = evaluatorType.InvokeMember(
                        "Set" + VariableName,
                        BindingFlags.InvokeMethod,
                        null,
                        evaluator,
                        new object[] { Value }
                     );
        }

        public double GetVariable(string VariableName)
        {
            object o = evaluatorType.InvokeMember(
                        "Get" + VariableName,
                        BindingFlags.InvokeMethod,
                        null,
                        evaluator,
                        new object[] { }
                     );
            string s = o.ToString();
            return double.Parse(s.ToString());
        }

        public bool Compile()
        {
            switch (Language)
            {
                case Languages.CSharp:
                    source = "namespace UserScript\r\n{\r\nusing System;\r\nusing System.Dynamic;\r\n" +
                        "public class RunScript" + count.ToString() + "\r\n{\r\n" +
                        variables + "\r\npublic Object Eval()\r\n{\r\ndynamic Result;\r\n" +
                        code + "\r\nreturn Result;\r\n}\r\n}\r\n}";
                    compiler = new CSharpCodeProvider();
                    break;
                case Languages.JScript:
                    source = "package UserScript\r\n{\r\n" + 
                        "class RunScript" + count.ToString() + "\r\n{\r\n" + 
                        variables + "\r\npublic function Eval() : Object\r\n{\r\nvar Result;\r\n" +
                        code + "\r\nreturn Result; \r\n}\r\n}\r\n}\r\n";
                    compiler = new JScriptCodeProvider();
                    break;
                case Languages.FSharp:
                    throw new Exception("FSharp is not supportet");
                    /*source = "#light\r\nmodule UserScript\r\nopen System\r\n" +
                        "type RunScript" + count.ToString() + "() =\r\n" +
                        "    let mutable Result = Double.NaN\r\n" +
                        variables + "\r\n" + variables1 +
                        "    member this.Eval() =\r\n" +
                        code + "\r\n        Result\r\n";
                    compiler = new FSharpCodeProvider();*/
                    break;
                default: // VBasic
                    source = "Imports System\r\nNamespace UserScript\r\nPublic Class RunScript" + count.ToString() + "\r\n" +
                        variables + "Public Function Eval() As Object\r\nDim Result As Object\r\n" +
                        code + "\r\nReturn Result\r\nEnd Function\r\nEnd Class\r\nEnd Namespace\r\n";
                    compiler = new VBCodeProvider();
                    break;
            }

            results = compiler.CompileAssemblyFromSource(this.Parameters, source);
            // Check for compile errors / warnings
            if (results.Errors.HasErrors || results.Errors.HasWarnings)
            {
                Messages = new string[results.Errors.Count];
                for (int i = 0; i < results.Errors.Count; i++)
                    Messages[i] = results.Errors[i].ToString();
                return false;
            }
            else
            {
                Messages = null;
                assembly = results.CompiledAssembly;
                Type[] tt = assembly.GetTypes();
                if (Language == Languages.FSharp)
                    evaluatorType = assembly.GetType("UserScript+RunScript" + count.ToString());
                else
                    evaluatorType = assembly.GetType("UserScript.RunScript" + count.ToString());
                evaluator = Activator.CreateInstance(evaluatorType);
                count++;
                return true;
            }
        }

        public object Evaluate()
        {
            object o = evaluatorType.InvokeMember(
                        "Eval",
                        BindingFlags.InvokeMethod,
                        null,
                        evaluator,
                        new object[] { }
                     );
            return o;
            /*string s = o.ToString();
            return double.Parse(s.ToString());*/
        }
    }
}
