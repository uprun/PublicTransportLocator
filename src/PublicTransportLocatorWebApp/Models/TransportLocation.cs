using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PublicTransportLocatorWebApp.Models
{
    public class TransportLocation
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public DateTime LocationRecordedTime { get; set; }

        [ScaffoldColumn(false)]
        public int TransportRouteID { get; set; }

        public virtual TransportRoute TransportRoute { get; set; }
    }
}
