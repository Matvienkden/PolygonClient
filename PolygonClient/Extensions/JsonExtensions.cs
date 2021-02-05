using Newtonsoft.Json.Linq;
using PolygonClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Extensions
{
    public static class JsonExtensions
    {
        public static GeoPoint CreatePoint(this JToken j)
        {
            return new GeoPoint((double)j[0], (double)j[1]);
        }

        public static Polygon CreatePolygon(this JToken j)
        {
            var polygon = j[0].Select(jpoint => jpoint.CreatePoint())
                .ToList();

            var exceptingPoligons = j.Skip(1)
                .Select(jpol => jpol.Select(jpoint => jpoint.CreatePoint()).ToList())
                .ToList();

            return new Polygon(polygon, exceptingPoligons);             
        }
    }
}
