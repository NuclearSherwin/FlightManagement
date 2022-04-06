using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlightManagement.DbContext;
using FlightManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagement.Controllers
{
    public class PlanesController : Controller
    { 
        private readonly ApplicationDbContext _db;

        public PlanesController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var planesList = _db.Planes.ToList();
            return View(planesList);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Plane());
        }

        [HttpPost]
        public IActionResult Create(Plane plane)
        {
            if (ModelState.IsValid)
            {
                _db.Planes.Add(plane);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plane);
        }

        [HttpGet]
        public IActionResult Delete(int planeId)
        {
            var planeDb = _db.Planes.Where(p => p.PlaneId == planeId);
            if (planeDb.Any())
            {
                _db.Planes.Remove(planeDb.First());
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Update(int planeId)
        {
            var planeDb = _db.Planes.Where(p => p.PlaneId == planeId);
            if (planeDb.Any())
            {
                return View(planeDb.First());
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Plane plane)
        {
            var planeDb = _db.Planes.Where(p => p.PlaneId == plane.PlaneId);
            if (!planeDb.Any())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Planes.Update(plane);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(plane);
        }
    }
}