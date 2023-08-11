using System.Globalization;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Metrics;
namespace AirportTicketBooking
{
    internal static class Program
    {
            public static int NumberOfFlights;
            public static int NumberOfBookings;
            static async Task Main(string[] args)
            {
            await LoadCsvFilesData();
            NumberOfFlights = FlightsManager.Flights.Count;
            NumberOfBookings = BookingsManager.Bookings.Count;
            int choise;
            while (true)
            {
                Console.WriteLine("- - - - - - - - - - - - - ");
                Console.WriteLine("1 - Log in as a Passenger :  ");
                Console.WriteLine("2 - Log in as a Manager :  ");
                Console.WriteLine("- - - - - - - - - - - - - ");
                if (!int.TryParse(Console.ReadLine(), out choise))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                switch (choise)
                {
                    case 1:
                        Console.WriteLine("Enter your name :  ");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Enter your Password :  ");
                        string userPassword = Console.ReadLine();
                        Passenger passenger = PassengersManager.Passengers.FirstOrDefault(p => p.Name == userName && p.Password == userPassword);
                        if (passenger != null)
                        {
                            Console.WriteLine("Welcome, " + passenger.Name + "!");

                            while (true)
                            {
                                Console.WriteLine("- - - - - - - - - - - - - ");
                                Console.WriteLine("Choose what you want to do :  ");
                                Console.WriteLine("1 - Show the flights and make a booking :  ");
                                Console.WriteLine("2 - Search for a flights based on the destination :  ");
                                Console.WriteLine("3 - Cancel a booking :   ");
                                Console.WriteLine("4 - Modify a booking :  ");
                                Console.WriteLine("5 - Show my bookings :  ");
                                Console.WriteLine("0 - Back :  ");
                                Console.WriteLine("- - - - - - - - - - - - - ");

                                int option;
                                if (!int.TryParse(Console.ReadLine(), out option))
                                {
                                    Console.WriteLine("Invalid input.");
                                    continue;
                                }
                                if (option == 0) break;
                                switch (option)
                                {
                                    case 1:
                                        FlightsManager.PrintFlights(FlightsManager.Flights);
                                        Console.WriteLine("Enter the number of the flight : ");
                                        int numberOfFlight;
                                        if (!int.TryParse(Console.ReadLine(), out numberOfFlight))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        Flight flight = FlightsManager.Flights[numberOfFlight - 1];
                                        Console.WriteLine("What class do you need?");
                                        Console.WriteLine("1 - Economy class");
                                        Console.WriteLine("2 - Business class");
                                        Console.WriteLine("3 - First class");
                                        if (!Enum.TryParse(Console.ReadLine(), out FlightClassType flightClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        AddNewBooking(passenger, flight, flightClass);
                                        break;

                                    case 2:
                                        Console.WriteLine("Inter the destination name :  ");
                                        string destinationName = Console.ReadLine();
                                        if (FlightsManager.Flights.Any(f => f.DestinationCountry == destinationName))
                                        {
                                            List<Flight> destinationFlights = FlightsManager.Flights.Where(f => f.DestinationCountry == destinationName).ToList();
                                            FlightsManager.PrintFlights(destinationFlights);
                                        }
                                        else Console.WriteLine("Theres no flights to this destination :( :  ");
                                        break;

                                    case 3:
                                        BookingsManager.PrintBookingsList(passenger.PassengerBookings);
                                        Console.WriteLine("Select number of booking to cancel :  ");
                                        int cancelBooking;
                                        if (!int.TryParse(Console.ReadLine(), out cancelBooking))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        if (passenger.CancelBooking(cancelBooking))
                                            Console.WriteLine("Booking canceled .");
                                        else Console.WriteLine("Theres no booking with this number . ");
                                        break;
                                    case 4:
                                        BookingsManager.PrintBookingsList(passenger.PassengerBookings);
                                        Console.WriteLine("Enter the number of booking to modify : ");
                                        int newClass;
                                        if (!int.TryParse(Console.ReadLine(), out newClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        Console.WriteLine("What's the new class you need?");
                                        Console.WriteLine("1 - Economy class");
                                        Console.WriteLine("2 - Business class");
                                        Console.WriteLine("3 - First class");
                                        if (!Enum.TryParse(Console.ReadLine(), out FlightClassType newFlightClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        ModifyBookingClass(passenger, newClass, newFlightClass);
                                        break;
                                    case 5:
                                        BookingsManager.PrintBookingsList(passenger.PassengerBookings);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }//switch 2
                            }//while2
                        }
                        else
                        {
                            Console.WriteLine("Invalid credentials. Login failed.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter your name :  ");
                        string managerName = Console.ReadLine();
                        Console.WriteLine("Enter your Password :  ");
                        string managerPassword = Console.ReadLine();
                        Manager manager = ManegersManager.Managers.FirstOrDefault(m => m.Name == managerName && m.Password == managerPassword);
                        if (manager != null)
                        {
                            Console.WriteLine("Welcome, " + manager.Name + "!");
                            Console.WriteLine("- - - - - - - - - - - - - ");
                            while (true)
                            {
                                Console.WriteLine("Choose what you want to do :  ");
                                Console.WriteLine("1 - Show all flights :  ");
                                Console.WriteLine("2 - Show all bookings :  ");
                                Console.WriteLine("3 - Show all bookings for destination :  ");
                                Console.WriteLine("4 - Add new flight :  ");
                                Console.WriteLine("0 - back :   ");
                                Console.WriteLine("- - - - - - - - - - - - - ");

                                int option;
                                if (!int.TryParse(Console.ReadLine(), out option))
                                {
                                    Console.WriteLine("Invalid input.");
                                    continue;
                                }
                                if (option == 0) break;
                                switch (option)
                                {
                                    case 1:
                                        FlightsManager.PrintFlights(FlightsManager.Flights);
                                        break;
                                    case 2:
                                        await BookingsManager.PrintBookingsList(BookingsManager.Bookings);
                                        break;
                                    case 3:
                                        await SearchDestination();
                                        break;
                                    case 4:
                                        AddNewFlightToCsvFile(manager);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid credentials. Login failed.");
                        }
                        break;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }//switch
            }//while
        }//main
        private static async Task LoadCsvFilesData()
        {
            Task readFlightsTask = Task.Run(FlightsManager.ReadFlightsFromFile);
            Task readPassengersTask = Task.Run(PassengersManager.ReadPassengersFromFile);
            Task readManagersTask = Task.Run(ManegersManager.ReadManagersFromFile);
            Task readBookingsTask = Task.Run(BookingsManager.ReadBookingsFromFile);

            await Task.WhenAll(readFlightsTask, readPassengersTask, readManagersTask, readBookingsTask);

            Task loadPassengersBookingsTask = Task.Run(PassengersManager.LoadPassengersBookings);
        }
        private static void AddNewBooking(Passenger passenger, Flight flight, FlightClassType flightClass)
        {
            switch (flightClass)
            {
                case FlightClassType.Economy:
                    if (passenger.AddBooking(new Booking(0, passenger, flight, FlightClassType.Economy)))
                        Console.WriteLine("Flight booked!");
                    else Console.WriteLine("You've already booked this flight.");
                    break;
                case FlightClassType.Business:
                    if (passenger.AddBooking(new Booking(0, passenger, flight, FlightClassType.Business)))
                        Console.WriteLine("Flight booked!");
                    else Console.WriteLine("You've already booked this flight.");
                    break;
                case FlightClassType.First:
                    if (passenger.AddBooking(new Booking(0, passenger, flight, FlightClassType.First)))
                        Console.WriteLine("Flight booked!");
                    else Console.WriteLine("You've already booked this flight.");
                    break;
                default:
                    Console.WriteLine("Invalid flight class.");
                    break;
            }
        }
        private static void ModifyBookingClass(Passenger passenger, int newClass, FlightClassType newFlightClass)
        {
            switch (newFlightClass)
            {
                case FlightClassType.Economy:
                    if (passenger.ModifyBooking(newClass, FlightClassType.Economy))
                        Console.WriteLine("Flight class modified!");
                    else Console.WriteLine("Booking not found.");
                    break;
                case FlightClassType.Business:
                    if (passenger.ModifyBooking(newClass, FlightClassType.Business))
                        Console.WriteLine("Flight class modified!");
                    else Console.WriteLine("Booking not found.");
                    break;
                case FlightClassType.First:
                    if (passenger.ModifyBooking(newClass, FlightClassType.First))
                        Console.WriteLine("Flight class modified!");
                    else Console.WriteLine("Booking not found.");
                    break;
                default:
                    Console.WriteLine("Invalid flight class.");
                    break;
            }
        }
        private static async Task SearchDestination()
        {
            Console.WriteLine("Enter the name of destination country : ");
            string destinationName = Console.ReadLine();
            List<Booking> bookingsForDestination = new List<Booking>();
            bookingsForDestination = BookingsManager.Bookings.Where(b => b.BookedFlight.DestinationCountry == destinationName).ToList();
            await BookingsManager.PrintBookingsList(bookingsForDestination);
        }
        private static void AddNewFlightToCsvFile(Manager manager)
        {
            Console.WriteLine("Enter the information about the new flight :  : ");
            Console.WriteLine("Departure Country : ");
            string departureCountry = Console.ReadLine();
            Console.WriteLine("Destination Country : ");
            string destinationCountry = Console.ReadLine();
            Console.WriteLine("Departure Airport : ");
            string departureAirport = Console.ReadLine();
            Console.WriteLine("Arrival Airport : ");
            string arrivalAirport = Console.ReadLine();
            Console.WriteLine("Departure Date : ");
            string departureDate = Console.ReadLine();
            Console.WriteLine("Economy Class Price : ");
            decimal EconomyClassPrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Bussiness Class Price : ");
            decimal bussinessClassPrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("First Class Price : ");
            decimal firstClassPrice = decimal.Parse(Console.ReadLine());

            Flight newFlight = new Flight(0, departureCountry, destinationCountry, departureAirport, arrivalAirport, departureDate, EconomyClassPrice, bussinessClassPrice, firstClassPrice);
            List<string> validationErrors = newFlight.IsValid(newFlight);
            if (validationErrors.Count > 0)
            {
                Console.WriteLine("Flight validation failed. Please correct the following errors:");
                foreach (var error in validationErrors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Flight is added :) ");
                manager.AddNewFlight(newFlight);
            }
        }
    }//class
}//namespace


