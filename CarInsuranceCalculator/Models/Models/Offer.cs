using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Data.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public int? InsurerId { get; set; }
        public Insurer Insurer { get; set; }

        public int? InfoForInsuranceId { get; set; }
        public InfoForInsurance InfoForInsurance { get; set; }
        [Range(0,double.MaxValue)]
        public double Premium { get; set; }
    }
}
