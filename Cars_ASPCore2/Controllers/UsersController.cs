﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cars_ASPCore2.Models;
using Cars_ASPCore2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cars_ASPCore2.Utility;
namespace Cars_ASPCore2.Controllers
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class UsersController : Controller
    {


        private ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string option = null, string search = null)
        {
            var users = _db.Users.ToList();
            if (option == "email" && search != null)
            {
                users = _db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                if (option == "name" && search != null)
                {
                    users = _db.Users.Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                    || u.LastName.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    if (option == "phone" && search != null)
                    {
                        users = _db.Users.Where(u => u.PhoneNumber.ToLower().Contains(search.ToLower())).ToList();
                    }
                }
            }
            return View(users);
        }
        //GET: Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }
        //GET: Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }
        //GET: Delte
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }

        //POST---------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (!ModelState.IsValid) return View("Edit", user);
            else
            {
                var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
                if (userInDb == null) return NotFound();
                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Address = user.Address;
                userInDb.City = user.City;
                userInDb.PostalCode = user.PostalCode;

                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Users");
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            var cars = _db.Cars.Where(c => c.UserId == user.Id);
            foreach (var car in cars)
            {
                var services = _db.Services.Where(c => c.CarId == car.Id);
                _db.Services.RemoveRange(services);
            }
            _db.Cars.RemoveRange(cars);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Users");
            //return RedirectToAction(nameof(Index));
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