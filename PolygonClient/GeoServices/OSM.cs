using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PolygonClient.Extensions;
using PolygonClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.GeoServices
{
    public class OSM : IGeoService
    {
        public List<Result> GetPolygons(string searchString)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://nominatim.openstreetmap.org/search?q={searchString}&format=json&polygon_geojson=1");
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36";
            var response = request.GetResponse();
            var stream = response.GetResponseStream();

            JArray json;
            using (var reader = new StreamReader(stream))
            {
                json = JArray.Parse(reader.ReadToEnd());
            }
            stream.Close();

            if (json.Count == 0)
            {
                return new List<Result>();
            }

            var result = json // по задаче нам нужны только полигоны, так что отсеиваем всё что к ним не относится
               .Where(r => (string)r["geojson"]["type"] == "Polygon" || (string)r["geojson"]["type"] == "MultiPolygon")
               .Select(d => new Result((string)d["display_name"], GetPoint(d["geojson"])))
               .ToList();

            return result;
        }

        List<Polygon> GetPoint(JToken jobj)
        {
            var type = (string)jobj["type"];
            var j = jobj["coordinates"];
            if (type == "Polygon")
            {
                return new List<Polygon>
                {
                    j.CreatePolygon()
                };
            }

            return j.Select(jpol => jpol.CreatePolygon()).ToList();
        }

    }
}
