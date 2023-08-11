using AirportTicketBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal static class ManegersManager
    {
        public static List<Manager> Managers = new List<Manager>();
        public static async Task ReadManagersFromFile()
        {
            string csvFilePath = "Managers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var Manager = new Manager
                    (
                        fields[0],
                        fields[1]
                    );
                    Managers.Add(Manager);
                }
            }
        }// to read Managers data from file
    }
}
