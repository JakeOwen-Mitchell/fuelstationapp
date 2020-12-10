using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petrol_Station_Simulator;

namespace PetrolStationTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestOne()
        {
            Vehicle vehicle = new Vehicle();

            for (int i = 0; i <= 250; i++)
            {
                vehicle.CheckWaitTime();
            }

            bool testAssert = vehicle.VehicleExit();

            Assert.IsTrue(testAssert, "CheckWaitTime() not correctly adding +1 to _waitTimeCounter with each run");
        }
        [TestMethod]
        public void TestPumpCreation()
        {
            //Test fuel pump creation
            Lane lane1 = new Lane();

            Assert.AreSame(lane1.Pump[0],lane1.Pump[0],"Pump not created!");
        }
    }
}
