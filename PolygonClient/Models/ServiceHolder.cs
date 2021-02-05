using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonClient.Models
{
    public class ServiceHolder
    {
        public IGeoService Service {get; private set; }
        public List<Result> GetPolygons(string searchString) => Service.GetPolygons(searchString);

        public ServiceHolder(IGeoService service)
        {
            Service = service;
        }
    }
}
