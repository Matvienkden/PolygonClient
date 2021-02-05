using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Models
{
    public class Result
    {
        public string ObjectName { get; private set; }
        public List<Polygon> Polygons { get; private set; }

        public Result(string name, List<Polygon> polygons)
        {
            ObjectName = name;
            Polygons = polygons;
        }
    }
}
