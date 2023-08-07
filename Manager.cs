
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Manager : Person
    {
        public Manager(string name, string password) : base(name, password)
        {
            Name = name;
            Password = password;
        }
        public void ViewAllFlights()
        {
            StaticMembers.PrintFlights(StaticMembers.Flights);
        }
        public void AddNewFlight(Flight newflight)
        {
            StaticMembers.Flights.Add(newflight);
            newflight.SaveToCsv("flights.csv");
        }// to add to the list and to the file
    }
}

