using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Models;
using CarInsuranceCalculator.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class InsurerRiskController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public InsurerRiskController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Insurer")]
        public IActionResult Create(InsurerRiskOrBonus irb)
        {
            var categories = db.Category.ToList();
            ViewBag.Categories = categories;
            var risksAndBonuses = db.RisksOrBonuses.ToList();
            ViewBag.RisksOrBonuses = risksAndBonuses;
            if (ModelState.IsValid && irb.RiskOrBonusId != 0)
            {

                var userId = GetCurrentUserAsync().Result.Id;
                var insurer = db.Insurers.FirstOrDefault(i => i.ApplicationUserId == userId);
                var risk = risksAndBonuses.FirstOrDefault(r => r.Id == irb.RiskOrBonusId);
                var riskHasBeenAdded =
                    db.InsurersRisksOrBonuses.FirstOrDefault(i => i.RiskOrBonusId == irb.RiskOrBonusId&&i.InsurerId==insurer.Id);
                if (riskHasBeenAdded != null)
                {
                    ModelState.AddModelError(string.Empty, "This risk has already been added!");
                    return View(irb);
                }
                var insuranceRiskOrBonus = new InsurerRiskOrBonus()
                {
                    Insurer = insurer,
                    InsurerId = insurer.Id,
                    RiskOrBonus = risk,
                    RiskOrBonusId = risk.Id,
                    TariffNumberChange = irb.TariffNumberChange
                };
                db.InsurersRisksOrBonuses.Add(insuranceRiskOrBonus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(irb);
        }
        [Authorize(Roles = "Insurer")]
        public IActionResult Edit(InsurerRiskOrBonus irb)
        {
            var risksAndBonuses = db.RisksOrBonuses.FirstOrDefault(rob => rob.Id == irb.RiskOrBonusId);
            if (risksAndBonuses != null)
            {
                ViewBag.RisksOrBonuses = risksAndBonuses.Nomenclature;
                var categories = db.Category.FirstOrDefault(c => c.Id == risksAndBonuses.CategoryId);
                ViewBag.Categories = categories.Name;
            }

            var insurerRiskOrBonusToEdit = db.InsurersRisksOrBonuses.FirstOrDefault(i =>
                i.InsurerId == irb.InsurerId && i.RiskOrBonusId == irb.RiskOrBonusId);

            if (irb.TariffNumberChange!=insurerRiskOrBonusToEdit.TariffNumberChange)
            {
                if (irb.TariffNumberChange!=0)
                {
                    insurerRiskOrBonusToEdit.TariffNumberChange = irb.TariffNumberChange;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tariff number cannot be 0!");
                }

            }

            return View(insurerRiskOrBonusToEdit);
        }
        [Authorize(Roles = "Insurer")]
        public IActionResult Delete(InsurerRiskOrBonus irb)
        {
            var insurerRiskOrBonusToDelete = db.InsurersRisksOrBonuses.FirstOrDefault(i =>
                i.InsurerId == irb.InsurerId && i.RiskOrBonusId == irb.RiskOrBonusId);
            db.Remove(insurerRiskOrBonusToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(InsurerRiskOrBonus irb, int? page)
        {
            int pageNumber = (page ?? 1);
            var insurers = db.Insurers.FirstOrDefault(i => i.Id == irb.InsurerId);
            ViewBag.Insurers = insurers;
            var risks = db.RisksOrBonuses.OrderBy(r=>r.Nomenclature).ToList();
            ViewBag.Risks = risks;
            var insurersRisksOrBonuses = db.InsurersRisksOrBonuses.Where(i =>
                i.InsurerId == irb.InsurerId).ToList();
           
            ViewBag.InsurerWithRisksAndBonuses = insurersRisksOrBonuses;
            int pageSize = 20;
            return View(insurersRisksOrBonuses.ToPagedList(pageNumber,pageSize));
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var insurers = db.Insurers.OrderBy(i => i.Name).ToList();
            ViewBag.Insurers = insurers;
            var risks = db.RisksOrBonuses.ToList();
            ViewBag.Risks = risks;
            var userId = GetCurrentUserAsync().Result.Id;

            var currentInsurer = db.Insurers.FirstOrDefault(i => i.ApplicationUserId == userId);
            if (currentInsurer != null)
            {
                ViewBag.CurrentUser = currentInsurer.Id;
            }

            var insurersRisksOrBonuses = db.InsurersRisksOrBonuses.OrderBy(i => i.Insurer.Name)
                .ThenBy(i => i.RiskOrBonus.Nomenclature).ToList();


            int pageSize = 20;
            return View(insurersRisksOrBonuses.ToPagedList(pageNumber,pageSize));
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}
