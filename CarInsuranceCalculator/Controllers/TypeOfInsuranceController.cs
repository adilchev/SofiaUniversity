using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TypeOfInsuranceController:Controller
    {
        private readonly ApplicationDbContext db;

        public TypeOfInsuranceController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Create(TypeOfInsurance toi)
        {
            if (ModelState.IsValid)
            {
                var typeOfInsurance = new TypeOfInsurance()
                {
                    Name = toi.Name,
                    InsurersTypesOfInsurance = new List<InsurerTypeOfInsurance>()
                };
                db.TypesOfInsurance.Add(typeOfInsurance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toi);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(TypeOfInsurance toi)
        {
            var typeOfInsuranceToEdit = db.TypesOfInsurance.FirstOrDefault(t => t.Id == toi.Id);
            if (ModelState.IsValid)
            {
                typeOfInsuranceToEdit.Name = toi.Name;
                if (toi.InsurersTypesOfInsurance==null)
                {
                    typeOfInsuranceToEdit.InsurersTypesOfInsurance=new List<InsurerTypeOfInsurance>();
                }
                else
                {
                    typeOfInsuranceToEdit.InsurersTypesOfInsurance = toi.InsurersTypesOfInsurance;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeOfInsuranceToEdit);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(TypeOfInsurance toi)
        {
            var typeOfInsuranceToDelete = db.TypesOfInsurance.FirstOrDefault(t => t.Id == toi.Id);
            db.TypesOfInsurance.Remove(typeOfInsuranceToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var typeOfInsuranceList = db.TypesOfInsurance.OrderBy(r => r.Name).ToList();

            int pageSize = 7;
            return View(typeOfInsuranceList.ToPagedList(pageNumber,pageSize));
        }
    }
}
