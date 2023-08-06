using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Passenger
    {
        public string Name { get; init; }
        public string Password { get; init; }
        public List<Booking> PassengersBookings { get; set; }

        public Passenger(string name , string password )
        {
            Name = name;
            Password = password;
            PassengersBookings = new List<Booking>();

        }// con
        public bool AddBooking(Booking booking)
        {
            if (PassengersBookings.Any(b => b.BookedFlight.FlightNumber == booking.BookedFlight.FlightNumber))
            {
                return false;
            }
            PassengersBookings.Add(booking);
            booking.SaveToCsv("bookings.csv");
            return true;
        }// add
        public List<Booking> GetAllBookings()
        {
            return PassengersBookings;
        }//get
        private void SaveBookingsToCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in StaticMethods.Bookings)
            {
                csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }
        private void DeleteBookingFromCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in StaticMethods.Bookings)
            {
                if (!PassengersBookings.Contains(booking)) 
                {
                    csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
                }
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }
        public bool CancelBooking(int bookingId)
        {
            Booking bookingToRemove = PassengersBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToRemove != null)
            {
                PassengersBookings.RemoveAll(b => b.Id == bookingId);
                StaticMethods.Bookings.RemoveAll(b => b.Id == bookingId);
                DeleteBookingFromCsv("bookings.csv");
                return true;
            }
            return false;
        }//cancel
        public bool ModifyBooking(int bookingId , string newClass)
        {
            Booking bookingToModify = PassengersBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToModify != null)
            {
                bookingToModify.Class = newClass;
                PassengersBookings.RemoveAll(b => b.Id == bookingId);
                StaticMethods.Bookings.RemoveAll(b => b.Id == bookingId);
                PassengersBookings.Add(bookingToModify);
                StaticMethods.Bookings.Add(bookingToModify);
                SaveBookingsToCsv("bookings.csv");
                return true;
            }
            return false ;
        }//modify
        public void PrintBookings()
        {
            Console.WriteLine("your Bookings :");
            foreach (var booking in PassengersBookings)
            {
                Console.WriteLine("Booking ID: " + booking.Id);
                Console.WriteLine("Flight Number: " + booking.BookedFlight.FlightNumber);
                Console.WriteLine("Departure Airport: " + booking.BookedFlight.DepartureAirport);
                Console.WriteLine("Arrival Airport: " + booking.BookedFlight.ArrivalAirport);
                Console.WriteLine("Booking Date: " + booking.BookingDate);
                Console.WriteLine("Class: " + booking.Class);
                Console.WriteLine("----------------------");
            }
        }
    }
}