using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Models;
using CarInsuranceCalculator.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;


namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class InsurerTypeOfInsuranceController:Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public InsurerTypeOfInsuranceController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Insurer")]
        public IActionResult Create(InsurerTypeOfInsurance itoi)
        {
            var typeOfInsuranceList = db.TypesOfInsurance.ToList();
            ViewBag.TypeOfInsuranceList = typeOfInsuranceList;
           
            if (ModelState.IsValid && itoi.TypeOfInsuranceId!=0)
            {
               
                var userId = GetCurrentUserAsync().Result.Id;
                var insurer = db.Insurers.FirstOrDefault(i => i.ApplicationUserId == userId);
                var typeOfInsurance = db.TypesOfInsurance.FirstOrDefault(toi => toi.Id == itoi.TypeOfInsuranceId);
                var inusrerTypeOfInsuranceAlreadyExists =
                    db.InsurersTypesOfInsurance.FirstOrDefault(i => i.TypeOfInsuranceId == itoi.TypeOfInsuranceId&&i.InsurerId==insurer.Id);
                if (inusrerTypeOfInsuranceAlreadyExists!=null)
                {
                    ModelState.AddModelError(string.Empty,"This type of insurance has already been added!");
                    return View(itoi);
                }
                var insurerTypeOfInsurance = new InsurerTypeOfInsurance()
                {
                    InsurerId = insurer.Id,
                    Insurer = insurer,
                    TypeOfInsuranceId = typeOfInsurance.Id,
                    TypeOfInsurance = typeOfInsurance,
                    TariffNumber = itoi.TariffNumber
                    
                };
                db.InsurersTypesOfInsurance.Add(insurerTypeOfInsurance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itoi);
        }
        [Authorize(Roles = "Insurer")]
        public IActionResult Edit(InsurerTypeOfInsurance itoi)
        {
            var typeOfInsurance = db.TypesOfInsurance.FirstOrDefault(t => t.Id == itoi.TypeOfInsuranceId);
            if (typeOfInsurance!=null)
            {
                ViewBag.TypesOfInsurance = typeOfInsurance.Name;
            }

            var typeOfInsuranceToEdit = db.InsurersTypesOfInsurance.FirstOrDefault(t =>
                t.TypeOfInsuranceId == itoi.TypeOfInsuranceId && t.InsurerId == itoi.InsurerId);
            if (itoi.TariffNumber != typeOfInsuranceToEdit.TariffNumber)
            {
                if (itoi.TariffNumber!=0)
                {
                    typeOfInsuranceToEdit.TariffNumber = itoi.TariffNumber;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Tariff number cannot be 0!");
                }
            }
            return View(typeOfInsuranceToEdit);
        }
        [Authorize(Roles = "Insurer")]
        public IActionResult Delete(InsurerTypeOfInsurance itoi)
        {
            var insuranceTypeOfInsuranceToDelete = db.InsurersTypesOfInsurance.FirstOrDefault(i =>
                i.InsurerId == itoi.InsurerId && i.TypeOfInsuranceId == itoi.TypeOfInsuranceId);
            db.Remove(insuranceTypeOfInsuranceToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(InsurerTypeOfInsurance itoi, int? page)
        {
            int pageNumber = (page ?? 1);
            var insurers = db.Insurers.FirstOrDefault(i=>i.Id==itoi.InsurerId);
            ViewBag.Insurers = insurers;
            var typeOfInsuranceList = db.TypesOfInsurance.ToList();
            ViewBag.TypeOfInsuranceList = typeOfInsuranceList;
            var insurersTypesOfInsurance = db.InsurersTypesOfInsurance.Where(i => i.InsurerId==itoi.InsurerId).ToList();
            ViewBag.InsurersTypesOfInsurance = insurersTypesOfInsurance;

            int pageSize = 20;

            return View(insurersTypesOfInsurance.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var insurers = db.Insurers.OrderBy(i => i.Name).ToList();
            ViewBag.Insurers = insurers;
            var typeOfInsuranceList = db.TypesOfInsurance.ToList();
            ViewBag.TypeOfInsuranceList = typeOfInsuranceList;
            var userId = GetCurrentUserAsync().Result.Id;

            var currentInsurer = db.Insurers.FirstOrDefault(i => i.ApplicationUserId == userId);
            if (currentInsurer != null)
            {
                ViewBag.CurrentUser = currentInsurer.Id;
            }

            var insurersTypesOfInsurance = db.InsurersTypesOfInsurance.OrderBy(i => i.Insurer.Name)
                .ThenBy(i => i.TypeOfInsurance.Name).ToList();


            int pageSize = 20;
            return View(insurersTypesOfInsurance.ToPagedList(pageNumber,pageSize));
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}
