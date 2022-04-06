using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlightManagement.DbContext;
using FlightManagement.Models;
using FlightManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Controllers
{
    public class FlightsController : Controller
    {
        // GET
        private readonly ApplicationDbContext _db;

        public FlightsController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var flightList = _db.Flights.Include(f =>f.Plane).ToList();
            return View(flightList);
        }           
        
        [HttpGet]
        public IActionResult Create()
        {
            var planeDb = _db.Planes.ToList();
            FlightMV flightMv = new FlightMV()
            {
                Flight = new Flight(),
                PlaneList = planeDb.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.PlaneId.ToString()
                })
              
            };
            return View(flightMv);
        }

        [HttpPost]
        public IActionResult Create(FlightMV flightVm)
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Add(flightVm.Flight);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var flightDb = _db.Flights.ToList();

            flightVm.PlaneList = flightDb.Select(f => new SelectListItem()
            {
                Text = f.Destination,
                Value = f.PlaneId.ToString()
            });
            return View(flightVm);
        }

        [HttpGet]
        public IActionResult Delete(int flightId)
        {
            var flightDb = _db.Flights.Where(f => f.FlightId == flightId);
            if (flightDb.Any())
            {
                _db.Flights.Remove(flightDb.First());
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Update(int flightId)
        {
            var flightDb = _db.Flights.Where(f => f.FlightId == flightId);
            if (flightDb.Any())
            {
                return View(flightDb.First());
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Flight flight)
        {
            var flightDb = _db.Flights.Where(f => f.FlightId == flight.FlightId);
            if (!flightDb.Any())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Flights.Update(flight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(flight);
        }
    }
}