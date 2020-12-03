using System;
using System.Collections.Generic;
using System.Text;

namespace MOD005424_Assignment
{
    public class Lane
    {
        public List<Pump> Pump = new List<Pump>();

        public Lane()
        {
            CreatePumps();
        }

        private void CreatePumps()
        {
            for (int i = 0; i < 3; i++)
            {
                Pump.Add(new Pump());
            }

        }
    }
}
