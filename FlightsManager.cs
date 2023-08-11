using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AirportTicketBooking
{
    internal static class FlightsManager
    {
        public static List<Flight> Flights = new List<Flight>();
        public static async Task ReadFlightsFromFile()
        {
            string csvFilePath = "flights.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
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
        public static void AddFlightToCsvFile(string csvFilePath, Flight newflight)
        {
            string csvLine = $"{newflight.FlightNumber},{newflight.DepartureCountry},{newflight.DestinationCountry},{newflight.DepartureAirport},{newflight.ArrivalAirport},{newflight.DepartureDate},{newflight.EconomyClassPrice},{newflight.BusinessClassPrice},{newflight.FirstClassPrice}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        }// save new flight to the file
    }
}
