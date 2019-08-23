using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Facade
{
    public class CalculateMVRelatedRisksSubSystem
    {
        public double CalculateMVrelatedRisksAndBonuses(InfoForInsurance info, CarModel carModel, double tariffNumber,
            Models.Models.InsurerRiskOrBonus irb, RiskOrBonus currentRisk)
        {
            switch (currentRisk.Nomenclature)
            {
                case "Car model is Special":
                    if (carModel.SpecialModel)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car age less than a year":
                    if (info.NewImport)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car age between 1 and 4 years":
                    if (info.CarAge >= 1 && info.CarAge <= 4)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car age between 5 and 7 years":
                    if (info.CarAge >= 5 && info.CarAge <= 7)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car age between 8 and 10 years":
                    if (info.CarAge >= 8 && info.CarAge <= 10)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car age over 10 years":
                    if (info.CarAge > 10)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car is new import":
                    if (info.NewImport)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }

                    break;
                case "Car has right-side wheel":
                    if (info.RightSideWheel)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car horsepower is over 200":
                    if (carModel.Horsepower >= 200)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;

            }

            return tariffNumber;
        }
    }
}
