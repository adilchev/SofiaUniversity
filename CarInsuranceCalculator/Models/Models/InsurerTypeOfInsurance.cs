using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarInsuranceCalculator.Models.Models
{
    public class InsurerTypeOfInsurance
    {
        public int InsurerId { get; set; }
        public Insurer Insurer { get; set; }
        public int TypeOfInsuranceId { get; set; }
        public TypeOfInsurance TypeOfInsurance { get; set; }
        [Required]
        [Range(-1.0, 1.0, ErrorMessage = "Tariff number should be between -1 and 1!")]
        public double TariffNumber { get; set; }
    }
}
