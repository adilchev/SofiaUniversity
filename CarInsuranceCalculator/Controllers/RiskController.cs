using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Models.Models;
using CarInsuranceCalculator.Observer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class RiskController:Controller, ISubject
    {
        private readonly ApplicationDbContext db;
        private IList<IObserver> observers;

        public RiskController(ApplicationDbContext db)
        {
            this.db = db;
            observers = new List<IObserver>();
        }

        public IActionResult Create(RiskOrBonus rob)
        {
            if (rob.CategoryId == 0 || rob.CategoryId == null)
            {
                var categoryList = db.Category.ToList();
                ViewBag.CategoryList = categoryList;
            }
            if (ModelState.IsValid&&rob.Nomenclature!=null)
            {
                var category = db.Category.FirstOrDefault(c => c.Id == rob.CategoryId);
                var riskOrBonus = new RiskOrBonus()
                {
                    CategoryId = rob.CategoryId,
                    Category = rob.Category,
                    Nomenclature = rob.Nomenclature,
                    //InsurersRisksOrBonuses = new List<InsurerRiskOrBonus>()
                };
                db.RisksOrBonuses.Add(riskOrBonus);
                db.SaveChanges();
                var insurers = db.Insurers.ToList();
                foreach (var insurer in insurers)
                {
                    this.Attach(insurer);
                }
                this.Notify();
                return RedirectToAction("Index");
            }
            return View(rob);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(RiskOrBonus rob)
        {
            if (rob.CategoryId == 0 || rob.CategoryId == null)
            {
                var categoryList = db.Category.ToList();
                ViewBag.CategoryList = categoryList;
            }

            var riskOrBonusToEdit = db.RisksOrBonuses.FirstOrDefault(r => r.Id == rob.Id);
            if (ModelState.IsValid&&rob.Nomenclature!=null)
            {
                riskOrBonusToEdit.Nomenclature = rob.Nomenclature;
                riskOrBonusToEdit.CategoryId = rob.CategoryId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskOrBonusToEdit);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(RiskOrBonus rob)
        {

            var robToDelete = db.RisksOrBonuses.FirstOrDefault(r => r.Id == rob.Id);
            db.RisksOrBonuses.Remove(robToDelete);
            db.SaveChanges();
            var insurers = db.Insurers.ToList();
            foreach (var insurer in insurers)
            {
                this.Detach(insurer);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            var categoryList = db.Category.ToList();
            ViewBag.CategoryList = categoryList;
            var riskOrBonuses = db.RisksOrBonuses.OrderBy(r => r.Category.Name).ThenBy(r => r.Nomenclature).ToList();

            int pageSize = 20;
            return View(riskOrBonuses.ToPagedList(pageNumber,pageSize));
        }

        public void Attach(IObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }
    }
}
