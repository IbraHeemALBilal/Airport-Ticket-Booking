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
        public Flight(int FlightNumber, string DepartureCountry, string DestinationCountry,
              string DepartureAirport, string ArrivalAirport, string DepartureDate,
              decimal EconomyClassPrice, decimal BusinessClassPrice, decimal FirstClassPrice)
        {
            if (FlightNumber != default) this.FlightNumber = FlightNumber;// when read from file
            else this.FlightNumber = ++Program.NumberOfFlights;//when make from the program
            this.DepartureCountry = DepartureCountry;
            this.DestinationCountry = DestinationCountry;
            this.DepartureAirport = DepartureAirport;
            this.ArrivalAirport = ArrivalAirport;
            this.DepartureDate = DepartureDate;
            this.EconomyClassPrice = EconomyClassPrice;
            this.BusinessClassPrice = BusinessClassPrice;
            this.FirstClassPrice = FirstClassPrice;
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
