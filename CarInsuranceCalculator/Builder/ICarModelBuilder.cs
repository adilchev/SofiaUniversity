using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Builder
{
    public interface ICarModelBuilder
    {


        void AddName(CarModel carModelInfo);

        void AddHorsepower(CarModel carModelInfo);

        void AddEngineCapacity(CarModel carModelInfo);

        void AddProductionYear(CarModel carModelInfo);

        void AddPrice(CarModel carModelInfo);

        void AddSpecialModel(CarModel carModelInfo);

        void AddCarMakeId(CarModel carModelInfo);

        CarModel GetCarModel();
    }
}
