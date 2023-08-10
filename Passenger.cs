using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{

    internal class Passenger : Person
    {
        public List<Booking> PassengerBookings { get; set; }
        public Passenger(string name, string password) : base(name, password)
        {
            Name = name;
            Password = password;
            PassengerBookings = new List<Booking>();

        }// con
        public void ViewAllFlights()
        {
            StaticMembers.PrintFlights(StaticMembers.Flights);
        }
        public bool AddBooking(Booking booking)
        {
            if (PassengerBookings.Any(b => b.BookedFlight.FlightNumber == booking.BookedFlight.FlightNumber))
            {
                return false;
            }
            PassengerBookings.Add(booking);
            booking.SaveToCsv("bookings.csv");
            return true;
        }// add
        public List<Booking> GetAllBookings()
        {
            return PassengerBookings;
        }//get
        private void SaveBookingsToCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in StaticMembers.Bookings)
            {
                csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }// save to file
        private void DeleteBookingFromCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in StaticMembers.Bookings)
            {
                if (!PassengerBookings.Contains(booking)) 
                {
                    csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
                }
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }//to delete from the file
        public bool CancelBooking(int bookingId)
        {
            Booking bookingToRemove = PassengerBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToRemove != null)
            {
                PassengerBookings.RemoveAll(b => b.Id == bookingId);
                StaticMembers.Bookings.RemoveAll(b => b.Id == bookingId);
                DeleteBookingFromCsv("bookings.csv");
                return true;
            }
            return false;
        }//cancel
        public bool ModifyBooking(int bookingId , string newClass)
        {
            Booking bookingToModify = PassengerBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToModify != null)
            {
                bookingToModify.Class = newClass;
                PassengerBookings.RemoveAll(b => b.Id == bookingId);
                StaticMembers.Bookings.RemoveAll(b => b.Id == bookingId);
                PassengerBookings.Add(bookingToModify);
                StaticMembers.Bookings.Add(bookingToModify);
                SaveBookingsToCsv("bookings.csv");
                return true;
            }
            return false ;
        }//modify
        public void PrintBookings()
        {
            Console.WriteLine("your Bookings :");
            foreach (var booking in PassengerBookings)
            {
                Console.WriteLine("Booking ID: " + booking.Id);
                Console.WriteLine("Flight Number: " + booking.BookedFlight.FlightNumber);
                Console.WriteLine("Departure Airport: " + booking.BookedFlight.DepartureAirport);
                Console.WriteLine("Arrival Airport: " + booking.BookedFlight.ArrivalAirport);
                Console.WriteLine("Booking Date: " + booking.BookingDate);
                Console.WriteLine("Class: " + booking.Class);
                Console.WriteLine("----------------------");
            }
        }// print bookings for this passenger
    }
}