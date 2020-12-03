using System;
using System.Collections.Generic;
using System.Text;

namespace MOD005424_Assignment
{
    public class Pump
    {
        private bool _isOccupied;
        private Vehicle _vehicleAtPump;
        private int _fuelTimer = 0;
        private float litresDispensedPerMS = 0.0015f;
        public float TotalLitresDispensed {get set}

        public Pump()
        {
            _isOccupied = false;
        }

        public bool CheckOccupancy()
        {
            bool IsOccupied = _isOccupied;
            return IsOccupied;
        }

        public void ChangeOccupancy()
        {
            if (this._isOccupied == false)
                this._isOccupied = true;
            else if (this._isOccupied == true)
                this._isOccupied = false;
        }

        public Vehicle SendToPump(Vehicle fVehicleAtPump)
        {
            this._vehicleAtPump = fVehicleAtPump;
            return fVehicleAtPump;
        }

        public bool CheckIfVehicleFilled()
        {
            bool VehicleIsFilled = false;
            _fuelTimer++;
            _vehicleAtPump.LitresInCar += litresDispensedPerMS;

            if (_fuelTimer == 180)
            {
                _fuelTimer = 0;
                TotalLitresDispensed += _vehicleAtPump.LitresInCar;
                _vehicleAtPump = null;
                VehicleIsFilled = true;
                ChangeOccupancy();
                return VehicleIsFilled;
            }

            return VehicleIsFilled;

        }
    }
}
