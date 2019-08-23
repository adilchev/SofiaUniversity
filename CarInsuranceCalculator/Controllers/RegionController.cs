using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class RegionController : Controller
    {
        private readonly ApplicationDbContext db;
        public RegionController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Region regionModel)
        {
            if (ModelState.IsValid)
            {
                var regionExists = db.Regions.Any(r => r.Name == regionModel.Name || r.Abbreviation==regionModel.Abbreviation);
                if (regionExists)
                {
                    ModelState.AddModelError(string.Empty,"This region already exists");
                    return View(regionModel);
                }
                var model = new Region()
                {
                    Abbreviation = regionModel.Abbreviation,
                    Name = regionModel.Name

                };
                db.Regions.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(regionModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Region regionModel)
        {

            var modelToDelete = db.Regions.FirstOrDefault(r => r.Id == regionModel.Id);
            db.Regions.Remove(modelToDelete);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Region regionModel)
        {
            var regionToEdit = db.Regions.FirstOrDefault(r => r.Id == regionModel.Id);
            if (ModelState.IsValid)
            {
                // var regionToEdit = db.Regions.FirstOrDefault(r => r.Id == regionModel.Id);
                regionToEdit.Name = regionModel.Name;
                regionToEdit.Abbreviation = regionModel.Abbreviation;
               // db.Update(regionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regionToEdit);
        }

        public IActionResult Index(int? page)
        {
           int pageNumber = (page ?? 1);
            var regionList = db.Regions.OrderBy(r => r.Name).ToList();

            int pageSize = 7;
            return View(regionList.ToPagedList(pageNumber,pageSize));
        }
    }
}
