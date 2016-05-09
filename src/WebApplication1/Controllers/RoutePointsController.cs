using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

        // GET: RoutePoints/Create
        public IActionResult Create()
        {
            return View();
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

        // GET: RoutePoints/Edit/5
        public IActionResult Edit(int? id)
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

        // POST: RoutePoints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoutePoint routePoint)
        {
            if (ModelState.IsValid)
            {
                _context.Update(routePoint);
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