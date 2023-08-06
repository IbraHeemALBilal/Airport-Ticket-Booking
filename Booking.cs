
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
        private static int lastAssignedId = Program.numOfBookings;
        public int Id { get; init ; }
        public Passenger Passenger { get ; init ; }
        public Flight BookedFlight { get ; init ; }   
        public DateTime BookingDate { get; init ; }
        public string Class { get; set; }
        public decimal price { get; set; } 

        public Booking(int id , Passenger passenger , Flight flight , string classType)
        {
            if (id != default) Id = id;
            else Id = Program.numOfBookings++;
            BookedFlight = flight;
            Passenger = passenger;
            BookingDate = DateTime.Now;
            Class = classType;
            if (classType == "economy class")
                price = flight.EconomyClassPrice;
            else if (classType == "business class")
                price = flight.BusinessClassPrice;
            else if (classType == "first class")
                price = flight.FirstClassPrice;
        }
        public void SaveToCsv(string csvFilePath)
        {
            string csvLine = $"{Id},{Passenger.Name},{BookedFlight.FlightNumber},{Class}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        }
    }
}
