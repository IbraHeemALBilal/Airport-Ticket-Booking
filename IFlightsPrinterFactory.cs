using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal interface IFlightsPrinterFactory
    {
        IFlightsPrinter CreateFlightsPrinter(PrinterType type);
    }
}
