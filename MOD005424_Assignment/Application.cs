using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Petrol_Station_Simulator
{
    public class Application
    {
        
        

        public static void Run()
        {
            //initialise list of vehicle objects to be used for vehicle queue
            List<Vehicle> vehicles = new List<Vehicle>();

            //create 3 lanes for 9 pump stations
            Lane lane1 = new Lane();
            Lane lane2 = new Lane();
            Lane lane3 = new Lane();
            GUI gui = new GUI();
            Counters counters = new Counters();

            //initialise counter variables
            int carTimerCounter = 0;
            int carsLeft = 0;
            float totalLitresDispensed = 0f;
            //float placeholderTotalLitresDispensed = 0f;
            float pricePerLitre = 1.15f;

            //setup pay variables
            DateTime loginTime = DateTime.Now; //Established on program initialisation so we can calculate pay upon logout
            float payPerMinute = 8.95f / 60; //Establish pay per minute, makes it easier to calculate pay later on. 

            bool programRun = true;

                Random rng = new Random();

            GUI.LoadGui(lane1, lane2, lane3, vehicles,carsLeft,counters,totalLitresDispensed);

            
            //start program loop
            while (programRun)
            {
                Thread.Sleep(100);
                
                //Checks for user input to allow user to select a pump
                if (Console.KeyAvailable)
                {
                    programRun = CheckKeyPress(lane1, lane2, lane3, vehicles,carsLeft,counters,totalLitresDispensed,programRun);
                }

                //Uses a random number generator to add a vehicle to vehicles list between 1.5-2.5 seconds
                //Also uses this opportunity to update the number of litres that have been filled on all pumps.
                carTimerCounter++;
                if (carTimerCounter > rng.Next(15,25))
                {
                    carTimerCounter = 0;
                    vehicles.Add(new Vehicle());
                    totalLitresDispensed = 0;
                    totalLitresDispensed =
                        UpdateTotalLitres(lane1, totalLitresDispensed) +
                        UpdateTotalLitres(lane2, totalLitresDispensed) +
                        UpdateTotalLitres(lane3, totalLitresDispensed);
                    GUI.LoadGui(lane1, lane2, lane3, vehicles, carsLeft, counters, totalLitresDispensed);
                }

                //Runs through every vehicle in list, 
                for (int i = 0;i < vehicles.Count;i++)
                {
                    //Checks how long a vehicle has been waiting. Always adds 1 to the wait time (1 = 100ms) and if it is over 25 (2500ms) then
                    //Set internal variable _waitTimeExpired to true
                    vehicles[i].CheckWaitTime();

                    //Checks to see if _waitTimeExpires is true. If it is, removes the vehicle and plays an audio beep to alert user that a vehicle has left the forecourt.
                    if (vehicles[i].VehicleExit())
                    {
                        vehicles.RemoveAt(i);
                        Console.Clear();
                        GUI.LoadGui(lane1, lane2, lane3, vehicles, carsLeft, counters, totalLitresDispensed);
                        Console.Beep();
                        carsLeft++;
                    }
                }
                
                //Check if any of the vehicles have been filled based on a timer
                FillVehicle(lane1, counters);
                FillVehicle(lane2, counters);
                FillVehicle(lane3, counters);


            }

            //On logout
            //Calculate the total time logged in
            DateTime logoutTime = DateTime.Now;
            TimeSpan timeLoggedIn = logoutTime - loginTime;

            //Calculate Bonus
            float bonus = (pricePerLitre * totalLitresDispensed) / 100;

            //Convert logged in hours in to minutes so pay calculation is easier later on
            var timeLoggedInHours = Convert.ToInt32(timeLoggedIn.Hours * 60);
            var timeLoggedInMinutes = Convert.ToInt32(timeLoggedIn.Minutes);

            //Add together hours and minutes (e.g 2 hours and 5 minutes should be 125) and times it by the pay per minute
            float totalPay = payPerMinute * (timeLoggedInMinutes + timeLoggedInHours) + bonus;

            //Present info to user
            Console.Clear();
            Console.WriteLine("Logging out of system");
            Console.WriteLine($"You were logged on to the system for {timeLoggedInHours} hours,{timeLoggedIn.Minutes} minutes. ");
            Console.WriteLine($"You dispensed a total of {Math.Round(totalLitresDispensed)} litres");
            Console.WriteLine($"At £{Math.Round(payPerMinute*60,2)} per hour with a £{Math.Round(bonus,2)} bonus you have earned £{Math.Round(totalPay,2)}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        private static float UpdateTotalLitres(Lane lane, float ftotalLitresDispensed)
        {
            foreach (Pump pumps in lane.Pump)
            {
                ftotalLitresDispensed += pumps.TotalFuelDelivered();
            }

            return ftotalLitresDispensed;
        }

        private static void FillVehicle(Lane fLane, Counters counters)
        {
            //Check each pump, if it is occupied, then run a method that wil return true when a vehicle has been filled.
            foreach (Pump pump in fLane.Pump)
            {
                if (pump.CheckOccupancy())
                {
                    pump.CheckIfVehicleFilled();

                    if (pump.CheckIfVehicleFilled())
                    {
                        counters.IncreaseCarsServiced();
                    }
                }
            }
        }
        //Checks for key press, if one is selected, loads pump select method and reloads GUI
        private static bool CheckKeyPress(Lane lane1, Lane lane2, Lane lane3, List<Vehicle> vehicle, int fcarsLeft, Counters counters, float totalLitresDispensed, bool fProgramRun)
        {
            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            //Lane 1
            if (keyPress.Key == ConsoleKey.D1 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad1 & vehicle.Count > 0)
            {
                PumpSelect(0,lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters,totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D2 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad2 & vehicle.Count > 0)
            {
                PumpSelect(1, lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D3 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad3 & vehicle.Count > 0)
            {
                PumpSelect(2, lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            //Lane 2
            if (keyPress.Key == ConsoleKey.D4 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad4 & vehicle.Count > 0)
            {
                PumpSelect(0, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D5 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad5 & vehicle.Count > 0)
            {
                PumpSelect(1, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D6 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad6 & vehicle.Count > 0)
            {
                PumpSelect(2, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            //Lane 3
            if (keyPress.Key == ConsoleKey.D7 & vehicle.Count > 0  || keyPress.Key == ConsoleKey.NumPad7 & vehicle.Count > 0)
            {
                PumpSelect(0, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D8 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad8 & vehicle.Count > 0)
            {
                PumpSelect(1, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.D9 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad9 & vehicle.Count > 0)
            {
                PumpSelect(2, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle, fcarsLeft, counters, totalLitresDispensed);
            }

            if (keyPress.Key == ConsoleKey.Escape)
            {
                fProgramRun = false;
                return fProgramRun;
            }

            fProgramRun = true;
            return fProgramRun;
        }

        //Initially checks if pump is occupied, if it is, display a warning. If it is not, find a vehicle that has _isfuelling set to false
        public static void PumpSelect(int stationNo,Lane fLane, List<Vehicle> vehicle)
        {
            if (!fLane.Pump[stationNo].CheckOccupancy())
            {
                fLane.Pump[stationNo].ChangeOccupancy();
                for (int i = 0; i < vehicle.Count;i++)
                {
                    //If vehicle._isfuelling is set to false, it will send that vehicle to a new list in Pump and then remove it from the list in Application
                    if (!vehicle[i].CheckIfVehicleFueling())
                    {
                        {
                            vehicle[i].SetVehicleToFueling();
                            fLane.Pump[stationNo].SendToPump(vehicle[i]);
                            vehicle.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else
            {
                PumpTaken();
            }
        }

        private static void PumpTaken()
        {
            Console.SetCursorPosition(0, 11);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Pump is already taken!");
            Console.BackgroundColor = ConsoleColor.Black;
            Thread.Sleep(1500);
        }

    }
}
