using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal static class FlightsManager
    {
        public static List<Flight> AllFlights = new List<Flight>();
        public static List<Flight> SearchFlights(string? departureCountry, string? destinationCountry, string? departureDate, string? departureAirport, string? arrivalAirport)
        {
            var searchedFlights = AllFlights.Where(flight =>
                (string.IsNullOrEmpty(departureCountry) || flight.DepartureCountry == departureCountry) &&
                (string.IsNullOrEmpty(destinationCountry) || flight.DestinationCountry == destinationCountry) &&
                (string.IsNullOrEmpty(departureDate) || flight.DepartureDate == departureDate) &&
                (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport) &&
                (string.IsNullOrEmpty(arrivalAirport) || flight.ArrivalAirport == arrivalAirport)).ToList();

            return searchedFlights;
        }
    }
}
