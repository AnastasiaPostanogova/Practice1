using System;
using System.Collections.Generic;
using System.IO;

namespace _1_Задача
{
    class Program
    {
        public static string path = "";
        public static bool GetMax(float[] m, out int nummax)
        {
            float max = 0;
            nummax = 0;
            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] > max)
                {
                    max = m[i];
                    nummax = i;
                }
            }
            if (max > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            int n, k1, k2;
            float d;
            Console.WriteLine("starting");
            StreamReader sr = new StreamReader(path + "input.txt");
            string line = sr.ReadLine();
            string[] mas = line.Split(' ');
            n = Convert.ToInt32(mas[0]);
            d = float.Parse(mas[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            k1 = Convert.ToInt32(mas[2]);
            k2 = Convert.ToInt32(mas[3]);

            string[] zakazname = new string[n];
            float[] zakazcount = new float[n];
            float[] rez = new float[n];
            for (int i = 0; i < n; i++) { rez[i] = 0; };
            Dictionary<string, float> s1 = new Dictionary<string, float>();
            Dictionary<string, float> s2 = new Dictionary<string, float>();

            for (int i = 0; i < n; i++)
            {
                line = sr.ReadLine();
                string[] temp = line.Split(' ');
                zakazname[i] = temp[0].ToLower();
                zakazcount[i] = float.Parse(temp[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            }

            line = sr.ReadLine();
            for (int i = 0; i < k1; i++)
            {
                line = sr.ReadLine();
                string[] temp = line.Split(' ');
                s1.Add(temp[0].ToLower(), float.Parse(temp[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo));
            }

            line = sr.ReadLine();
            for (int i = 0; i < k2; i++)
            {
                line = sr.ReadLine();
                string[] temp = line.Split(' ');
                s2.Add(temp[0].ToLower(), float.Parse(temp[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo));
            }

            sr.Close();

            float[] delta = new float[n];
            float t = 0;
            for (int i = 0; i < n; i++)
            {
                if (s2.ContainsKey(zakazname[i]))
                {
                    t = (float)Math.Round(d / (zakazcount[i] * s1[zakazname[i]]), 4);
                    delta[i] = (float)Math.Round(t * (s1[zakazname[i]] - s2[zakazname[i]]), 4);
                }
                else
                {
                    delta[i] = 0;
                }
            }

            int j = 0;
            while (GetMax(delta, out j))
            {
                if (d >= zakazcount[j] * s1[zakazname[j]])
                {
                    rez[j] = zakazcount[j];
                    delta[j] = 0;
                    d = d - zakazcount[j] * s1[zakazname[j]];
                }
                else
                {
                    rez[j] = (float)Math.Round(d / s1[zakazname[j]], 4);
                    delta[j] = 0;
                    d = (float)Math.Round(d - rez[j] * s1[zakazname[j]], 4);
                }
            }

            StreamWriter sw = new StreamWriter(path + "output.txt");
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(rez[i].ToString("N4", System.Globalization.NumberFormatInfo.InvariantInfo));
            }
            sw.Close();
        }
    }
}