// ----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Bradford">
// Copyright (c) 2020 Andrew Bradford
// </copyright>
// ----------------------------------------------------------------------

namespace FlashForgeFileTransfer
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using AdventurerClientDotNet.Core;

    /// <summary>
    /// The main application class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The IP address of the printer.
        /// </summary>
        static string printerIp;

        /// <summary>
        /// The application entrypoint.
        /// </summary>
        /// <param name="args">
        /// The command line args.
        /// </param>
        static void Main(string[] args)
        {
            Console.WriteLine("Enter printer IP address");
            printerIp = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Select command");
                Console.WriteLine("e: Track endstop status");
                Console.WriteLine("t: Track temperature");
                Console.WriteLine("l: Set LED");
                Console.WriteLine("a: Move Extruder");
                Console.WriteLine("p: Print a file");

                var command = Console.ReadKey();
                switch(command.Key)
                {
                    case ConsoleKey.E:
                        TrackEndstopStatus();
                        break;
                    case ConsoleKey.T:
                        TrackTemperature();
                        break;
                    case ConsoleKey.P:
                        PrintFile();
                        break;
                    case ConsoleKey.L:
                        SetPrinterLed();
                        break;
                    case ConsoleKey.A:
                        ChangeAxis();
                        break;
                    default:
                        Console.WriteLine("Unknown Command");
                        break;
                }
            }
        }

        /// <summary>
        /// Changes LED status to ON or OFF.
        /// </summary>
        private static void SetPrinterLed()
        {
            Console.WriteLine("Enter ON(1) or OFF(2)");
            var status = Console.ReadLine();
            Boolean sta = (status == "1");

            Console.WriteLine("Connecting to printer...");
            using var printer = new Printer(printerIp);
            printer.Connect();
            Console.WriteLine("...Connected");
            printer.SetPrinterLed(sta);
            Console.WriteLine("Done");

        }

        /// <summary>
        /// Manually Changes axis
        /// </summary>
        private static void ChangeAxis()
        {
            Console.WriteLine("Enter Axis to Change");
            var axis = Console.ReadLine();

            Console.WriteLine("Enter Steps to move");
            var steps = Console.ReadLine();

            Console.WriteLine("Enter movement speed");
            var speed = Console.ReadLine();

            Console.WriteLine("Connecting to printer...");
            using var printer = new Printer(printerIp);
            printer.Connect();
            Console.WriteLine("...Connected");
            printer.ChangeAxis(axis, steps, speed);
            Console.WriteLine("Done");

        }

        /// <summary>
        /// Transferresagcode file to the printer and starts it printing.
        /// </summary>
        private static void PrintFile()
        {
            Console.WriteLine("Enter path to gcode file");
            var modelPath = Console.ReadLine();

            while (!File.Exists(modelPath))
            {
                // Could not find file to transfer.
                Console.WriteLine("Could not find model at path '{0}'", modelPath);
                Console.WriteLine("Enter path to gcode file");
                modelPath = Console.ReadLine();
            }

            Console.WriteLine("Connecting to printer...");
            using var printer = new Printer(printerIp);
            printer.Connect();
            Console.WriteLine("...Connected");

            // Generate the file name for the printer by changing the extension to .g, as
            // it does not support Cura's .gcode files.
            var fileName = Path.GetFileNameWithoutExtension(modelPath) + ".g";

            Console.WriteLine("Streaming file to printer...");
            printer.StoreFile(modelPath, fileName);
            Console.WriteLine("Done");

            Console.WriteLine("Printing file");
            printer.PrintFile(fileName);
            Console.WriteLine("Print started");
        }

        /// <summary>
        /// Tracks the current endstop until interrupted by the user.
        /// </summary>
        private static void TrackEndstopStatus()
        {
            Console.WriteLine("Connecting to printer...");
            using var printer = new Printer(printerIp);
            printer.Connect();
            Console.WriteLine("...Connected");
            Console.WriteLine("Tracking status. Press q to stop");

            while (true)
            {
                var status = printer.GetPrinterStatus();
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Machine Status:{0}", status.MachineStatus));
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Move Mode:{0}", status.MoveMode));
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Endstop: x={0} y={1} z={2}", status.Endstop.X, status.Endstop.Y, status.Endstop.Z));

                Thread.Sleep(1000);
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Tracks the current temperature until interrupted by the user.
        /// </summary>
        private static void TrackTemperature()
        {
            Console.WriteLine("Connecting to printer...");
            using var printer = new Printer(printerIp);
            printer.Connect();
            Console.WriteLine("...Connected");
            Console.WriteLine("Tracking status. Press q to stop");

            while (true)
            {
                var temp = printer.GetPrinterTemperature();
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Extruder: {0}", temp.ExtruderTemperature));
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Build Plate: {0}", temp.BuildPlateTemperature));

                Thread.Sleep(1000);
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q)
                {
                    return;
                }
            }
        }
    }
}
