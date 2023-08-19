using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal class ConsoleFlightsPrinter : IFlightsPrinter
    {
        public void PrintFlights(List<Flight> flightsToPrint)
        {
            Console.WriteLine("----------------------");
            foreach (var flight in flightsToPrint)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                Console.WriteLine($"Departure Country: {flight.DepartureCountry}");
                Console.WriteLine($"Destination Country: {flight.DestinationCountry}");
                Console.WriteLine($"Departure Airport: {flight.DepartureAirport}");
                Console.WriteLine($"Arrival Airport: {flight.ArrivalAirport}");
                Console.WriteLine($"Departure Date: {flight.DepartureDate}");
                Console.WriteLine($"Economy Class Price: {flight.EconomyClassPrice}");
                Console.WriteLine($"Business Class Price: {flight.BusinessClassPrice}");
                Console.WriteLine($"First Class Price: {flight.FirstClassPrice}");
                Console.WriteLine("------------------------------");
            }
        }// print list of flights
    }
}
