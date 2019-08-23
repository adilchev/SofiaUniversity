using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Builder;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class CarModelController : Controller
    {
        private readonly ApplicationDbContext db;
        private CarModelDirector carModelDirector;
        private ICarModelBuilder carModelBuilder;

        public CarModelController(ApplicationDbContext db)
        {
            this.db = db;
            this.carModelDirector = new CarModelDirector();
            this.carModelBuilder = new CarModelBuilder();
            this.carModelDirector.CarModelBuilder = carModelBuilder;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CarModel model)
        {
            if (model.CarMakeId == 0 || model.CarMakeId == null)
            {
                var makesList = db.CarMakes.ToList();
                ViewBag.MakesList = makesList;
            }
            if (ModelState.IsValid)
            {
                var carMake = db.CarMakes.FirstOrDefault(cm => cm.Id == model.CarMakeId);
                this.carModelDirector.BuildCarModelWithAllInfo(model);
                         
                db.CarModels.Add(this.carModelBuilder.GetCarModel());
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CarModel model)
        {
            if (model.CarMakeId == 0 || model.CarMakeId == null)
            {
                var makesList = db.CarMakes.ToList();
                ViewBag.MakesList = makesList;
            }
            var carModelToEdit = db.CarModels.FirstOrDefault(cm => cm.Id == model.Id);
            if (ModelState.IsValid)
            {

                carModelToEdit.Name = model.Name;
                carModelToEdit.CarMakeId = model.CarMakeId;
                carModelToEdit.EngineCapacity = model.EngineCapacity;
                carModelToEdit.Horsepower = model.Horsepower;
                carModelToEdit.SpecialModel = model.SpecialModel;
                carModelToEdit.ProductionYear = model.ProductionYear;
                carModelToEdit.Price = model.Price;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carModelToEdit);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(CarModel model)
        {

            var modelToDelete = db.CarModels.FirstOrDefault(cm => cm.Id == model.Id);
            db.CarModels.Remove(modelToDelete);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var makesList = db.CarMakes.ToList().OrderBy(cm => cm.Name).ToList();
            ViewBag.MakesList = makesList;
            var carModelsList = db.CarModels.OrderBy(r => r.CarMake.Name).ThenBy(m => m.Name).ToList();

            int pageSize = 20;
            return View(carModelsList.ToPagedList(pageNumber,pageSize));
        }

    }
}
