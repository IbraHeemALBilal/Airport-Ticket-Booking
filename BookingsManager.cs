using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace AirportTicketBooking
{
    internal class BookingsManager
    {
        public static List<Booking> AllBookings = new List <Booking> ();
        public static async Task UpdateBookingsListAsync()
        {
                AllBookings.Clear();
                await BookingsFileOperations.ReadBookingsFromFileAsync();
        }// to update the bookings
        public static List<Booking> FilterBookings(string? departureCountry, string? destinationCountry, string? departureDate, string? departureAirport, string? arrivalAirport, FlightClassType? classType)
        {
            var filteredBookings = AllBookings.Where(booking =>
                (string.IsNullOrEmpty(departureCountry) || booking.BookedFlight.DepartureCountry == departureCountry) &&
                (string.IsNullOrEmpty(destinationCountry) || booking.BookedFlight.DestinationCountry == destinationCountry) &&
                (string.IsNullOrEmpty(departureDate) || booking.BookedFlight.DepartureDate == departureDate) &&
                (string.IsNullOrEmpty(departureAirport) || booking.BookedFlight.DepartureAirport == departureAirport) &&
                (string.IsNullOrEmpty(arrivalAirport) || booking.BookedFlight.ArrivalAirport == arrivalAirport) &&
                (!classType.HasValue || booking.Class == classType)).ToList();

            return filteredBookings;
        }//for filter the bookings
    }
}
