using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class ManagersFileOperations : ISignUpSurvice
    {
        private static ManagersFileOperations _instance;
        public static ManagersFileOperations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManagersFileOperations();
                }
                return _instance;
            }
        }
        public async static Task ReadManagersFromFileAsync()
        {
            string csvFilePath = "Managers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var manager = new Manager(Name: fields[0], Password: fields[1]);
                    ManagersManager.AllManagers.Add(manager);
                }
            }
        }// to read Managers data from file
        public bool AddNewUser(Person person)
        {
            var csvFilePath = "Managers.csv";
            if (ManagersManager.AllManagers.Any(p => p.Name == person.Name))
                return false;
            string csvLine = $"{person.Name},{person.Password}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
            ManagersManager.AllManagers.Add(person);
            return true;
        }
    }
}
