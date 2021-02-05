using PolygonClient.GeoServices;
using PolygonClient.Models;
using PolygonClient.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("Видите наименование субъекта по которому нужна информация");
                var service = new ServiceHolder(new OSM());

                var address = Console.ReadLine();
                var result = service.GetPolygons(address);

                Console.WriteLine($"\r\nВсего найдено объектов: {result.Count}");
                foreach (var r in result)
                {
                    Console.WriteLine("\r\n" + r.ObjectName);
                    Console.WriteLine($"Получено полигонов: {r.Polygons.Count}");
                    foreach (var p in r.Polygons.Select((p, i) => new { polygon = p, i }))
                    {
                        var exceptingPoints = p.polygon.ExceptingPoligons.Sum(ep => ep.Count);
                        var exceptingPointsString = exceptingPoints > 0 ? $" + {exceptingPoints} точек для исключающих областей" : "";
                        Console.WriteLine($"\t{p.i + 1}) всего точек {p.polygon.Instance.Count}" + exceptingPointsString);
                    }
                }

                int key;
                if (result.Count > 0)
                {
                    key = ReadKey("\r\nНажмите 1 для нового поиска, 2 для сохранения результата", new[] { 1, 2 });
                }
                else
                {
                    key = ReadKey("\r\nНажмите 1 для нового поиска", new[] { 1 });
                }

                if (key == 1)
                {
                    Console.Clear();
                    continue;
                }

                var freq = ReadKey("\r\nУкажите с какой частотой сохранять точки (1 - 9)", new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                var fileName = ReadFileName();

                SaveResultService.Save(result, fileName, freq);
                Console.WriteLine($"\r\nФайл {fileName}.txt успешно сохранён");

                ReadKey("\r\nНажмите 1 для нового поиска", new[] { 1 });
                Console.Clear();
            }           
        }

        static int ReadKey(string message, int[] values)
        {
            Console.WriteLine(message);
            int key;
            do
            {
                int.TryParse(Console.ReadLine(), out key);
            } while (key == 0 || !values.Contains(key));
            return key;
        }

        static string ReadFileName()
        {
            Console.WriteLine("\r\nУкажите имя файла (расширение *.txt добавится автоматически)");         
            while (true)
            {
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || Path.GetInvalidPathChars().Intersect(name).Any() )
                {
                    Console.WriteLine("Укажите другое имя файла");
                    continue;
                } 
                return name;
            }
        }

    }
}
