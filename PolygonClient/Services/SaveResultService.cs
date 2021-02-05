using PolygonClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Services
{
    public static class SaveResultService
    {
        public static void Save(List<Result> results, string fileName, int freq)
        {
            using (var file = new FileStream(fileName + ".txt", FileMode.Create))
            {
                var sw = new StreamWriter(file);
                foreach (var r in results)
                {
                    sw.WriteLine("Имя объекта: " + r.ObjectName);
                    foreach (var polygon in r.Polygons)
                    {
                        sw.WriteLine("основной полигон");
                        SavePolygon(polygon.Instance, freq, sw);

                        foreach (var ep in polygon.ExceptingPoligons)
                        {
                            sw.WriteLine("Исключающий полигон");
                            SavePolygon(ep, freq, sw);
                        }
                    }
                }
                sw.Close();
            }
        }

        // Проверка нужна для того что бы полигон при сохранении не превратился в тыкву. У полигона не может быть меньше трёх точек.
        static bool Check(int quantityPoints, int freq) => freq * 2 + 1 < quantityPoints;

        static void SavePolygon(List<GeoPoint> polygon, int freq, StreamWriter sw)
        {
            var actualFreq = freq;
            if (Check(polygon.Count, freq) == false)
            {
                // если проверку не прошли, то вычисляем максимально возможное значение для пропуска сохранения точек  
                actualFreq = (polygon.Count - 1) / 2;
            }
            var pointsToSave = polygon.Where((point, i) => freq == 1 || (i + 1) % actualFreq == 1 );

            foreach (var point in pointsToSave)
            {
                sw.WriteLine(point);
            }
        }
    }
}
