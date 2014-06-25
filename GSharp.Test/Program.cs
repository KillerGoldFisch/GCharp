using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSharp.Data;

using GSharp.Extensions.StringEx;
using GSharp.Scripting;

namespace GSharp.Test {
    class Program {
        static void Main(string[] args)
        {
            testScriptEngine();

            Console.ReadKey();
        }

        private static void testDB() {
            DB.I.BeginEdit();
            for (int n = 0; n < 20; n++) {
                DB.I["TEST/" + n.ToString()] = n.ToString().Encrypt();
            }
            DB.I.EndEdit();

            Console.WriteLine(DB.I.dbFile.FullName);

            foreach (var s in DB.I.GlobPair("TEST/*")) {
                Console.WriteLine(s.Key.ToString() + " " + s.Value.ToString().Decrypt());
            }

            DB.I["CRYPTTEST"] = "Test".Encrypt();
            Console.WriteLine(((string)DB.I["CRYPTTEST"]).Decrypt());
            
        }

        private static void testScriptEngine() {
            ScriptEngine engine = new ScriptEngine(ScriptEngine.Languages.CSharp);
            engine.Code = "Result = new ExpandoObject(); Result.Dude=\"HI\"+x.ToString();";
            //engine.Code = "Result = 4;";
            engine.AddVariable("x");

            if (engine.Compile()) {
                engine.SetVariable("x", "Kevin");
                dynamic d = engine.Evaluate();
                Console.WriteLine(d.Dude);
            } else {
                foreach (string errmsg in engine.Messages) {
                    Console.WriteLine("|-------------------------------------------|");
                    Console.WriteLine(errmsg);
                    Console.WriteLine("|-------------------------------------------|");
                }
            }
        }
    }
    
}
