using System.Linq;
using FlightManagement.DbContext;
using FlightManagement.Models;
using FlightManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Controllers
{
    public class CustomersController : Controller
    {
        // GET
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var customersList = _db.Customers.ToList();
            return View(customersList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }
        
        [HttpPost]
        public IActionResult Create(Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _db.Customers.Add(customer);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(customer);
            }

        [HttpGet]
        public IActionResult Delete(int customerId)
        {
            var customerDb = _db.Customers.Where(c => c.CustomerId == customerId);
            if (customerDb.Any())
            {
                _db.Customers.Remove(customerDb.First());
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Update(int customerId)
        {
            var customerDb = _db.Customers.Where(c => c.CustomerId == customerId);
            if (customerDb.Any())
            {
                return View(customerDb.First());
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            var customerDb = _db.Customers.Where(c => c.CustomerId == customer.CustomerId);
            if (!customerDb.Any())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Customers.Update(customer);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }
    }
}