
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal class PassengersManager
    {
        public static List<Person> AllPassengers = new List<Person>();
        public static async Task LoadPassengersBookingsAsync()
        {
            foreach (var p in AllPassengers)
            {
                ((Passenger)p).PassengerBookings = BookingsManager.AllBookings.Where(b => b.Passenger.Name == p.Name).ToList();
            }
        }
    }
}
