using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class PassengersFileOperations : ISignUpSurvice
    {
        private static PassengersFileOperations _instance; 
        public static PassengersFileOperations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PassengersFileOperations();
                }
                return _instance;
            }
        }
        public async static Task ReadPassengersFromFileAsync()
        {
            string csvFilePath = "Passengers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var passenger = new Passenger
                    (Name: fields[0], Password: fields[1]);
                    PassengersManager.AllPassengers.Add(passenger);
                }
            }
        }// to read passengers data from file
        public bool AddNewUser(Person person)
        {
            var csvFilePath = "Passengers.csv";
            if (PassengersManager.AllPassengers.Any(p => p.Name == person.Name))
                return false;
            string csvLine = $"{person.Name},{person.Password}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
            PassengersManager.AllPassengers.Add(person);
            return true;
        }
    }
}
