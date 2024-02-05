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
        public Manager(string Name, string Password) : base(Name, Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
    }
}

