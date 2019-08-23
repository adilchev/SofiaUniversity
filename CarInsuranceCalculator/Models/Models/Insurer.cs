using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Models;
using CarInsuranceCalculator.Models.Models;
using CarInsuranceCalculator.Observer;

namespace CarInsuranceCalculator.Data.Models
{
    public class Insurer:IObserver
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public double MaximumDiscount { get; set; }
       
        public ICollection<InsurerTypeOfInsurance> InsurersTypesOfInsurance{ get; set; } = new List<InsurerTypeOfInsurance>();
       
        public ICollection<InsurerRiskOrBonus> InsurersRisksOrBonuses { get; set; } = new List<InsurerRiskOrBonus>();

       // public Logger Logger = Logger.GetLogger();
        
        public void Update(ISubject subject)
        {                 
            Logger.GetLogger().UpdateMessages.Add($"At {DateTime.Now}, a new Risk was created! Please {this.Name}, fill in your tariff number.");
        }
    }
}
