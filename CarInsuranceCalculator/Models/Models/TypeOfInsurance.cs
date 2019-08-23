using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data.Models;

namespace CarInsuranceCalculator.Models.Models
{
    public class TypeOfInsurance
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

       // public string Description { get; set; }

       // public double TariffNumber { get; set; }

        public ICollection<InsurerTypeOfInsurance> InsurersTypesOfInsurance { get; set; }=new List<InsurerTypeOfInsurance>();
    }
}
