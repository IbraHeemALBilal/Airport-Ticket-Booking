using System.Text;

namespace AirportTicketBooking
{
    internal class BookingsFileOperations
    {
        public static async Task ReadBookingsFromFileAsync()
        {
            string csvFilePath = "bookings.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    string dataLine = await reader.ReadLineAsync();
                    string[] fields = dataLine.Split(',');
                    int bookingId = int.Parse(fields[0]);
                    string passengerName = fields[1];
                    int flightNumber = int.Parse(fields[2]);
                    FlightClassType classType = Enum.Parse<FlightClassType>(fields[3]);
                    Passenger passenger =(Passenger) PassengersManager.AllPassengers.FirstOrDefault(p => p.Name == passengerName);
                    Flight flight = FlightsManager.AllFlights.FirstOrDefault(f => f.FlightNumber == flightNumber);

                    if (passenger != null && flight != null)
                    {
                        var booking = new Booking(bookingId, passenger, flight, classType);
                        BookingsManager.AllBookings.Add(booking);
                    }
                }
            }
        }// to read bookings data from file
        public static void AddBookingToCsvFile(string csvFilePath, Booking newBooking)
        {
            string csvLine = $"{newBooking.Id},{newBooking.Passenger.Name},{newBooking.BookedFlight.FlightNumber},{newBooking.Class}";
            File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        }//save booking to file
        public static void SaveBookingsToCsv(string filePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in BookingsManager.AllBookings)
            {
                csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }// save to file
        public static void DeleteBookingFromCsv(string filePath, int bookingToRemoveId)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("booking_id,passenger_name,F_num,Class");
            foreach (var booking in BookingsManager.AllBookings)
            {
                if (booking.Id != bookingToRemoveId)
                {
                    csvData.AppendLine($"{booking.Id},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
                }
                else csvData.AppendLine($"{-1},{booking.Passenger.Name},{booking.BookedFlight.FlightNumber},{booking.Class}");
            }
            System.IO.File.WriteAllText(filePath, csvData.ToString());
        }//to delete from the file
    }
}
