using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Facade
{
    public class CalculateOwnerRelatedRisksSubSystem
    {
        public double CalculateOwnerRelatedRisksAndBonuses(InfoForInsurance info, Insurer insurer, double tariffNumber,
                                                           Models.Models.InsurerRiskOrBonus irb, RiskOrBonus currentRisk)
        {
            switch (currentRisk.Nomenclature)
            {
                case "Owner age between 18 and 25":
                    if (info.OwnerAge >= 18 && info.OwnerAge <= 25)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Owner age between 26 and 50":
                    if (info.OwnerAge >= 26 && info.OwnerAge <= 50)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Owner age over 50":
                    if (info.OwnerAge > 50)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "New Customer":
                    if (info.CarInsurerId != insurer.Id)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Customer has MTPL":
                    if (info.MTPLInsurerId == insurer.Id)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }

                    break;
                case "Customer has car insurance":
                    if (info.CarInsurerId == insurer.Id)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;

            }

            return tariffNumber;
        }
    }
}
