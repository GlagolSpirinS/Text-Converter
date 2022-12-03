using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace ConsoleApp1
{
    internal class Program
    {
        public static int min = 0;
        public static int max = 0;
        public static int pos = 0;
        public static int pis = 0;
        public static string[] jopa1;
        public static string jopa0;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу");
            jopa0 = Console.ReadLine();
            jopa1 = File.ReadAllLines(jopa0);
            min = Console.CursorTop;

            foreach (var item in jopa1)
            {
                Console.WriteLine(item);
            }
            max = Console.CursorTop;
            Console.SetCursorPosition(0, min);
            strelka();
            Console.ReadLine();

            static int strelka()
            {
                var key = Console.ReadKey();
                while (key.Key != ConsoleKey.F1)
                {
                    while (key.Key != ConsoleKey.Backspace || !Char.IsLetterOrDigit(key.KeyChar) || key.Key != ConsoleKey.Spacebar)
                    {
                        switch (key.Key)
                        {
                            case ConsoleKey.UpArrow:
                                pos--;
                                pis = 0;
                                if (pos < 0) pos = max;
                                break;

                            case ConsoleKey.DownArrow:
                                pos++;
                                pis = 0;
                                if (pos > max) pos = 0;
                                break;

                            case ConsoleKey.LeftArrow:
                                if (pis != 0) pis--;
                                break;

                            case ConsoleKey.RightArrow:
                                if (jopa1[pos].Length != pis) pis++;

                                break;

                            case ConsoleKey.Backspace:
                                MakeListToString(pos, pis - 1, key);
                                if (pis != 0) pis--;
                                break;
                                // тут вообще песня, вместо того чтоб проверять формат файла, я забубенил что идёт после точки.
                            case ConsoleKey.F1:
                                Console.Clear();
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("Напише формат в котором вы хотите сохранить файл.");
                                string path = Console.ReadLine();
                                string[] paths = path.Split(".");
                                string extension = paths[1];
                                if (extension == "txt")
                                {

                                }
                                else if (extension == "json")
                                {
                                    List<Alcohol> ListObject = new List<Alcohol>();
                                    Alcohol Object = new Alcohol();
                                    foreach (var item in jopa1)
                                    {
                                        if (!int.TryParse(item, out int number)) Object.Name = item;
                                        else
                                        {
                                            Object.Price = Convert.ToInt32(item);
                                            ListObject.Add(Object);
                                            Object = new Alcohol();
                                        }
                                    }
                                    using StreamWriter sw = File.CreateText(path);
                                    sw.WriteLine(JsonConvert.SerializeObject(ListObject));
                                }
                                else if (extension == "xml")
                                {
                                    XmlSerializer xml = new XmlSerializer(typeof(Alcohol));
                                    using StreamWriter xm = File.CreateText(path);
                                }
                                break;
                            default:
                                MakeListToString(pos, pis, key);
                                pis++;
                                break;
                        }
                        Console.SetCursorPosition(pis, pos + min);
                        key = Console.ReadKey();
                    }
                }
                return pos;
            }

            static void MakeListToString(int a, int b, ConsoleKeyInfo key)
            {
                if (key.Key == ConsoleKey.Backspace) jopa1[a] = jopa1[a].Remove(b, 1);
                else jopa1[a] = jopa1[a].Insert(b, Convert.ToString(key.KeyChar));
                Console.SetCursorPosition(0, a + min);
                Console.Write(string.Format("{{0, -{0}}}", Console.BufferWidth), jopa1[a]); //от нулевой позиции очищаеться хрень кроме моей жопы которая идёт не дальше консоли спиздил с интернета у индуса
            }
        }
    }
}