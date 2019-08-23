using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}
