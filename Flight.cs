using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Flight
    {
        [Required(ErrorMessage = "FlightNumber is required .. Free text ")]
        public int FlightNumber { get; set; }

        [Required(ErrorMessage = "DepartureCountry is required .. Free text")]
        public string DepartureCountry { get; set; }

        [Required(ErrorMessage = "DestinationCountry is required .. Free text")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "DepartureAirport is required .. Free text")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "ArrivalAirport is required  .. Free text")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "DepartureDate is required .. Free text")]
        public string DepartureDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "EconomyClassPrice should be greater than or equal to 0.")]
        public decimal EconomyClassPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "BusinessClassPrice should be greater than or equal to 0.")]
        public decimal BusinessClassPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FirstClassPrice should be greater than or equal to 0.")]
        public decimal FirstClassPrice { get; set; }
        public Flight(int flightNumber, string departureCountry, string destinationCountry,
              string departureAirport, string arrivalAirport, string departureDate,
              decimal economyClassPrice, decimal businessClassPrice, decimal firstClassPrice)
        {
            if (flightNumber != default) FlightNumber = flightNumber;// when read from file
            else FlightNumber = ++Program.NumberOfFlights;//when make from the program
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureDate = departureDate;
            EconomyClassPrice = economyClassPrice;
            BusinessClassPrice = businessClassPrice;
            FirstClassPrice = firstClassPrice;
        }//con
        public List<string> IsValid(Flight flight)
        {
            List<string> validationErrors = new List<string>();
            var context = new ValidationContext(flight, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(flight, context, results, true))
            {
                foreach (var result in results)
                {
                    validationErrors.Add(result.ErrorMessage);
                }
            }

            return validationErrors;
        }// to check if flight is valid and return a list of errors 
    }
}
