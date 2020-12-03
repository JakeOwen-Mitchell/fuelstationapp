using System;
using System.Collections.Generic;
using System.Text;

namespace MOD005424_Assignment
{
    public class Counters
    {
        private int carsServiced = 0;

        public void IncreaseCarsServiced()
        {
            carsServiced++;
        }

        public int GetCarsServiced()
        {

            return carsServiced;
        }

    }
}
