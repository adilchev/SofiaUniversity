using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Models.Models;


namespace CarInsuranceCalculator.Data.Models
{
    public class InfoForInsurance
    {
        public int Id { get; set; }

        [Required]
        public int? TypeOfInsuranceId { get; set; }
        public TypeOfInsurance TypeOfInsurance { get; set; }

        [Required]
        public int? CarMakeId { get; set; }
        public CarMake CarMake { get; set; }

        [Required]
        public int? CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        [Required]
        public int? RegionId { get; set; }
        public Region Region { get; set; }
        
        public int? CarInsurerId { get; set; }
        [ForeignKey("CarInsurerId")]
        public Insurer CarInsurer { get; set; }
        
        public int? MTPLInsurerId { get; set; }
        [ForeignKey("MTPLInsurerId")]
        public Insurer MTPLInsurer { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int CarAge { get; set; }

        [Required]
        public bool RightSideWheel { get; set; } = false;

        [Required]
        public int OwnerAge { get; set; }

        [Required]
        public bool NewImport { get; set; } = false;

        [Required]
        [Range(0,double.MaxValue)]
        public int Value { get; set; }

        [Required]
        public NumberOfPayments NumberOfPayments { get; set; }
    }
}
