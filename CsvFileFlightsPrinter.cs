using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class CsvFileFlightsPrinter : IFlightsPrinter
    {
        public void PrintFlights(List<Flight> flights)
        {
            foreach (Flight flight in flights)
            {
                string csvFilePath = "PrintFlights.csv";
                string csvLine = $"{flight.FlightNumber},{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate},{flight.EconomyClassPrice},{flight.BusinessClassPrice},{flight.FirstClassPrice}";
                File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
            }
        }
    }
}
