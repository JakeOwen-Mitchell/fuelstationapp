using System;
using System.Collections.Generic;
using System.Text;

namespace Petrol_Station_Simulator
{
    public class Lane
    {
        public List<Pump> Pump = new List<Pump>();

        public Lane()
        {
            CreatePumps();
        }
        /// <summary>
        /// Creates 3 pumps when a lane is created. Expected to have Lane 1, 2 and 3 for all 9 pumps.
        /// </summary>
        private void CreatePumps()
        {
            for (int i = 0; i < 3; i++)
            {
                Pump.Add(new Pump());
            }

        }
    }
}
