using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lib1
{
   
    public class TripletFind
    {

        static char[] def_exl_arr = { ' ', '-', '"', ':', ';', '?', 
            '!', '.', ',', '\u00BB','\'', '(', ')', '\u0027',
        '\u201C', '\u2026', '\u00AB' , '0', '1', '2', '3',
        '4', '5', '6', '7', '8', '9', '\u201E', '\u2013', '/',
        '@', '#', '$', '&','%', '}','{','[',']'};

        static Object lock_object = new Object();
        static string ReadTriplet(string word, int offset)
        {
            StringBuilder sb = new StringBuilder(); 
            int j = 0;
            for (int i = offset; i < word.Length; i++)
            {
                if (j == 3)
                {
                    break;
                }
                sb.Append(word[i]);
                j++;
            }
            if (j != 3) return null;
            return sb.ToString();
        }

        static void ReadTriplets(Dictionary<string, int> hst, string word, int offset)
        {
            
            for (int i = offset; i < word.Length; i += 3)
            {
                string tr = ReadTriplet(word, i);
                if (tr == null) break;
                lock (lock_object)
                {
                    if (hst.ContainsKey(tr))
                    {
                        hst[tr] = hst[tr] + 1;
                    }
                    else
                    {
                        hst.Add(tr, 0);
                    }
                }
                
            }
        }

        public static void FindTriplets(Dictionary<string, int> hst, 
            string line, char[] exlude = null)
        {
            if (line.Length < 3) return;
            string[] words;
            if (exlude == null)
            {
                words = line.Split(def_exl_arr, StringSplitOptions.None);
            }
            else
            {
                words = line.Split(exlude, StringSplitOptions.RemoveEmptyEntries);
            }


            //Parallel.ForEach(words,(string obj)=> {
            //    if (obj.Length > 2)
            //    {
            //        string word = obj.ToLower();
            //        ReadTriplets(hst, word, 0);
            //        ReadTriplets(hst, word, 1);
            //        ReadTriplets(hst, word, 2);
            //    }
            //});

           foreach( string obj in words){
               
                if (obj.Length > 2)
                {
                    string word = obj.ToLower();
                    ReadTriplets(hst, word, 0);
                    ReadTriplets(hst, word, 1);
                    ReadTriplets(hst, word, 2);
                }
            };
        }

        public enum Mode
        {
            First,
            Second,
            Third
        }
    }
}
