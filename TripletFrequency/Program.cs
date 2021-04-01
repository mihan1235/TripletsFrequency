using System;
using System.Collections.Generic;
using System.IO;

namespace TripletFrequency
{
    class Program
    {
        static string help = "Usage: Programm.exe textfile";
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Wrong parametres");
                Console.WriteLine(help);
                return 0;
            }
            args[0].Trim();
            if ((args[0] == "help") || (args[0] == "Help")
                || (args[0] == "?"))
            {
                Console.WriteLine(help);
                return 0;
            }
            string FileName = args[0];

            SortedDictionary <string,int> hst = new SortedDictionary<string, int>();
            Lib1.TripletFind.FindTriplets(hst, FileName);
            if (hst.Count != 0)
            {
                foreach (var key in hst.Keys)
                {
                    Console.WriteLine($"{key}: {hst[key]}");
                }
            }
            return 0;
        }
    }
}
