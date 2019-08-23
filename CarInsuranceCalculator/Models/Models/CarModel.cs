using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Data.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(0,int.MaxValue)]
        public int EngineCapacity { get; set; }

        [Range(0,int.MaxValue)]
        public int Horsepower { get; set; }

        public int ProductionYear { get; set; }
        [Range(0.0,double.MaxValue)]
        public double Price { get; set; }

        public bool SpecialModel { get; set; }

        public int? CarMakeId { get; set; }
        public CarMake CarMake { get; set; }
    }
}
