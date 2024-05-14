using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prackt16_3_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("file.txt"))
            {
                try
                {
                    //Заполнение массива данными из файла
                    string[] valuesOfFile = File.ReadAllText("file.txt").Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                    double[] numbers = valuesOfFile.Select(double.Parse).ToArray();

                    //Определение и вывод частоты
                    var ch = numbers.GroupBy(n => n)
                        .Select(g => new
                        {
                            num = g.Key,
                            period = g.Count()
                        });
                    Console.WriteLine("Число\tЧастота");
                    foreach (var v in ch)
                    {
                        Console.WriteLine($"{v.num}\t{v.period}");
                    }

                    //Создание нового массива и его запись в файл
                    var newArray = numbers.Select(n => n * ch.First(x => x.num == n).period).ToArray().GroupBy(n => n)
                        .Select(g => new
                        {
                        num = g.Key,
                        period = g.Count()
                        }); ;
                    if (File.Exists("newarray.txt"))
                    {
                        File.WriteAllLines("newarray.txt", newArray.Select(x => x.ToString()));
                    }
                    else Console.WriteLine("Файл для записи нового массива не найден!");

                    //Вывод нового массива
                    Console.WriteLine("Новый массив: ");
                    foreach (var v in newArray)
                    {
                        Console.WriteLine($"{v.num} - {v.period}");
                    }
                }
                catch { Console.WriteLine("В файле есть данные неверного формата!"); }
            }
            else Console.WriteLine("Файл с данными не найден!");

            Console.ReadKey();
        }
    }
}
