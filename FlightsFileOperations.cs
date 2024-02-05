using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class FlightsFileOperations
    {
        public static async Task ReadFlightsFromFileAsync()
        {
            string csvFilePath = "flights.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var flight = new Flight(
                    FlightNumber : int.Parse(fields[0], CultureInfo.InvariantCulture),
                    DepartureCountry: fields[1],
                    DestinationCountry: fields[2],
                    DepartureAirport: fields[3],
                    ArrivalAirport: fields[4],
                    DepartureDate: fields[5],
                    EconomyClassPrice: decimal.Parse(fields[6], CultureInfo.InvariantCulture),
                    BusinessClassPrice: decimal.Parse(fields[7], CultureInfo.InvariantCulture),
                    FirstClassPrice: decimal.Parse(fields[8], CultureInfo.InvariantCulture)
                    );
                    FlightsManager.AllFlights.Add(flight);
                }
            }

        }// to read flights data from file
        public static void AddFlightToCsvFile(Flight newflight)
        {
            string csvFilePath = "flights.csv";
            string csvLine = $"{newflight.FlightNumber},{newflight.DepartureCountry},{newflight.DestinationCountry},{newflight.DepartureAirport},{newflight.ArrivalAirport},{newflight.DepartureDate},{newflight.EconomyClassPrice},{newflight.BusinessClassPrice},{newflight.FirstClassPrice}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        }
    }
}
