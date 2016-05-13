using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicTransportLocatorWebApp.Models
{
    public class RoutePoint
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [ScaffoldColumn(false)]
        public int? NextRoutePointID { get; set; }

        [NotMapped]
        public SelectList NextRoutePointsList { get; set; }

        public virtual RoutePoint NextRoutePoint { get; set; }

        [ScaffoldColumn(false)]
        public int TransportRouteID { get; set; }

        [NotMapped]
        public SelectList TransportRoutesList { get; set; }

        public virtual TransportRoute TransportRoute { get; set; }
    }
}
