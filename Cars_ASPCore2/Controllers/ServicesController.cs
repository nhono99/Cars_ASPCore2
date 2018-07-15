using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cars_ASPCore2.Models;
using Cars_ASPCore2.ViewModel;
using Cars_ASPCore2.Data;
using Microsoft.EntityFrameworkCore;
using Cars_ASPCore2.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Cars_ASPCore2.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServicesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index(int carId)
        {
            var car = _db.Cars.FirstOrDefault(c => c.Id == carId);
            var model = new CarInServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                VIN = car.VIN,
                Year = car.Year,
                UserId = car.UserId,
                ServiceTypeObject = _db.ServiceTypes.ToList(),
                PastServiceObject = _db.Services.Where(s => s.CarId == carId).OrderByDescending(s => s.DateAdded)
            };

            return View(model);
        }
        [Authorize(Roles = StaticDetails.AdminEndUser)]
        //GET: Services/Create
        public IActionResult Create(int carId)
        {
            var car = _db.Cars.FirstOrDefault(c=>c.Id==carId);
            var model = new CarInServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                VIN = car.VIN,
                Year = car.Year,
                UserId = car.UserId,
                ServiceTypeObject = _db.ServiceTypes.ToList(),
                PastServiceObject = _db.Services.Where(s=>s.CarId == carId).OrderByDescending(s=>s.DateAdded).Take(5)
            };

            return View(model);
        }

        [Authorize(Roles = StaticDetails.AdminEndUser)]
        //POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarInServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewServiceObj.CarId = model.CarId;
                model.NewServiceObj.DateAdded = DateTime.Now;
              
                _db.Add(model.NewServiceObj);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { carId = model.CarId });
            }
            var car = _db.Cars.FirstOrDefault(c => c.Id == model.CarId);
            var newModel = new CarInServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                VIN = car.VIN,
                UserId = car.UserId,

                ServiceTypeObject = _db.ServiceTypes.ToList(),
                PastServiceObject = _db.Services.Where(s => s.CarId == model.CarId).OrderByDescending(s => s.DateAdded).Take(5)
            };

            return View(newModel);
        }

        [Authorize(Roles = StaticDetails.AdminEndUser)]
        //Delete Get
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var service = await _db.Services.Include(s => s.Car).Include(s => s.ServiceType).
                SingleOrDefaultAsync(m=>m.Id == id);
            if (service == null) return NotFound();
            //_db.Services.Remove(service);

            return View(service);
        }
        [Authorize(Roles = StaticDetails.AdminEndUser)]
        //Delete Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Service model)
        {
            var serviceId = model.Id;
            var carId = model.CarId;
            var service = await _db.Services.SingleOrDefaultAsync(s => s.Id == serviceId);
            if (service == null) return NotFound();
            _db.Services.Remove(service);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create), new { carId = carId});
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