using CarInsuranceCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Builder
{
    public class CarModelDirector
    {
        private ICarModelBuilder carModelBuilder;

        public ICarModelBuilder CarModelBuilder { set { this.carModelBuilder = value; } }


        public void BuildMinimumCarModelInfo(CarModel carModelInfo)
        {
            this.carModelBuilder.AddCarMakeId(carModelInfo);
            this.carModelBuilder.AddName(carModelInfo);
            this.carModelBuilder.AddProductionYear(carModelInfo);
            this.carModelBuilder.AddPrice(carModelInfo);
        }

        public void BuildCarModelWithAllInfo(CarModel carModelInfo)
        {
            this.carModelBuilder.AddCarMakeId(carModelInfo);
            this.carModelBuilder.AddName(carModelInfo);
            this.carModelBuilder.AddProductionYear(carModelInfo);
            this.carModelBuilder.AddPrice(carModelInfo);
            this.carModelBuilder.AddEngineCapacity(carModelInfo);
            this.carModelBuilder.AddHorsepower(carModelInfo);
            this.carModelBuilder.AddSpecialModel(carModelInfo);
        }

}
}
