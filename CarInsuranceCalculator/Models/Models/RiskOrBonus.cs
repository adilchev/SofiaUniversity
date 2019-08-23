using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Models.Models;

namespace CarInsuranceCalculator.Data.Models
{
    public class RiskOrBonus
    {
        public int Id { get; set; }
        [Required]
        public string Nomenclature { get; set; }

        //public double TariffNumberChange { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<InsurerRiskOrBonus> InsurersRisksOrBonuses { get; set; }=new List<InsurerRiskOrBonus>();
    }
}
