using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSharp.Data;

namespace GSharp.Test {
    class Program {
        static void Main(string[] args)
        {
            DB.I.BeginEdit();
            for (int n = 0; n < 20;n++)
            {
                //DB.I["TEST/" + n.ToString()] = n.ToString();
            }
            DB.I.EndEdit();

            Console.WriteLine(DB.I.dbFile.FullName);

            foreach (var s in DB.I.Glob("TEST/*"))
            {
                Console.WriteLine(s.ToString());
            }
            Console.ReadKey();
        }
    }
    
}
