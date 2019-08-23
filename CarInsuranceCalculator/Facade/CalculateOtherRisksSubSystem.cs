using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Facade
{
    public class CalculateOtherRisksSubSystem
    {




        public double CalculateOtherRisksAndBonuses(NumberOfPayments numberOfPayments, double tariffNumber, Models.Models.InsurerRiskOrBonus irb,
                                                    RiskOrBonus currentRisk, Region region)
        {
            switch (currentRisk.Nomenclature)
            {
                case "One time payment":
                    if (numberOfPayments == NumberOfPayments.One)
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
                case "Car is registered in Sofia":
                    if (region.Name == "Sofia")
                    {
                        tariffNumber += irb.TariffNumberChange;
                    }
                    break;
            }

            return tariffNumber;
        }
    }
}
