using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cars_ASPCore2.Data;
using Cars_ASPCore2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cars_ASPCore2.Utility;
namespace Cars_ASPCore2.Controllers
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServiceTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        //GET: ServiceType
        public IActionResult Index()
        {
            return View(_db.ServiceTypes.ToList());
        }
        //GET: ServiceType/Create
        public IActionResult Create()
        {
            return View();
        }
        //GET: ServiceType/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }
        //GET: ServiceType/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var serviceType = await _db.ServiceTypes.FirstOrDefaultAsync(m=>m.Id == id);
            if (serviceType == null) return NotFound();
            return View(serviceType);
        }
        //GET: ServiceType/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var serviceType = await _db.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null) return NotFound();
            return View(serviceType);
        }

        //POST-------------------------------------------------------------------------------

        //POST: ServiceType/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceType serviceTypes = await _db.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);
            _db.ServiceTypes.Remove(serviceTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //POST: ServiceType/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceType serviceType)
        {
            if (id != serviceType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _db.ServiceTypes.Update(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }
        //POST: ServiceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
               
                _db.ServiceTypes.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
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