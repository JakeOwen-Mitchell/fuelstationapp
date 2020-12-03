using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace MOD005424_Assignment
{
    public class Vehicle
    {
        private bool _isFueling;
        private bool _waitTimeExpired;
        private int _waitTimeCounter;
        public float LitresInCar {get set};

        public Vehicle()
        {
            _isFueling = false;
            _waitTimeExpired = false;
            _waitTimeCounter = 0;
            LitresInCar = 13.5f;
        }

        public bool CheckIfVehicleFueling()
        {
            return _isFueling;
        }

        public bool SetVehicleToFueling()
        {
            _isFueling = true;
            return _isFueling;
        }
        public void CheckWaitTime()
        {
            _waitTimeCounter += 1;
            if (_waitTimeCounter > 250 && _isFueling == false)
            {
                _waitTimeExpired = true;
            }
        }

        public bool VehicleExit()
        {
            return _waitTimeExpired;
        }

    }

    }
