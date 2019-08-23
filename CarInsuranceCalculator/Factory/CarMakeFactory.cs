using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data.Models;
using CarInsuranceCalculator.Models.Interfaces;

namespace CarInsuranceCalculator.Factory
{
    public class CarMakeFactory : IMotorVehicleMakeFactory
    {
        public IMotorVehicleMake CreateVehicle(IMotorVehicleMake makeInfo)
        {
            var carMake = new CarMake()
            {
                Name = makeInfo.Name,
                Country = makeInfo.Country
            };
            return carMake;
        }
    }
}
