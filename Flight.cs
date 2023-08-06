using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Flight
    {
        [Required(ErrorMessage = "FlightNumber is required.")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "DepartureCountry is required.")]
        public string DepartureCountry { get; set; }

        [Required(ErrorMessage = "DestinationCountry is required.")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "DepartureAirport is required.")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "ArrivalAirport is required.")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "DepartureDate is required.")]
        public string DepartureDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "EconomyClassPrice should be greater than or equal to 0.")]
        public decimal EconomyClassPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "BusinessClassPrice should be greater than or equal to 0.")]
        public decimal BusinessClassPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FirstClassPrice should be greater than or equal to 0.")]
        public decimal FirstClassPrice { get; set; }

        public Flight(string flightNumber, string departureCountry, string destinationCountry,
              string departureAirport, string arrivalAirport, string departureDate,
              decimal economyClassPrice, decimal businessClassPrice, decimal firstClassPrice)
        {
            FlightNumber = flightNumber;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureDate = departureDate;
            EconomyClassPrice = economyClassPrice;
            BusinessClassPrice = businessClassPrice;
            FirstClassPrice = firstClassPrice;
        }
    }
}
