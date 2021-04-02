using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TripletFrequency
{
    class Program
    {
        struct Pair : IComparable
        {
            public string triplet;
            public int num;

            int IComparable.CompareTo(object obj)
            {
                if (obj is Pair)
                {
                    Pair p1 = (Pair)obj;
                    if (p1.num == num) return 0;
                    if (p1.num > num) return -1;
                    if (p1.num < num) return 1;
                }
                throw new ArgumentException();
            }
        }

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

            //SortedDictionary <string,int> hst = new SortedDictionary<string, int>();
            //Lib1.TripletFind.FindTriplets(hst, FileName);
            //if (hst.Count != 0)
            //{
            //    foreach (var key in hst.Keys)
            //    {
            //        Console.WriteLine($"{key}: {hst[key]}");
            //    }
            //}
            int time = Environment.TickCount;
            Dictionary<string, int> hst = new Dictionary<string, int>();
            try
            {
                using (StreamReader stream = File.OpenText(FileName))
                {
                    Object stream_lock = new object();
                    Task[] tasks = new Task[2];
                   for(int i =0; i < tasks.Length; i++)
                    {
                        tasks[i] = new Task(() =>
                        {
                            string line = "";
                            while (line != null)
                            {
                                lock (stream_lock)
                                {
                                    line = stream.ReadLineAsync().Result;
                                }
                                if (line == null) break;
                                Lib1.TripletFind.FindTriplets(hst, line);
                            }
                        });
                    }
                    foreach (var obj in tasks)
                    {
                        obj.Start();
                    }

                    Task.WaitAll(tasks);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (hst.Count != 0)
            {
                ArrayList arr = new ArrayList();
                foreach (var key in hst.Keys)
                {
                    Pair p = new Pair();
                    p.triplet = key;
                    p.num = hst[key];
                    arr.Add(p);
                }
                arr.Sort();
                time = Environment.TickCount - time;
                int j = 0;
                for (int i = arr.Count-1; i !=0; i--)
                {
                    if (j == 10) break;
                    Console.WriteLine($"{((Pair)arr[i]).triplet}: {((Pair)arr[i]).num}");
                    j++;
                }
                Console.WriteLine($"Time: {time * 0.001}");
               
                using (StreamWriter sw = File.CreateText("result.txt"))
                {
                    foreach (Pair p in arr)
                    {
                        sw.WriteLine($"{p.triplet}: {p.num}");
                    }                  
                }
            }

            return 0;
        }
    }
}
