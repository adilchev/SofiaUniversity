using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Models;
using CarInsuranceCalculator.Models.Models;
using CarInsuranceCalculator.Observer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CarInsuranceCalculator.Controllers
{
    [Authorize]
    public class InsurerController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        

        public InsurerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
          
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return RedirectToAction("Register", "Account");
        }

        public IActionResult Edit(Insurer insurer)
        {
            var insurerToEdit = db.Insurers.FirstOrDefault(i => i.Id == insurer.Id);
            //var user = db.ApplicationUsers.FirstOrDefault(u => u.Id == insurer.ApplicationUserId);
            var userInfo = db.ApplicationUsers.FirstOrDefault(u => u.Id == insurerToEdit.ApplicationUserId);
            if (User.IsInRole("Insurer"))
            {
                if (insurer.MaximumDiscount != 0)
                {
                    insurerToEdit.Name = insurer.Name;
                    insurerToEdit.MaximumDiscount = insurer.MaximumDiscount;
                    userInfo.Email = insurer.ApplicationUser.Email;
                    userInfo.PhoneNumber = insurer.ApplicationUser.PhoneNumber;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Maximum discount cannot be 0");
                }
            }
            else
            {
                if (insurer.Name!=null)
                {
                    insurerToEdit.Name = insurer.Name;
                    userInfo.Email = insurer.ApplicationUser.Email;
                    userInfo.PhoneNumber = insurer.ApplicationUser.PhoneNumber;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Name is required!");
                }
               
            }
          

            return View(insurerToEdit);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Insurer ins)
        {
            var insurerToDelete = db.Insurers.FirstOrDefault(i => i.Id == ins.Id);
            var accountToDelete = db.ApplicationUsers.FirstOrDefault(u => u.Id == insurerToDelete.ApplicationUserId);
            db.Insurers.Remove(insurerToDelete);
            db.ApplicationUsers.Remove(accountToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
           // var insurers = db.Insurers.ToList();
            
           var userId = GetCurrentUserAsync().Result.Id;
            var users = db.ApplicationUsers.ToList();
            ViewBag.Users = users;
            var currentInsurer = db.Insurers.FirstOrDefault(i => i.ApplicationUserId == userId);
            if (currentInsurer != null)
            {
                ViewBag.CurrentUser = currentInsurer.Id;
            }

            var insurers = db.Insurers.OrderBy(i => i.Name).ToList();
            ViewBag.Insurers = insurers;


            int pageSize = 20;
            return View(insurers.ToPagedList(pageNumber,pageSize));
        }

        public IActionResult Messages()
        {
            var users = db.ApplicationUsers.ToList();
            ViewBag.Users = users;
            return View(Logger.GetLogger());
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);



      // public void Update(ISubject subject)
      // {
      //     this.updateMessages.Add($"At {DateTime.Now}, a new Risk was created! Please fill in your tariff number.");
      // }
    }
}
