using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarInsuranceCalculator.Observer
{
    public class Logger
    {
        private static object myLock = new object();
        private static volatile Logger instance;

        public List<string> UpdateMessages { get; set; } = new List<string>();


        private Logger() { }

      

        public static Logger GetLogger()
        {
            if (instance == null)
            {
                lock (myLock)
                {
                    if (instance==null)
                    {
                        instance = new Logger();
                    }
                }
              
            }
            return instance;
        }
    }
}
