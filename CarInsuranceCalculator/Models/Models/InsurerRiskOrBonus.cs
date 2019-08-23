using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data.Models;

namespace CarInsuranceCalculator.Models.Models
{
    public class InsurerRiskOrBonus
    {
        public int InsurerId { get; set; }
        public Insurer Insurer { get; set; }
        public int RiskOrBonusId { get; set; }
        public RiskOrBonus RiskOrBonus { get; set; }
        [Required]
        [Range(-1.0,1.0,ErrorMessage = "Tariff number should be between -1 and 1!")]
        public double TariffNumberChange { get; set; }
    }
}
