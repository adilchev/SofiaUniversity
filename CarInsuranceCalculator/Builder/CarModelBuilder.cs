using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Builder
{
    public class CarModelBuilder : ICarModelBuilder
    {
        private CarModel carModel;

        public CarModelBuilder()
        {
            this.Reset();
        }



        public void AddCarMakeId(CarModel carModelInfo)
        {
            carModel.CarMakeId = carModelInfo.CarMakeId;
        }

        public void AddEngineCapacity(CarModel carModelInfo)
        {
            carModel.EngineCapacity = carModelInfo.EngineCapacity;
        }

        public void AddHorsepower(CarModel carModelInfo)
        {
            carModel.Horsepower = carModelInfo.Horsepower;
        }

        public void AddName(CarModel carModelInfo)
        {
            carModel.Name = carModelInfo.Name;
        }

        public void AddPrice(CarModel carModelInfo)
        {
            carModel.Price = carModelInfo.Price;
        }

        public void AddProductionYear(CarModel carModelInfo)
        {
            carModel.ProductionYear = carModelInfo.ProductionYear;
        }

        public void AddSpecialModel(CarModel carModelInfo)
        {
            carModel.SpecialModel = carModelInfo.SpecialModel;
        }

        public void Reset()
        {
            this.carModel = new CarModel();
        }

        public CarModel GetCarModel()
        {
            CarModel result = this.carModel;
            this.Reset();

            return result;
        }
    }
}
