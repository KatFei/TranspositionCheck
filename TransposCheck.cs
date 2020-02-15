using System;
using System.IO;
using System.Collections.Generic;

/// <summary>Статический класс <c>TransposCheck</c> 
/// содержит метод <see cref="IsTransposition(string,string)"></see>.</summary>
/// <remarks></remarks>
/// <seealso cref="IsTransposition(string,string)"></seealso>

namespace TransposeCheckNS
{
    public static class TransposCheck
    {
        /// <summary>Метод <c>IsTransposition</c> проверяет, является ли одна строка перестановкой другой.</summary>
        /// <param name="s1"> Исходная строка</param>
        /// <param name="s2"> Cтрока, с которой сравниваем</param>
        /// <returns>Возвращает <c>true</c> или <c>false</c>, в зависимости от результата проверки.</returns>
        /// <seealso cref="String"></seealso>

        public static bool IsTransposition(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                return false;
            Dictionary<char, int> itemCounts = new Dictionary<char, int>(EqualityComparer<char>.Default);
            foreach (char ch in s1)
            {
                if (itemCounts.ContainsKey(ch))
                {
                    itemCounts[ch]++;
                }
                else
                {
                    itemCounts.Add(ch, 1);
                }
            }
            foreach (char ch in s2)
            {
                if (itemCounts.ContainsKey(ch))
                {
                    itemCounts[ch]--;
                }
                else
                {
                    return false;
                }
            }
            foreach (int value in itemCounts.Values)
            {
                if (value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <seealso cref="IsTransposition(string,string)"></seealso>


        /// <summary>
        /// Точка входа для приложения.
        /// </summary>
        /// <param name="args"> Список аргументов командной строки</param>
        public static int Main(String[] args)
        {
            string srcStr1 = null;
            string srcStr2 = null;
            bool result = false;
            string workDir = Environment.CurrentDirectory;

            string path = Directory.GetParent(workDir).Parent.FullName;
            Console.WriteLine(path);
            Console.ReadKey();
            StreamReader sIn = null;
            try
            {
                sIn = new StreamReader(path + "/Input.txt");
                srcStr1 = sIn.ReadLine();
                srcStr2 = sIn.ReadLine();
            }
            catch
            {
                Console.WriteLine("\nRead from file failed: \n" + path + "/Input.txt");
            }
            finally
            {
                if (sIn != null) sIn.Close();
            }

            if ((srcStr1 != null) && (srcStr2 != null))
            {
                result = TransposCheck.IsTransposition(srcStr1, srcStr2);


                StreamWriter sOut = null;
                try
                {
                    sOut = new StreamWriter(path + "/Output.txt");
                    if (sOut != null)
                    {
                        sOut.WriteLine(result);
                        Console.WriteLine("\nResult successfully written to " + path + "/Output.txt");
                    }
                }
                catch
                {
                    Console.WriteLine("\nWrite to  file failed");
                }
                finally
                {
                    if (sOut != null) sOut.Close();
                }

            }


            Console.ReadKey();
            return 0;
        }
    }

}
