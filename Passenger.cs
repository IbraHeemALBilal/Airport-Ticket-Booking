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
        public Passenger(string Name, string Password) : base(Name, Password)
        {
            this.Name = Name;
            this.Password = Password;
            PassengerBookings = new List<Booking>();
        }// con
        public bool AddBooking(Booking booking)
        {
            if (PassengerBookings.Any(b => b.BookedFlight.FlightNumber == booking.BookedFlight.FlightNumber))
            {
                return false;
            }
            PassengerBookings.Add(booking);
            BookingsFileOperations.AddBookingToCsvFile("bookings.csv",booking);
            return true;
        }// add
        public bool CancelBooking(int bookingId)
        {
            Booking bookingToRemove = PassengerBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToRemove != null)
            {
                PassengerBookings.RemoveAll(b => b.Id == bookingId);
                BookingsFileOperations.DeleteBookingFromCsv("bookings.csv" , bookingId);
                return true;
            }
            return false;
        }//cancel
        public bool ModifyBooking(int bookingId , FlightClassType newClass)
        {
            Booking bookingToModify = PassengerBookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingToModify != null)
            {
                bookingToModify.Class = newClass;
                PassengerBookings.RemoveAll(b => b.Id == bookingId);
                BookingsManager.AllBookings.RemoveAll(b => b.Id == bookingId);
                PassengerBookings.Add(bookingToModify);
                BookingsManager.AllBookings.Add(bookingToModify);
                BookingsFileOperations.SaveBookingsToCsv("bookings.csv");
                return true;
            }
            return false ;
        }//modify

    }
}