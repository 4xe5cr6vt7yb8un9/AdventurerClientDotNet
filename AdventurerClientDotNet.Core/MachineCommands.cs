// ----------------------------------------------------------------------
// <copyright file="FlashForgeFileTransfer.cs" company="Andy Bradford">
// Copyright (c) 2020 Andrew Bradford
// </copyright>
// ----------------------------------------------------------------------

namespace AdventurerClientDotNet.Core
{
    /// <summary>
    /// Commands that can be sent to the printer.
    /// </summary>
    public static class MachineCommands
    {
        /// <summary>
        /// The Get endstop status command.
        /// </summary>
        public const string GetEndstopStaus = "M119";
        
        /// <summary>
        /// The begin write to SD card command.
        /// </summary>
        public const string BeginWriteToSdCard = "M28";

        /// <summary>
        /// The end write to SD card command.
        /// </summary>
        public const string EndWriteToSdCard = "M29";

        /// <summary>
        /// The print file from SD card command.
        /// </summary>
        public const string PrintFileFromSd = "M23";

        /// <summary>
        /// The get firmware version command.
        /// </summary>
        public const string GetFirmwareVersion = "M115";

        /// <summary>
        /// The get Temperature version command.
        /// </summary>
        public const string GetTemperature = "M105";

        /// <summary>
        /// Turns the LED on or off.
        /// </summary>
        public const string ChangeLed = "M146";

        /// <summary>
        /// Begins Command to manually change axis.
        /// </summary>
        public const string StartAxisChange = "M114";

        /// <summary>
        /// Sends Commands to manually change axis.
        /// </summary>
        public const string SendAxisCommands = "G1";

        /// <summary>
        /// Ends command to manually change axis.
        /// </summary>
        public const string EndAxisChange = "M112";

        /// <summary>
        /// Sets the extruders temperature.
        /// </summary>
        public const string SetExtruderTemp = "M104";

        /// <summary>
        /// Sets the platform temperature.
        /// </summary>
        public const string SetPlatformTemp = "M140";
    }
}
