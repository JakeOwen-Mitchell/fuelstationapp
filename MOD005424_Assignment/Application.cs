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
            List<Vehicle> vehicles = new List<Vehicle>();

            Lane lane1 = new Lane();
            Lane lane2 = new Lane();
            Lane lane3 = new Lane();
            GUI gui = new GUI();
            Counters counters = new Counters();

            int carTimerCounter = 0;
            int carsLeft = 0;

            Random rng = new Random();

            GUI.LoadGui(lane1, lane2, lane3, vehicles,carsLeft,counters);

            

            while (true)
            {
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    CheckKeyPress(lane1, lane2, lane3, vehicles,carsLeft,counters);
                }


                carTimerCounter++;
                if (carTimerCounter > rng.Next(15,25))
                {
                    carTimerCounter = 0;
                    vehicles.Add(new Vehicle());
                    GUI.LoadGui(lane1,lane2,lane3,vehicles,carsLeft,counters);
                }


                for (int i = 0;i < vehicles.Count;i++)
                {
                    vehicles[i].CheckWaitTime();

                    if (vehicles[i].VehicleExit())
                    {
                        vehicles.RemoveAt(i);
                        Console.Clear();
                        GUI.LoadGui(lane1, lane2, lane3, vehicles,carsLeft,counters);
                        Console.Beep();
                        carsLeft++;
                    }
                }

                FillVehicle(lane1, counters);
                FillVehicle(lane2, counters);
                FillVehicle(lane3, counters);
            }

        }

        private static void FillVehicle(Lane fLane, Counters counters)
        {
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

        private static void CheckKeyPress(Lane lane1, Lane lane2, Lane lane3, List<Vehicle> vehicle, int fcarsLeft, Counters counters)
        {
            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            //Lane 1
            if (keyPress.Key == ConsoleKey.D1 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad1 & vehicle.Count > 0)
            {
                PumpSelect(0,lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D2 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad2 & vehicle.Count > 0)
            {
                PumpSelect(1, lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D3 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad3 & vehicle.Count > 0)
            {
                PumpSelect(2, lane1, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            //Lane 2
            if (keyPress.Key == ConsoleKey.D4 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad4 & vehicle.Count > 0)
            {
                PumpSelect(0, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D5 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad5 & vehicle.Count > 0)
            {
                PumpSelect(1, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D6 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad6 & vehicle.Count > 0)
            {
                PumpSelect(2, lane2, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            //Lane 3
            if (keyPress.Key == ConsoleKey.D7 & vehicle.Count > 0  || keyPress.Key == ConsoleKey.NumPad7 & vehicle.Count > 0)
            {
                PumpSelect(0, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D8 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad8 & vehicle.Count > 0)
            {
                PumpSelect(1, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }

            if (keyPress.Key == ConsoleKey.D9 & vehicle.Count > 0 || keyPress.Key == ConsoleKey.NumPad9 & vehicle.Count > 0)
            {
                PumpSelect(2, lane3, vehicle);
                GUI.LoadGui(lane1, lane2, lane3, vehicle,fcarsLeft,counters);
            }
        }

        private static void PumpSelect(int stationNo,Lane fLane, List<Vehicle> vehicle)
        {
            if (!fLane.Pump[stationNo].CheckOccupancy())
            {
                fLane.Pump[stationNo].ChangeOccupancy();
                for (int i = 0; i < vehicle.Count;i++)
                {
                    if (!vehicle[i].CheckIfVehicleFueling())
                    {
                        {
                            vehicle[i].SetVehicleToFueling();
                            fLane.Pump[0].SendToPump(vehicle[i]);
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
