using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cars_ASPCore2.Data;
using System.Security.Claims;
using Cars_ASPCore2.ViewModel;
using Cars_ASPCore2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars_ASPCore2.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CarsController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public IActionResult Index(string userId = null)
        {
            if(userId == null)
            {
                //only called when a guest customer logs in
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var model = new CarAndCustomerViewModel
            {
                Cars = _db.Cars.Where(c => c.UserId == userId),
                UserObj = _db.Users.FirstOrDefault(u => u.Id == userId)
            };
            return View(model);
        }

        //Create GET
        public IActionResult Create(string userId)
        {
            Car carObj = new Car
            {
                Year = DateTime.Now.Year,
                UserId = userId,
            };
            return View(carObj);
        }

        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _db.Add(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new Car {UserId = car.UserId});
            }

            return View(car);
        }
        //Details GET
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _db.Cars.Include(c => c.ApplicationUser).
                SingleOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);
         
        }
        //Edit GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _db.Cars.Include(c => c.ApplicationUser).
                SingleOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        //Delete GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _db.Cars.Include(c => c.ApplicationUser).
                SingleOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);

        }

        //EDit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if(id != car.Id)
            {
                
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = car.UserId});
            }
            return View(car);
        }

        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _db.Cars.SingleOrDefaultAsync(c => c.Id == id);
            if(car == null)
            {
                return NotFound();
            }
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {userId = car.UserId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}