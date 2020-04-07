using System;
using System.IO;
using System.Collections.Generic;



namespace TransposeCheckNS
{
    /// <summary>Статический класс <c>TransposCheck</c> 
    /// содержит метод <see cref="IsTransposition(string,string)"></see>.</summary>
    /// <remarks></remarks>
    /// <seealso cref="IsTransposition(string,string)"></seealso>
    public class TransposCheck
    {

        /// <summary>Метод <c>IsTransposition</c> проверяет, является ли одна строка перестановкой другой.
        /// Разницу в регистре не учитываем.</summary>
        /// <param name="s1"> Исходная строка</param>
        /// <param name="s2"> Cтрока, с которой сравниваем</param>
        /// <returns>Возвращает <c>true</c> или <c>false</c>, в зависимости от результата проверки.</returns>
        /// <seealso cref="String"></seealso>

        public static bool IsTransposition(string s1, string s2)
        {
            if ((s1 == null) && (s2 == null))
                throw new ArgumentNullException();
            else if (s2 == null)
                throw new ArgumentNullException("s2","Argument is null");
            else if (s1 == null)
                throw new ArgumentNullException("s1", "Argument is null");

            s1 = s1.ToLower();
            s2 = s2.ToLower();
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

    }

    /// <summary>Класс <c>StringsChecker</c> 
    /// содержит методы для работы с чтением/записью в файл и метод Main.</summary>
    /// <remarks></remarks>
    public class StringsChecker
    {

        string srcStr1 = null;
        string srcStr2 = null;
        bool result = false;
        /// <summary>Свойство <c>SrcStr1</c> содержит исходную строку N1.</summary>
        public string SrcStr1
        {
            get {    return srcStr1;     }
        }
        /// <summary>Свойство <c>SrcStr2</c> содержит исходную строку N2.</summary>
        public string SrcStr2
        {
            get { return srcStr2; }
        }

        /// <summary>Метод <c>ReadFromSource</c> считывает исходные данные из файла.</summary>
        /// <param name="path"> Путь к файлу</param>
        /// <seealso cref="String"></seealso>
        public void ReadFromSource(string path)
        {
            if (path == null)
                throw new ArgumentNullException("");

            StreamReader sIn = null;
            try
            {
                sIn = new StreamReader(path);
                srcStr1 = sIn.ReadLine();
                srcStr2 = sIn.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("\nRead from file failed: \n" + path);
                if(!File.Exists(path))
                    throw new ArgumentNullException("File does not exist", e);
                else
                    throw new ArgumentException("Some Read Exception", e);
            }
            finally
            {
                if (sIn != null) sIn.Close();
            }

            if ((SrcStr1 == null) && (SrcStr2 == null))
                throw new ArgumentException("File is empty");
        }

        /// <summary>Метод <c>WriteToFile</c> записывает результат в файл.</summary>
        /// <param name="path"> Путь к файлу</param>
        /// <seealso cref="String"></seealso>
        public void WriteToFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException();
            if ((SrcStr1 != null) && (srcStr2 != null))
            {
                result = TransposCheck.IsTransposition(SrcStr1, srcStr2);


                StreamWriter sOut = null;
                try
                {
                    sOut = new StreamWriter(path);
                    sOut.WriteLine("The result of comparing 2 strings {0}s1: {1} {0}s2: {2} {0}is:", Environment.NewLine, SrcStr1, SrcStr2);

                    sOut.WriteLine(result);
                    Console.WriteLine("\nResult successfully written to " + path);
                }
                catch(Exception e)
                {
                    Console.WriteLine("\nWrite to  file failed");
                    throw new ArgumentException("Some Write Exception", e);
                }
                finally
                {
                    if (sOut != null) sOut.Close();
                }

            }
            

        }

        /// <summary>
        /// Точка входа для приложения.
        /// </summary>
        /// <param name="args"> Список аргументов командной строки</param>
        public static int Main(String[] args)
        {
            string workDir = Environment.CurrentDirectory;

            string path = Directory.GetParent(workDir).Parent.FullName;
            Console.WriteLine(path);
            Console.ReadKey();
            StringsChecker checkingStrings = new StringsChecker();
            checkingStrings.ReadFromSource(path + "/Input.txt");
            checkingStrings.WriteToFile(path + "/Output.txt");

            Console.ReadKey();
            return 0;
        }
    }
}
