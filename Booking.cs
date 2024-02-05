
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Booking
    {
        private static int _lastAssignedId = Program.NumberOfBookings;
        public int Id { get; init; }
        public Passenger Passenger { get; init; }
        public Flight BookedFlight { get; init; }
        public DateTime BookingDate { get; init; }
        public FlightClassType Class { get; set; }
        public decimal price { get; set; }
        public Booking(int id, Passenger passenger, Flight flight, FlightClassType classType)
        {
            if (id != default) Id = id;//when read from file
            else Id = ++Program.NumberOfBookings;//when make booking from the program
            BookedFlight = flight;
            Passenger = passenger;
            BookingDate = DateTime.Now;
            Class = classType;
            switch (classType)
            {
                case FlightClassType.Economy:
                    price = flight.EconomyClassPrice;
                    break;
                case FlightClassType.Business:
                    price = flight.BusinessClassPrice;
                    break;
                case FlightClassType.First:
                    price = flight.FirstClassPrice;
                    break;
                default:
                    throw new ArgumentException("Invalid flight class type.", nameof(classType));
            }//con

        }
    }
}
