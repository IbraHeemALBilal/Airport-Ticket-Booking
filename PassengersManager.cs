
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal static class PassengersManager
    {
        public static List<Person> AllPassengers = new List<Person>();
        public static async Task ReadPassengersFromFile()
        {
            string csvFilePath = "Passengers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var passenger = new Passenger
                    (
                        fields[0],
                        fields[1]
                    );
                    AllPassengers.Add(passenger);
                }
            }
        }// to read passengers data from file
        public static async Task LoadPassengersBookings()
        {
            foreach (var p in AllPassengers)
            {
                ((Passenger)p).PassengerBookings = BookingsManager.AllBookings.Where(b => b.Passenger.Name == p.Name).ToList();
            }
        }
    }
}
