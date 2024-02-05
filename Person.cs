using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal abstract class Person
    {
        public string Name { get; init; }
        public string Password { get; init; }
        public Person(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
    }
}
