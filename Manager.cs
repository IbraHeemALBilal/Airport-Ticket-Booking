
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Manager
    {
        public string Name { set; get; }
        public string Password { set; get; }
        public Manager(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public void ViewAllFlights(List<Flight> flights)
        {
            StaticMethods.PrintFlights(flights);
        }


    }
}
