using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class FlightsPrinterFactory : IFlightsPrinterFactory
    {
        public IFlightsPrinter CreateFlightsPrinter(PrinterType type)
        {
            switch (type)
            {
                case PrinterType.ConsolePrinter:
                    return new ConsoleFlightsPrinter();
                case PrinterType.CsvFilePrinter:
                    return new CsvFileFlightsPrinter();
                default:
                    throw new ArgumentException("Invalid printer type.");
            }
        }
    }
}
