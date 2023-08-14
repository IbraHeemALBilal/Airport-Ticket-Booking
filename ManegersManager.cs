using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class ManagersManager
    {
        private static ManagersManager _instance;//Singleton 
        public List<Person> AllManagers { get; private set; }
        private ManagersManager()
        {
            AllManagers = new List<Person>();
        }
        public static ManagersManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManagersManager();
                }
                return _instance;
            }
        }
        public async Task ReadManagersFromFile()
        {
            string csvFilePath = "Managers.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    var manager = new Manager(fields[0], fields[1]);
                    AllManagers.Add(manager);
                }
            }
        }// to read Managers data from file
        
    }
}