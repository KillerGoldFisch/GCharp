using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSharp.Data;

using GSharp.Extensions.StringEx;

namespace GSharp.Test {
    class Program {
        static void Main(string[] args)
        {
            DB.I.BeginEdit();
            for (int n = 0; n < 20;n++)
            {
                DB.I["TEST/" + n.ToString()] = n.ToString().Encrypt();
            }
            DB.I.EndEdit();

            Console.WriteLine(DB.I.dbFile.FullName);

            foreach (var s in DB.I.GlobPair("TEST/*"))
            {
                Console.WriteLine(s.Key.ToString() +" " + s.Value.ToString().Decrypt());
            }

            DB.I["CRYPTTEST"] = "Test".Encrypt();
            Console.WriteLine(((string)DB.I["CRYPTTEST"]).Decrypt());
            Console.ReadKey();
        }
    }
    
}
