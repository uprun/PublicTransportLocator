using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void FirstTest()
        {
            WebApplication1.Controllers.TransportLocationsController tlc = new WebApplication1.Controllers.TransportLocationsController(null);
            List<RoutePoint> routePoints = new List<RoutePoint> {
                new RoutePoint() {
                    ID = 1,
                    Latitude = 100,
                    Longitude = 100,
                    TransportRouteID = 1
                },
                new RoutePoint() {
                    ID = 2,
                    Latitude = 0,
                    Longitude = 0,
                    TransportRouteID = 1,
                    NextRoutePointID = 1
                }
            };
            List<KeyValuePair<TransportLocation, double>> locations = new List<KeyValuePair<TransportLocation, double>> {
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 1,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0) ,
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 2,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.5),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 3,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 1),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 4,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.75),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 5,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.2)

            };
            var locationsTransformed = tlc.UpdateLocationsOnRoute(routePoints, locations);

            Assert.IsNotNull(locationsTransformed);
            Assert.AreEqual(5, locationsTransformed.Count);
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 1 && Math.Abs(loc.Latitude - 0) < 0.01 && Math.Abs(loc.Longitude - 0) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 2 && Math.Abs(loc.Latitude - 50) < 0.01 && Math.Abs(loc.Longitude - 50) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 3 && Math.Abs(loc.Latitude - 100) < 0.01 && Math.Abs(loc.Longitude - 100) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 4 && Math.Abs(loc.Latitude - 75) < 0.01 && Math.Abs(loc.Longitude - 75) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 5 && Math.Abs(loc.Latitude - 20) < 0.01 && Math.Abs(loc.Longitude - 20) < 0.01));
        }

        [Test]
        public void ThreePointsRouteTest()
        {
            WebApplication1.Controllers.TransportLocationsController tlc = new WebApplication1.Controllers.TransportLocationsController(null);
            List<RoutePoint> routePoints = new List<RoutePoint> {
                new RoutePoint() {
                    ID = 1,
                    Latitude = 200,
                    Longitude = 0,
                    TransportRouteID = 1,
                },
                new RoutePoint() {
                    ID = 2,
                    Latitude = 100,
                    Longitude = 100,
                    TransportRouteID = 1,
                    NextRoutePointID = 1
                },
                new RoutePoint() {
                    ID = 3,
                    Latitude = 0,
                    Longitude = 0,
                    TransportRouteID = 1,
                    NextRoutePointID = 2
                }
            };
            List<KeyValuePair<TransportLocation, double>> locations = new List<KeyValuePair<TransportLocation, double>> {
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 1,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0) ,
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 2,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.5),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 3,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 1),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 4,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.75),
                new KeyValuePair<TransportLocation, double> (new TransportLocation() {
                    ID = 5,
                    Latitude = 10,
                    Longitude = 10,
                    TransportRouteID = 1
                }, 0.2)

            };
            var locationsTransformed = tlc.UpdateLocationsOnRoute(routePoints, locations);

            Assert.IsNotNull(locationsTransformed);
            Assert.AreEqual(5, locationsTransformed.Count);
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 1 && Math.Abs(loc.Latitude - 0) < 0.01 && Math.Abs(loc.Longitude - 0) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 2 && Math.Abs(loc.Latitude - 100) < 0.01 && Math.Abs(loc.Longitude - 100) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 3 && Math.Abs(loc.Latitude - 200) < 0.01 && Math.Abs(loc.Longitude - 0) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 4 && Math.Abs(loc.Latitude - 150) < 0.01 && Math.Abs(loc.Longitude - 50) < 0.01));
            Assert.IsTrue(locationsTransformed.Exists(loc => loc.ID == 5 && Math.Abs(loc.Latitude - 40) < 0.01 && Math.Abs(loc.Longitude - 40) < 0.01));
        }
    }
}
