using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication1.Models;
using System.Collections.Generic;
using System;

namespace WebApplication1.Controllers
{
    public class TransportLocationsController : Controller
    {
        private ApplicationDbContext _context;

        public TransportLocationsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TransportLocations
        [HttpGet]
        public IActionResult Index(string sortBy = nameof(TransportLocation.ID), string sortType = "asc")
        {
            List<TransportLocation> initialList = _context.TransportLocation.ToList();

            sortType = sortType.ToLower();
            Comparison<TransportLocation> comparatorAsc;
            Comparison<TransportLocation> comparator;
            switch (sortBy)
            {
                case nameof(TransportLocation.ID):
                        comparatorAsc = (a, b) => a.ID - b.ID;
                    break;
                case nameof(TransportLocation.TransportRouteID): comparatorAsc = (a, b) => a.TransportRouteID.CompareTo(b.TransportRouteID);
                    break;
                case nameof(TransportLocation.LocationRecordedTime): comparatorAsc = (a, b) => a.LocationRecordedTime.CompareTo(b.LocationRecordedTime);
                    break;
                case nameof(TransportLocation.Latitude): comparatorAsc = (a, b) => {
                        if (a.Latitude < b.Latitude)
                        {
                            return -1;
                        }
                        else if (a.Latitude > b.Latitude)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }

                    };
                    break;
                case nameof(TransportLocation.Longitude):
                    comparatorAsc = (a, b) => {
                        if (a.Longitude < b.Longitude)
                        {
                            return -1;
                        }
                        else if (a.Longitude > b.Longitude)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }

                    };
                    break;
                default: return HttpBadRequest();
            }

            switch (sortType)
            {
                case "asc": comparator = comparatorAsc;
                    break;
                case "desc": comparator = (a, b) => comparatorAsc(b, a);
                    break;
                default: return HttpBadRequest();
            }
            initialList.Sort(comparator);
            return View(initialList);
        }

        public IEnumerable<TransportLocation> GetAllLocations()
        {
            return _context.TransportLocation.ToList();
        }

        public bool DeleteAllLocations()
        {
            _context.TransportLocation.RemoveRange(_context.TransportLocation);
            _context.SaveChanges();
            return true;
        }

        public bool GenerateRandomLocations()
        {
            double leftBottom_latitude = 46.3819427;
            double leftBottom_longitude = 30.6671623;
            double topRight_latitude = 46.4828937;
            double topRight_longitude = 30.7564793;

            Random rnd = new Random((int)DateTime.Now.Ticks);

            TransportRoute route = new TransportRoute() {
                RouteName = "Random Route"
            };

            _context.TransportRoute.Add(route);

            int points = 10;
            for (int i = 0; i < points; i++)
            {
                double latitude = rnd.NextDouble() * (topRight_latitude - leftBottom_latitude) + leftBottom_latitude;
                double longitude = rnd.NextDouble() * (topRight_longitude - leftBottom_longitude) + leftBottom_longitude;

                _context.TransportLocation.Add(new TransportLocation() {
                    Longitude = longitude,
                    Latitude = latitude,
                    LocationRecordedTime = DateTime.UtcNow,
                    TransportRoute = route
                });

            }
            _context.SaveChanges();
            
            return true;

        }

        public List<TransportLocation> UpdateLocationsOnRoute(List<RoutePoint> routePoints, List<KeyValuePair< TransportLocation, double>> preparedLocations )
        {
            routePoints.Sort((a, b) => b.ID - a.ID);
            List<double> partialLengths = new List<double>();
            partialLengths.Add(0);
            double totalLength = 0;
            for (int i = 1; i < routePoints.Count; ++i)
            {
                var prev = routePoints[i - 1];
                var cur = routePoints[i];
                totalLength += Math.Sqrt(Math.Pow(prev.Latitude - cur.Latitude, 2.0) + Math.Pow(prev.Longitude - cur.Longitude, 2.0));
                partialLengths.Add(totalLength);
            }

            List<TransportLocation> locationsToReturn = new List<TransportLocation>();
            foreach (var locationPair in preparedLocations)
            {
                double desiredTotalPath = totalLength * locationPair.Value;
                TransportLocation location = locationPair.Key;
                int index = partialLengths.FindIndex((ps) => ps > desiredTotalPath);
                var prev = routePoints[index - 1];
                var cur = routePoints[index];
                double localPath = desiredTotalPath - partialLengths[index - 1];
                double percentage = localPath / (partialLengths[index] - partialLengths[index - 1]);

                location.Latitude = (cur.Latitude - prev.Latitude) * percentage + prev.Latitude;
                location.Longitude = (cur.Longitude - prev.Longitude) * percentage + prev.Longitude;
                location.LocationRecordedTime = DateTime.UtcNow;

                
                locationsToReturn.Add(location);

            }
            return locationsToReturn;
        }

        [HttpPost]
        public void UpdateLocationsForDemo(int routeID = 1)
        {
            List<RoutePoint> routePoints = _context.RoutePoint.Where(rp => rp.TransportRouteID == routeID).ToList();
            
            List<TransportLocation> locations = _context.TransportLocation.ToList();

            List<KeyValuePair<TransportLocation, double>> preparedLocations = new List<KeyValuePair<TransportLocation, double>>();

            int count = 0;
            foreach (var location in locations)
            {
                // prepare location in form of route's percentage
                double desiredTotalPath = ((DateTime.Now.Second + (count * 60) / locations.Count) % 60) / 60.0 ;
                count++;
                preparedLocations.Add(new KeyValuePair<TransportLocation, double>(location, desiredTotalPath));

            }
            locations = UpdateLocationsOnRoute(routePoints, preparedLocations);
            _context.TransportLocation.UpdateRange(locations);
            _context.SaveChanges();
        }

        // GET: TransportLocations/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TransportLocation transportLocation = _context.TransportLocation.Single(m => m.ID == id);
            if (transportLocation == null)
            {
                return HttpNotFound();
            }

            return View(transportLocation);
        }

        // GET: TransportLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransportLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransportLocation transportLocation)
        {
            if (ModelState.IsValid)
            {
                _context.TransportLocation.Add(transportLocation);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportLocation);
        }

        

        // GET: TransportLocations/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TransportLocation transportLocation = _context.TransportLocation.Single(m => m.ID == id);
            if (transportLocation == null)
            {
                return HttpNotFound();
            }

            return View(transportLocation);
        }

        // POST: TransportLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TransportLocation transportLocation = _context.TransportLocation.Single(m => m.ID == id);
            _context.TransportLocation.Remove(transportLocation);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
