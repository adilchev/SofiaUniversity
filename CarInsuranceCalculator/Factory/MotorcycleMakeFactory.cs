using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Models.Interfaces;
using CarInsuranceCalculator.Models.Models;

namespace CarInsuranceCalculator.Factory
{
    public class MotorcycleMakeFactory : IMotorVehicleMakeFactory
    {
        public IMotorVehicleMake CreateVehicle(IMotorVehicleMake makeInfo)
        {
            var motorcycleMake = new MotorcycleMake()
            {
                Name = makeInfo.Name,
                Country = makeInfo.Country
            };
            return motorcycleMake;
        }
    }
}
