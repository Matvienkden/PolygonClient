using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Models
{
    public class Polygon
    {
        /// <summary>
        /// непосредственно полигон
        /// </summary>
        public List<GeoPoint> Instance { get; private set; }

        /// <summary>
        /// полигоны которые исключаем из основного 
        /// </summary>
        public List<List<GeoPoint>> ExceptingPoligons { get; private set;}

        public Polygon(List<GeoPoint> points, List<List<GeoPoint>> exceptingPoligons)
        {
            Instance = points;
            ExceptingPoligons = exceptingPoligons;
        }
    }
}
