using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Factory;
using CarInsuranceCalculator.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class CarMakeController : Controller
    {
        private readonly ApplicationDbContext db;
        private IMotorVehicleMakeFactory makeFactory;

        public CarMakeController(ApplicationDbContext db)
        {
            this.db = db;
            this.makeFactory = new CarMakeFactory();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CarMake makeInfo)
        {
            if (ModelState.IsValid)
            {
                var carMakeExists = db.CarMakes.Any(c => c.Name== makeInfo.Name);
                if (carMakeExists)
                {
                    ModelState.AddModelError(string.Empty,"This car make already exists!");

                    return View(makeInfo);
                }
                var make = this.makeFactory.CreateVehicle(makeInfo);
                //var carMake = new CarMake()
                //{
                //    Name = make.Name,
                //    Country = make.Country,
                //   
                //};
                db.CarMakes.Add((CarMake)make);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(makeInfo);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CarMake make)
        {
            var carMakeToEdit = db.CarMakes.FirstOrDefault(cm => cm.Id == make.Id);
            if (ModelState.IsValid)
            {

                carMakeToEdit.Name = make.Name;
                carMakeToEdit.Country = make.Country;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carMakeToEdit);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(CarMake make)
        {

            var makeToDelete = db.CarMakes.FirstOrDefault(cm => cm.Id == make.Id);
            db.CarMakes.Remove(makeToDelete);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);

            var carMakesList = db.CarMakes.OrderBy(r => r.Name).ToList();

            int pageSize = 20;
            return View(carMakesList.ToPagedList(pageNumber,pageSize));
        }
    }
}