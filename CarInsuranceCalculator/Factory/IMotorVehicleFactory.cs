using CarInsuranceCalculator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Factory
{
    public interface IMotorVehicleMakeFactory
    {
        IMotorVehicleMake CreateVehicle(IMotorVehicleMake makeInfo);
    }
}
