
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal static class StaticMembers
    {
        public static List<Flight> Flights = new List<Flight>();
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Manager> Managers = new List<Manager>();
        public static List<Booking> Bookings = new List<Booking>();
        public static void ReadFlightsFromFile()
        {
            string csvFilePath = "flights.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string dataLine = reader.ReadLine();
                    string[] fields = dataLine.Split(',');
                    var flight = new Flight
                    (
                        int.Parse(fields[0], CultureInfo.InvariantCulture),
                        fields[1],
                        fields[2],
                        fields[3],
                        fields[4],
                        fields[5],
                        decimal.Parse(fields[6], CultureInfo.InvariantCulture),
                        decimal.Parse(fields[7], CultureInfo.InvariantCulture),
                        decimal.Parse(fields[8], CultureInfo.InvariantCulture)
                    );
                    Flights.Add(flight);
                }
            }
        }// to read flights data from file
        public static void PrintFlights(List<Flight> flightsToPrint)
        {
            Console.WriteLine("Flights :");
            Console.WriteLine("----------------------");
            foreach (var flight in flightsToPrint)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                Console.WriteLine($"Departure Country: {flight.DepartureCountry}");
                Console.WriteLine($"Destination Country: {flight.DestinationCountry}");
                Console.WriteLine($"Departure Airport: {flight.DepartureAirport}");
                Console.WriteLine($"Arrival Airport: {flight.ArrivalAirport}");
                Console.WriteLine($"Departure Date: {flight.DepartureDate}");
                Console.WriteLine($"Economy Class Price: {flight.EconomyClassPrice}");
                Console.WriteLine($"Business Class Price: {flight.BusinessClassPrice}");
                Console.WriteLine($"First Class Price: {flight.FirstClassPrice}");
                Console.WriteLine("------------------------------");
            }
        }// print list of flights
        public static void ReadPassengersFromFile()
        {
            string csvFilePath = "Passengers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string dataLine = reader.ReadLine();
                    string[] fields = dataLine.Split(',');
                    var passenger = new Passenger
                    (
                        fields[0],
                        fields[1]
                    );
                    Passengers.Add(passenger);
                }
            }
        }// to read passengers data from file
        public static void ReadManagersFromFile()
        {
            string csvFilePath = "Managers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string dataLine = reader.ReadLine();
                    string[] fields = dataLine.Split(',');
                    var Manager = new Manager
                    (
                        fields[0],
                        fields[1]
                    );
                    Managers.Add(Manager);
                }
            }
        }// to read Managers data from file
        public static void ReadBookingsFromFile()
        {
            string csvFilePath = "bookings.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string dataLine = reader.ReadLine();
                    string[] fields = dataLine.Split(',');
                    int bookingId = int.Parse(fields[0]);
                    string passengerName = fields[1];
                    int flightNumber = int.Parse(fields[2]);
                    string classType = fields[3];

                    Passenger passenger = Passengers.FirstOrDefault(p => p.Name == passengerName);
                    Flight flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

                    if (passenger != null && flight != null)
                    {
                        var booking = new Booking(bookingId, passenger, flight, classType);
                        Bookings.Add(booking);
                    }
                }
            }
        }// to read bookings data from file
        public static void PrintBookingsList(List<Booking> bookings)
        {
            bookings.Clear();
            ReadBookingsFromFile();
            Console.WriteLine("----------------------");
            foreach (var booking in bookings)
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
        public static void LoadPassengersBookings()
        {
            foreach (var p in Passengers)
            {
                p.PassengersBookings = Bookings.Where(b => b.Passenger.Name == p.Name).ToList();
            }
        }
        /*public static int Count()
        {
            return Passengers.Count;
        }*/
    }
}
