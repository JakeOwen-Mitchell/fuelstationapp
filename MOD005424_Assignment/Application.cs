using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace MOD005424_Assignment
{
    public class Application
    {
        private DateTime login = DateTime.Now;
        

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


            int carTimerCounter = 0;
            int carsLeft = 0;
            float totalLitresDispensed = 0f;

                Random rng = new Random();

            GUI.LoadGui(lane1, lane2, lane3, vehicles,carsLeft,counters,totalLitresDispensed);

            
            //start program loop
            while (true)
            {
                Thread.Sleep(100);
                
                //Checks for user input to allow user to select a pump
                if (Console.KeyAvailable)
                {
                    CheckKeyPress(lane1, lane2, lane3, vehicles,carsLeft,counters,totalLitresDispensed);
                }

                //Uses a random number generator to add a vehicle to vehicles list between 1.5-2.5 seconds
                carTimerCounter++;
                if (carTimerCounter > rng.Next(15,25))
                {
                    carTimerCounter = 0;
                    vehicles.Add(new Vehicle());
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

                //Update TotalLitres
                totalLitresDispensed = UpdateTotalLitres(lane1, totalLitresDispensed);
                totalLitresDispensed = UpdateTotalLitres(lane2, totalLitresDispensed);
                totalLitresDispensed = UpdateTotalLitres(lane3, totalLitresDispensed);
            }

        }

        private static float UpdateTotalLitres(Lane lane, float totalLitresDispensed)
        {
            foreach (Pump pumps in lane.Pump)
            {
                //No idea
            }

            return totalLitresDispensed;
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
        private static void CheckKeyPress(Lane lane1, Lane lane2, Lane lane3, List<Vehicle> vehicle, int fcarsLeft, Counters counters, float totalLitresDispensed)
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
        }

        //Initially checks if pump is occupied, if it is, display a warning. If it is not, find a vehicle that has _isfuelling set to false
        private static void PumpSelect(int stationNo,Lane fLane, List<Vehicle> vehicle)
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
