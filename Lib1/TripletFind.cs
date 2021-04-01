using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lib1
{
    public class TripletFind
    {
        static char[] def_exl_arr = { ' ', '-', '"', ':', ';' };

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

        static void FindTripletsWord(SortedDictionary<string,int> hst, string word)
        {
            int offset = 0;
            for (int i = offset; i < word.Length; i += 3)
            {
                string tr = ReadTriplet(word, i);
                if (tr == null) break;
                if (hst.ContainsKey(tr))
                {
                    hst[tr] = hst[tr] + 1;
                }
                else
                {
                    hst.Add(tr, 0);
                } 
            }

            offset = 1;
            for (int i = offset; i < word.Length; i += 3)
            {
                string tr = ReadTriplet(word, i);
                if (tr == null) break;
                if (hst.ContainsKey(tr))
                {
                    hst[tr] = hst[tr] + 1;
                }
                else
                {
                    hst.Add(tr, 0);
                }
            }

            offset = 2;
            for (int i = offset; i < word.Length; i += 3)
            {
                string tr = ReadTriplet(word, i);
                if (tr == null) break;
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
        public static void FindTriplets(SortedDictionary<string, int> hst, 
            string line, char[] exlude = null)
        {
            if (line.Length < 3) return;
            string[] words;
            if (exlude == null)
            {
                words = line.Split(def_exl_arr);
            }
            else
            {
                words = line.Split(exlude);
            }
            foreach(var word in words)
            {
                if (word.Length > 2)
                {
                    FindTripletsWord(hst, word);
                }
            }
        }

        public enum Mode
        {
            First,
            Second,
            Third
        }
    }
}
