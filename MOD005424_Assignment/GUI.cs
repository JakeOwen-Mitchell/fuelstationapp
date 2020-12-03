using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;

namespace MOD005424_Assignment
{
    public class GUI
    {
        /// <summary>
        /// Calls the GUI to be loaded. Has to take most variables to present them on screen.
        /// </summary>
        /// <param name="lane1"></param>
        /// <param name="lane2"></param>
        /// <param name="lane3"></param>
        /// <param name="vehicleQueue"></param>
        /// <param name="carsLeft"></param>
        public static void LoadGui(Lane lane1, Lane lane2, Lane lane3, List<Vehicle> vehicleQueue, int carsLeft, Counters counters)
        {
            Console.Clear();
            LaneAndDetails(lane1, lane2, lane3,vehicleQueue,carsLeft,counters);
            TextInfo(lane1, lane2, lane3);
            Console.Write("Type a pump number to send a car: ");
        }
        /// <summary>
        /// Displays what free pumps are currently available by running FreePumps method
        /// </summary>
        /// <param name="lane1"></param>
        /// <param name="lane2"></param>
        /// <param name="lane3"></param>
        private static void TextInfo(Lane lane1, Lane lane2, Lane lane3)
        {
            
            Console.SetCursorPosition(0,8);
            Console.Write("Pump: ");
            FreePumps(lane1, 1);
            FreePumps(lane2, 4);
            FreePumps(lane3, 7);
            Console.WriteLine("are currently available");
        }
        /// <summary>
        /// Main GUI builder. Prints most of the structure using stringbuilder
        /// </summary>
        /// <param name="lane1"></param>
        /// <param name="lane2"></param>
        /// <param name="lane3"></param>
        /// <param name="vehicleQueue"></param>
        /// <param name="carsLeft"></param>
        private static void LaneAndDetails(Lane lane1, Lane lane2, Lane lane3, List<Vehicle> vehicleQueue, int carsLeft, Counters counters)
        {

            string whitespace = "   ";

            //Line 0
            GuiBuilder(true, 12, false);
            Console.Write("LANE ONE");
            GuiBuilder(false, 11, true);
            Console.Write(whitespace);
            GuiBuilder(true, 11, false);
            Console.Write("DETAILS");
            GuiBuilder(false, 10, true);
            Console.WriteLine();

            //Line 1
            PrintLanes(lane1, 1);
            Console.Write(whitespace);
            GuiBuilder(true, 0, false);
            Console.Write(whitespace);
            Console.Write("Jake"); //May eventually show logged in user.
            Console.SetCursorPosition(65, 1);
            GuiBuilder(true, 0, false);
            Console.WriteLine();

            //Line 2
            GuiBuilder(true, 12, false);
            Console.Write("LANE TWO");
            GuiBuilder(false, 11, true);
            Console.Write(whitespace);
            GuiBuilder(true, 0, false);
            Console.Write(whitespace);
            Console.Write("CARS WAITING:");
            Console.SetCursorPosition(59, 2);
            Console.Write(vehicleQueue.Count); //Will eventually show length of cars list
            Console.SetCursorPosition(65, 2);
            GuiBuilder(true, 0, false);
            Console.WriteLine();

            //Line 3
            PrintLanes(lane2, 4);
            Console.Write(whitespace);
            GuiBuilder(true, 0, false);
            Console.Write(whitespace);
            Console.Write("CARS SERVICED:");
            Console.SetCursorPosition(59, 3);
            Console.Write(counters.GetCarsServiced()); //Will eventually show number of cars serviced
            Console.SetCursorPosition(65, 3);
            GuiBuilder(true, 0, false);
            Console.WriteLine();

            //Line 4
            GuiBuilder(true, 11, false);
            Console.Write("LANE THREE");
            GuiBuilder(false, 10, true);
            Console.Write(whitespace);
            GuiBuilder(true, 0, false);
            Console.Write(whitespace);
            Console.Write("CARS LEFT:");
            Console.SetCursorPosition(59, 4);
            Console.Write(carsLeft);
            Console.SetCursorPosition(65, 4);
            GuiBuilder(true, 0, false);
            Console.WriteLine();


            //Line 5
            PrintLanes(lane3, 7);
            Console.Write(whitespace);
            GuiBuilder(true, 0, false);
            Console.Write(whitespace);
            Console.Write("Litres Dispensed:");
            Console.SetCursorPosition(59, 5);
            Console.Write("323"); //Will eventually show number of cars serviced
            Console.SetCursorPosition(65, 5);
            GuiBuilder(true, 0, false);
            Console.WriteLine();

            //Line 6
            GuiBuilder(true, 31, true);
            Console.Write(whitespace);
            GuiBuilder(true, 28, true);
        }
        /// <summary>
        /// Checks if lane is occupied using station.CheckOccupancy. Displays pump as red if occupied
        /// And green if not occupied.
        /// </summary>
        /// <param name="fLane"></param>
        /// <param name="firstStationNumber"></param>
        private static void PrintLanes(Lane fLane, int firstStationNumber)
        {
            
            GuiBuilder(true, 4, false);
            for (int i = 0; i < 3; i++)
            {
                if (fLane.Pump[i].CheckOccupancy())
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write($"PUMP{i +firstStationNumber}");
                }
                else if (!fLane.Pump[i].CheckOccupancy())
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"PUMP{i + firstStationNumber}");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                GuiBuilder(4);
            }

            GuiBuilder(false, 0, true);
        }

        /// <summary>
        /// Type in a number of hyphens to add after current cursor position
        /// </summary>
        /// <param name="dashes"></param>
        static void GuiBuilder(int dashes)
        {
            StringBuilder guiBuilder = new StringBuilder();
            guiBuilder.Append('-', dashes);
            Console.Write(guiBuilder);
        }
        /// <summary>
        /// First argument is a boolean to add a pipe before string (T/F)
        /// Second argument is number of hyphens to show (Integer)
        /// Third argument is a boolean to add a pipe after string (T/F)
        /// </summary>
        /// <param name="pipeBefore"></param>
        /// <param name="dashes"></param>
        /// <param name="pipeAfter"></param>
        static void GuiBuilder(bool pipeBefore, int dashes, bool pipeAfter)
        {
            StringBuilder guiBuilder = new StringBuilder();

            if (pipeBefore)
                guiBuilder.Append('|', 1);
            guiBuilder.Append('-', dashes);
            if (pipeAfter)
                guiBuilder.Append('|', 1);

            Console.Write(guiBuilder);
        }
        static void FreePumps(Lane flane, int firstStationNumber)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!flane.Pump[i].CheckOccupancy())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"({i + firstStationNumber}) ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}