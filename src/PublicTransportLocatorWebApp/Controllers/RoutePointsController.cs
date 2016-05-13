using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using PublicTransportLocatorWebApp.Models;

namespace PublicTransportLocatorWebApp.Controllers
{
    public class RoutePointsController : Controller
    {
        private ApplicationDbContext _context;

        public RoutePointsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: RoutePoints
        public IActionResult Index()
        {
            return View(_context.RoutePoint.ToList());
        }

        // GET: RoutePoints/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            RoutePoint routePoint = _context.RoutePoint.Single(m => m.ID == id);
            if (routePoint == null)
            {
                return HttpNotFound();
            }

            return View(routePoint);
        }

        public List<RoutePoint> GetSortedPointsByRouteId(int routeID)
        {
            List<RoutePoint> initialList = _context.RoutePoint.Where(rp => rp.TransportRouteID == routeID).ToList();
            initialList.Sort((a, b) => b.ID -  a.ID);
            foreach (var point in initialList)
            {
                point.NextRoutePoint = null;
            }
            return initialList;

        }

        public class PointSelectItem
        {
            public int? Id { get; set; }
            public string Value { get; set; }
        }

        public class RouteSelectItem
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        // GET: RoutePoints/Create
        public IActionResult Create()
        {
            var existingPoints = _context.RoutePoint.OrderBy(point => point.ID).Select(x => new PointSelectItem { Id = x.ID, Value = x.ID.ToString() }).ToList();

            existingPoints.Add(new PointSelectItem() { Id = null, Value = "None" });

            var existingRoutes = _context.TransportRoute.OrderBy(route => route.ID).Select(x => new RouteSelectItem { Id = x.ID, Value = x.RouteName }).ToList();

            var model = new RoutePoint();
            model.NextRoutePointsList = new SelectList(existingPoints, "Id", "Value");
            model.TransportRoutesList = new SelectList(existingRoutes, "Id", "Value");
            return View(model);
        }

        // POST: RoutePoints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoutePoint routePoint)
        {
            if (ModelState.IsValid)
            {
                _context.RoutePoint.Add(routePoint);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(routePoint);
        }

        // GET: RoutePoints/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            RoutePoint routePoint = _context.RoutePoint.Single(m => m.ID == id);
            if (routePoint == null)
            {
                return HttpNotFound();
            }

            return View(routePoint);
        }

        // POST: RoutePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            RoutePoint routePoint = _context.RoutePoint.Single(m => m.ID == id);
            _context.RoutePoint.Remove(routePoint);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
