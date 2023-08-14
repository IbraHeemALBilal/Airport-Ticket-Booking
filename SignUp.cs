using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal static class SignUp
    {
        public static bool AddNewUser(Person person , string csvFilePath ,List<Person>persons)
        {
            if (persons.Any(p => p.Name == person.Name))
                return false;
            string csvLine = $"{person.Name},{person.Password}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
            persons.Add(person);
            return true;
        }
    }
}
