using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Models
{
    public class GeoPoint
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public GeoPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        string Format(double coord)
        {
            return String.Format("{0:f7}", coord);
        }

        public override string ToString()
        {
            return $"{Format(X)}, {Format(Y)}";
        }
    }
}
