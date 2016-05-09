using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransportRoutesController : Controller
    {
        private ApplicationDbContext _context;

        public TransportRoutesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TransportRoutes
        public IActionResult Index()
        {
            return View(_context.TransportRoute.ToList());
        }

        // GET: TransportRoutes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TransportRoute transportRoute = _context.TransportRoute.Single(m => m.ID == id);
            if (transportRoute == null)
            {
                return HttpNotFound();
            }

            return View(transportRoute);
        }

        // GET: TransportRoutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransportRoutes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransportRoute transportRoute)
        {
            if (ModelState.IsValid)
            {
                _context.TransportRoute.Add(transportRoute);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportRoute);
        }

        // GET: TransportRoutes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TransportRoute transportRoute = _context.TransportRoute.Single(m => m.ID == id);
            if (transportRoute == null)
            {
                return HttpNotFound();
            }
            return View(transportRoute);
        }

        // POST: TransportRoutes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransportRoute transportRoute)
        {
            if (ModelState.IsValid)
            {
                _context.Update(transportRoute);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportRoute);
        }

        // GET: TransportRoutes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TransportRoute transportRoute = _context.TransportRoute.Single(m => m.ID == id);
            if (transportRoute == null)
            {
                return HttpNotFound();
            }

            return View(transportRoute);
        }

        // POST: TransportRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TransportRoute transportRoute = _context.TransportRoute.Single(m => m.ID == id);
            _context.TransportRoute.Remove(transportRoute);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
