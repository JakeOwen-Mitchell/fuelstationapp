using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Petrol_Station_Simulator
{
    public class Counters
    {
        private int _carsServiced = 0;

        public void IncreaseCarsServiced()
        {
            _carsServiced++;
        }

        public int GetCarsServiced()
        {

            return _carsServiced;
        }

    }
}
