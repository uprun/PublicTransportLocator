using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PublicTransportLocatorWebApp.Models
{
    public class TransportRoute
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string RouteName {get; set;}

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
