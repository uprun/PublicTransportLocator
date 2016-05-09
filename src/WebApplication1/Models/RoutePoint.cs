using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RoutePoint
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [ScaffoldColumn(false)]
        public int? NextRoutePointID { get; set; }

        public virtual RoutePoint NextRoutePoint { get; set; }

        [ScaffoldColumn(false)]
        public int TransportRouteID { get; set; }

        public virtual TransportRoute TransportRoute { get; set; }
    }
}
