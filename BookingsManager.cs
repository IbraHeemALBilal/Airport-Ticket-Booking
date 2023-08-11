using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace AirportTicketBooking
{
    internal static class BookingsManager
    {
        public static List<Booking> Bookings = new List<Booking>();
        public static async Task ReadBookingsFromFile()
        {
            string csvFilePath = "bookings.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    int bookingId = int.Parse(fields[0]);
                    string passengerName = fields[1];
                    int flightNumber = int.Parse(fields[2]);
                    FlightClassType classType = Enum.Parse<FlightClassType>(fields[3]);
                    Passenger passenger = PassengersManager.Passengers.FirstOrDefault(p => p.Name == passengerName);
                    Flight flight = FlightsManager.Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

                    if (passenger != null && flight != null)
                    {
                        var booking = new Booking(bookingId, passenger, flight, classType);
                        Bookings.Add(booking);
                    }
                }
            }
        }// to read bookings data from file
        public static async Task PrintBookingsList(List<Booking> bookings)
        {
            if (bookings == Bookings)
            {
                bookings.Clear();
                await ReadBookingsFromFile();
            }
            const int bookingIdOfDeletedBookings = -1 ;
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
        public static void AddBookingToCsvFile(string csvFilePath, Booking newBooking)
        {
            string csvLine = $"{newBooking.Id},{newBooking.Passenger.Name},{newBooking.BookedFlight.FlightNumber},{newBooking.Class}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        }//save booking to file
        public static void SaveBookingsToCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in BookingsManager.Bookings)
            {
                csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }// save to file
        public static void DeleteBookingFromCsv(string filePath , int bookingToRemoveId)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in BookingsManager.Bookings)
            {
                if (booking.Id != bookingToRemoveId)
                {
                    csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
                }
                else csvData.AppendLine($"{-1},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }//to delete from the file
    }
}
