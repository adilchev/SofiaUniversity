using CarInsuranceCalculator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Data.Models
{
    public class CarMake:IMotorVehicleMake
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

      
    }
}
