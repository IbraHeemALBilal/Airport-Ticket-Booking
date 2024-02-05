using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal class BookingsPrinter
    {
        public static async Task PrintBookingsListAsync(List<Booking> bookings)
        {
            const int bookingIdOfDeletedBookings = -1;
            Console.WriteLine("----------------------");
            foreach (var booking in bookings)
            {
                if (booking.Id != bookingIdOfDeletedBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.Id}");
                    Console.WriteLine($"Passenger Name: {booking.Passenger.Name}");
                    Console.WriteLine($"Flight Number: {booking.BookedFlight.FlightNumber}");
                    Console.WriteLine($"Flight Destination: {booking.BookedFlight.DestinationCountry}");
                    Console.WriteLine($"Class: {booking.Class}");
                    Console.WriteLine($"Date: {booking.BookingDate}");
                    Console.WriteLine($"Price: {booking.price}");
                    Console.WriteLine("----------------------");
                }
            }
        }
    }
}
