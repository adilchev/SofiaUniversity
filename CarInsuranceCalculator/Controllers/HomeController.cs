using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CarInsuranceCalculator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarInsuranceCalculator.Facade;

namespace CarInsuranceCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var typesOfInsurance = db.TypesOfInsurance.ToList();
            ViewBag.TypesOfInsurance = typesOfInsurance;

            var carMakes = db.CarMakes.ToList();
            carMakes.Insert(0, new CarMake { Name = "Select...", Id = 0 });
            ViewBag.CarMakes = carMakes;

            var regions = db.Regions.ToList();
            foreach (var region in regions)
            {
                region.Abbreviation += $" - {region.Name}";
            }
            regions.Insert(0, new Region() { Id = 0, Abbreviation = "Select..." });
            ViewBag.Regions = regions;

            var numberOfPayments = new List<NumberOfPayments>()
            {
                NumberOfPayments.One,
                NumberOfPayments.Two,
                NumberOfPayments.Four
            };
            ViewBag.NumberOfPayments = numberOfPayments;

            var insurers = db.Insurers.ToList();
            insurers.Insert(0, new Insurer() { Id = 0, Name = "Nowhere" });
            ViewBag.Insurers = insurers;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Calculate(InfoForInsurance info)
        {
            if (!ModelState.IsValid || info.CarMakeId == 0 || info.CarModelId == 0 || info.RegionId == 0)
            {
                return RedirectToAction("Index");
            }
            var offers = new List<Offer>();
            var typeOfInsurance = db.TypesOfInsurance.FirstOrDefault(t => t.Id == info.TypeOfInsuranceId);
            var carMake = db.CarMakes.FirstOrDefault(c => c.Id == info.CarMakeId);
            var carModel = db.CarModels.FirstOrDefault(c => c.Id == info.CarModelId);

            var region = db.Regions.FirstOrDefault(r => r.Id == info.RegionId);
            var numberOfPayments = info.NumberOfPayments;
            var insurers = db.Insurers.ToList();
            ViewBag.Insurers = insurers;
            var users = db.ApplicationUsers.ToList();
            ViewBag.Users = users;
            var insuranceInfo = new InfoForInsurance();
            if (info.CarInsurerId == 0 && info.MTPLInsurerId == 0)
            {
                insuranceInfo = new InfoForInsurance()
                {
                    CarMakeId = carMake.Id,
                    CarAge = info.CarAge,
                    CarModelId = carModel.Id,
                    RegionId = region.Id,
                    TypeOfInsuranceId = typeOfInsurance.Id
                };
                db.InfoForInsurances.Add(insuranceInfo);
                db.SaveChanges();
            }
            else if (info.CarInsurerId == 0 && info.MTPLInsurerId != 0)
            {
                insuranceInfo = new InfoForInsurance()
                {
                    CarMakeId = carMake.Id,
                    CarAge = info.CarAge,
                    CarModelId = carModel.Id,
                    MTPLInsurerId = info.MTPLInsurerId,
                    RegionId = region.Id,
                    TypeOfInsuranceId = typeOfInsurance.Id
                };
                db.InfoForInsurances.Add(insuranceInfo);
                db.SaveChanges();
            }
            else if (info.CarInsurerId != 0 && info.MTPLInsurerId == 0)
            {
                insuranceInfo = new InfoForInsurance()
                {
                    CarMakeId = carMake.Id,
                    CarAge = info.CarAge,
                    CarModelId = carModel.Id,
                    CarInsurerId = info.CarInsurerId,
                    RegionId = region.Id,
                    TypeOfInsuranceId = typeOfInsurance.Id
                };
                db.InfoForInsurances.Add(insuranceInfo);
                db.SaveChanges();
            }
            else
            {
                insuranceInfo = new InfoForInsurance()
                {
                    CarMakeId = carMake.Id,
                    CarAge = info.CarAge,
                    CarModelId = carModel.Id,
                    MTPLInsurerId = info.MTPLInsurerId,
                    CarInsurerId = info.CarInsurerId,
                    RegionId = region.Id,
                    TypeOfInsuranceId = typeOfInsurance.Id
                };
                db.InfoForInsurances.Add(insuranceInfo);
                db.SaveChanges();
            }

            foreach (var insurer in insurers)
            {
                var premium = 0.0;
                if (info.Value > carModel.Price)
                {
                    premium = carModel.Price;
                }
                else
                {
                    premium = info.Value;
                }

                var currentTypeOfInsurance = db.InsurersTypesOfInsurance.FirstOrDefault(t => t.InsurerId == insurer.Id
                                                                                             && typeOfInsurance.Id == t.TypeOfInsuranceId);
                var insurerRisksOrBonuses = db.InsurersRisksOrBonuses.Where(irb => irb.InsurerId == insurer.Id).ToList();
                if (currentTypeOfInsurance == null)
                {
                    continue;
                }
                var tariffNumber = currentTypeOfInsurance.TariffNumber;
                premium = tariffNumber * premium;
       
                var calcMVRelatedRisks = new CalculateMVRelatedRisksSubSystem();
                var calOwnerRelatedRisks = new CalculateOwnerRelatedRisksSubSystem();
                var calcOtherRisks = new CalculateOtherRisksSubSystem();
                var facade = new CalculateTariffNumberFacade(calcMVRelatedRisks,calOwnerRelatedRisks, calcOtherRisks,
                    db,tariffNumber,insurerRisksOrBonuses,info,insurer,carModel,region,numberOfPayments);

                facade.CalculateTariffNumber();


                if (tariffNumber <= insurer.MaximumDiscount)
                {
                    tariffNumber = insurer.MaximumDiscount;
                    premium -= tariffNumber * premium;
                }
                else if (tariffNumber <= 0)
                {
                    premium -= tariffNumber * premium;
                }
                else
                {
                    premium += tariffNumber * premium;
                }

                var offer = new Offer()
                {
                    InsurerId = insurer.Id,
                    Premium = premium,
                    InfoForInsuranceId = insuranceInfo.Id
                };
                offers.Add(offer);
                db.Offers.Add(offer);
                db.SaveChanges();
            }

            ViewBag.ListOfOffers = offers;

            return View(offers);
        }

     //   private double CalculateOtherRisksAndBonuses(NumberOfPayments numberOfPayments, double tariffNumber, Models.Models.InsurerRiskOrBonus irb,
     //                                                RiskOrBonus currentRisk, Region region)
     //   {
     //       switch (currentRisk.Nomenclature)
     //       {
     //           case "One time payment":
     //               if (numberOfPayments == NumberOfPayments.One)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car is registered in Sofia":
     //               if (region.Name == "Sofia")
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //       }
     //
     //       return tariffNumber;
     //   }
     //
     //   private double CalculateMVrelatedRisksAndBonuses(InfoForInsurance info, CarModel carModel, double tariffNumber,
     //       Models.Models.InsurerRiskOrBonus irb, RiskOrBonus currentRisk)
     //   {
     //       switch (currentRisk.Nomenclature)
     //       {
     //           case "Car model is Special":
     //               if (carModel.SpecialModel)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car age less than a year":
     //               if (info.NewImport)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car age between 1 and 4 years":
     //               if (info.CarAge >= 1 && info.CarAge <= 4)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car age between 5 and 7 years":
     //               if (info.CarAge >= 5 && info.CarAge <= 7)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car age between 8 and 10 years":
     //               if (info.CarAge >= 8 && info.CarAge <= 10)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car age over 10 years":
     //               if (info.CarAge > 10)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car is new import":
     //               if (info.NewImport)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //
     //               break;
     //           case "Car has right-side wheel":
     //               if (info.RightSideWheel)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Car horsepower is over 200":
     //               if (carModel.Horsepower>=200)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //               
     //       }
     //
     //       return tariffNumber;
     //   }
     //
     //   private double CalculateOwnerRelatedRisksAndBonuses(InfoForInsurance info, Insurer insurer, double tariffNumber,
     //                                                       Models.Models.InsurerRiskOrBonus irb, RiskOrBonus currentRisk)
     //   {
     //       switch (currentRisk.Nomenclature)
     //       {
     //           case "Owner age between 18 and 25":
     //               if (info.OwnerAge >= 18 && info.OwnerAge <= 25)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Owner age between 26 and 50":
     //               if (info.OwnerAge >= 26 && info.OwnerAge <= 50)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Owner age over 50":
     //               if (info.OwnerAge > 50)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "New Customer":
     //               if (info.CarInsurerId != insurer.Id)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //           case "Customer has MTPL":
     //               if (info.MTPLInsurerId == insurer.Id)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //
     //               break;
     //           case "Customer has car insurance":
     //               if (info.CarInsurerId==insurer.Id)
     //               {
     //                   tariffNumber += irb.TariffNumberChange;
     //               }
     //               break;
     //                   
     //       }
     //
     //       return tariffNumber;
     //   }

        public JsonResult GetCarModel(int CarMakeId)
        {
            var carModels = db.CarModels.Where(m => m.CarMakeId == CarMakeId).ToList();
         carModels.Insert(0, new CarModel() { Id = 0, Name = "Select..." });
            return Json(new SelectList(carModels, "Id", "Name"));
        }
    }
}
