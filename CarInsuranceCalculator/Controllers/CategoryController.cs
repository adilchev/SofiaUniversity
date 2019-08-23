using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class CategoryController:Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = cat.Name
                };
                db.Category.Add(category);
                db.SaveChanges();
              return RedirectToAction("Index");
            }

            return View(cat);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Category cat)
        {
            var categoryToDelete = db.Category.FirstOrDefault(c => c.Id == cat.Id);
            var risksWithThatCategory = db.RisksOrBonuses.Where(r => r.CategoryId == categoryToDelete.Id).ToList();
            db.RisksOrBonuses.RemoveRange(risksWithThatCategory);
            db.Category.Remove(categoryToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Category cat)
        {
            var categoryToEdit = db.Category.FirstOrDefault(c => c.Id == cat.Id);
            if (ModelState.IsValid)
            {
                categoryToEdit.Name = cat.Name;
                db.SaveChanges();
               return RedirectToAction("Index");
            }

            return View(categoryToEdit);
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var categoryList = db.Category.OrderBy(r => r.Name);

            int pageSize = 7;
            return View(categoryList.ToPagedList(pageNumber,pageSize));
        }
    }
}
