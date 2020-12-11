using System;
using System.Collections.Generic;
using System.Text;

namespace Petrol_Station_Simulator
{
    public class Pump
    {
        private bool _isOccupied;
        private Vehicle _vehicleAtPump;
        private int _fuelTimer = 0;
        private readonly int _timeToFillVehicle = 180;
        private readonly float _fuelDeliverPerHundredMS = 0.15f;
        private float _totalFuelDelivered = 0f;

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
	
	//This method adds 1 to the fuel timer each time it is run (100ms)
	//Once the fueltimer reaches the amount required to fill vehicle
	//Update total fuel delivered to the pump, reset fuel timer
	//remove the vehicle at the pump, set the return variable to true
	//And change occupancy of the pump back to free
	
        public bool CheckIfVehicleFilled()
        {
            bool VehicleIsFilled = false;
            _fuelTimer++;

            if (_fuelTimer >= _timeToFillVehicle)
            {
                _totalFuelDelivered += (_fuelDeliverPerHundredMS * _timeToFillVehicle) ; //1.5 litres per second * time to fill (18s) = 27
                _fuelTimer = 0;
                _vehicleAtPump = null;
                VehicleIsFilled = true;
                ChangeOccupancy();
                return VehicleIsFilled;
            }

            return VehicleIsFilled;

        }

        public float TotalFuelDelivered()
        {
            float totalFuel = _totalFuelDelivered;
            return totalFuel;
        }
    }
}
