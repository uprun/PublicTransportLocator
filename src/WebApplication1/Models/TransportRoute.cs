using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TransportRoute
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string RouteName {get; set;}

        public virtual IEnumerable<RoutePoint> RoutePoints { get; set; }
    }
}
