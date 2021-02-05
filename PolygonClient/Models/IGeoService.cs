using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Models
{
    public interface IGeoService
    {
        List<Result> GetPolygons(string searchString);
    }
}
