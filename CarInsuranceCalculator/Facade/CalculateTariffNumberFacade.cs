using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Facade
{
    public class CalculateTariffNumberFacade
    {
        private CalculateMVRelatedRisksSubSystem mvRelatedRisks;

        private CalculateOwnerRelatedRisksSubSystem ownerRelatedRisks;

        private CalculateOtherRisksSubSystem otherRisks;

        private ApplicationDbContext db;
        private double tariffNumber;
        private List<InsurerRiskOrBonus> insurerRisksOrBonuses;
        private InfoForInsurance info;
        private Insurer insurer;
        private CarModel carModel;
        private Region region;
        private NumberOfPayments numberOfPayments;

        public CalculateTariffNumberFacade(CalculateMVRelatedRisksSubSystem mvRelatedRisks,
            CalculateOwnerRelatedRisksSubSystem ownerRelatedRisks,
            CalculateOtherRisksSubSystem otherRisks,
            ApplicationDbContext db, 
            double tariffNumber,
            List<InsurerRiskOrBonus> insurerRisksOrBonuses,
            InfoForInsurance info, 
            Insurer insurer,
            CarModel carModel,
            Region region,
            NumberOfPayments numberOfPayments)
        {
            this.mvRelatedRisks = mvRelatedRisks;
            this.ownerRelatedRisks = ownerRelatedRisks;
            this.otherRisks = otherRisks;
            this.db = db;
            this.tariffNumber = tariffNumber;
            this.insurerRisksOrBonuses = insurerRisksOrBonuses;
            this.info = info;
            this.insurer = insurer;
            this.carModel = carModel;
            this.region = region;
            this.numberOfPayments = numberOfPayments;
        }

        public double CalculateTariffNumber()
        {
            foreach (var irb in insurerRisksOrBonuses)
            {
                var currentRisk = db.RisksOrBonuses.FirstOrDefault(r => r.Id == irb.RiskOrBonusId);
                var categories = db.Category.FirstOrDefault(c => c.Id == currentRisk.CategoryId);
                if (categories.Name == "Owner related")
                {
                    tariffNumber = ownerRelatedRisks.CalculateOwnerRelatedRisksAndBonuses(info, insurer, tariffNumber, irb, currentRisk);
                }
                else if (categories.Name == "MV related")
                {
                    tariffNumber = mvRelatedRisks.CalculateMVrelatedRisksAndBonuses(info, carModel, tariffNumber, irb, currentRisk);
                }
                else if (categories.Name == "Other")
                {
                    tariffNumber = otherRisks.CalculateOtherRisksAndBonuses(numberOfPayments, tariffNumber, irb, currentRisk, region);
                }
            }
            return tariffNumber;
        }
    }
}
