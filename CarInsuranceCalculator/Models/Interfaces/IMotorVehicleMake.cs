using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Models.Interfaces
{
    public interface IMotorVehicleMake
    {
         int Id { get; set; }
        string Name { get; set; }

         string Country { get; set; }
    }
}
