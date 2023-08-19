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
            NumberOfFlights = FlightsManager.AllFlights.Count;
            NumberOfBookings = BookingsManager.AllBookings.Count;
            IFlightsPrinterFactory printerFactory = new FlightsPrinterFactory();
            int choise;
            while (true)
            {
                Console.WriteLine("- - - - - - - - - - - - - ");
                Console.WriteLine("1 - Log in as a Passenger :  ");
                Console.WriteLine("2 - Log in as a Manager :  ");
                Console.WriteLine("3 - Sign Up :  ");
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
                        Passenger passenger = (Passenger)PassengersManager.AllPassengers.FirstOrDefault(p => p.Name == userName && p.Password == userPassword);
                        if (passenger != null)
                        {
                            Console.WriteLine("Welcome, " + passenger.Name + "!");

                            while (true)
                            {
                                Console.WriteLine("- - - - - - - - - - - - - ");
                                Console.WriteLine("Choose what you want to do :  ");
                                Console.WriteLine("1 - Show the flights and make a booking :  ");
                                Console.WriteLine("2 - Search for flights  :  ");
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
                                        IFlightsPrinter consolePrinter = printerFactory.CreateFlightsPrinter(PrinterType.ConsolePrinter);
                                        consolePrinter.PrintFlights(FlightsManager.AllFlights);
                                        Console.WriteLine("Enter the number of the flight : ");
                                        int numberOfFlight;
                                        if (!int.TryParse(Console.ReadLine(), out numberOfFlight))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        Flight flight = FlightsManager.AllFlights[numberOfFlight - 1];
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
                                        SearchForFlights();
                                        break;
                                    case 3:
                                        BookingsPrinter.PrintBookingsListAsync(passenger.PassengerBookings);
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
                                        BookingsPrinter.PrintBookingsListAsync(passenger.PassengerBookings);
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
                                        BookingsPrinter.PrintBookingsListAsync(passenger.PassengerBookings);
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
                        Manager manager = (Manager)ManagersManager.AllManagers.FirstOrDefault(m => m.Name == managerName && m.Password == managerPassword);
                        if (manager != null)
                        {
                            Console.WriteLine("Welcome, " + manager.Name + "!");
                            Console.WriteLine("- - - - - - - - - - - - - ");
                            while (true)
                            {
                                Console.WriteLine("Choose what you want to do :  ");
                                Console.WriteLine("1 - Show all flights on console :  ");
                                Console.WriteLine("2 - Show all flights on file  :  ");
                                Console.WriteLine("3 - Show all bookings :  ");
                                Console.WriteLine("4 - Filter the Bookings :  ");
                                Console.WriteLine("5 - Add new flight :  ");
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
                                        IFlightsPrinter consolePrinter = printerFactory.CreateFlightsPrinter(PrinterType.ConsolePrinter);
                                        consolePrinter.PrintFlights(FlightsManager.AllFlights);
                                        break;
                                    case 2:
                                        IFlightsPrinter csvFilePrinter = printerFactory.CreateFlightsPrinter(PrinterType.CsvFilePrinter);
                                        csvFilePrinter.PrintFlights(FlightsManager.AllFlights);
                                        break;
                                    case 3:
                                        await BookingsManager.UpdateBookingsListAsync();
                                        await BookingsPrinter.PrintBookingsListAsync(BookingsManager.AllBookings);
                                        break;
                                    case 4:
                                        await FilterBookings();
                                        break;
                                        break;
                                    case 5:
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
                    case 3:
                        SignUpOption();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;

                }//switch
            }//while
        }//main
        private static void SearchForFlights()
        {
            Console.WriteLine("Enter search criteria (press Enter to skip):");
            Console.WriteLine("Enter filtering criteria (press Enter to skip):");
            Console.Write("Departure Country: ");
            string departureCountryFilter = Console.ReadLine();
            Console.Write("Destination Country: ");
            string destinationCountryFilter = Console.ReadLine();
            Console.Write("Departure Date (yyyy-MM-dd): ");
            string departureDateFilter = Console.ReadLine();
            Console.Write("Departure Airport: ");
            string departureAirportFilter = Console.ReadLine();
            Console.Write("Arrival Airport: ");
            string arrivalAirportFilter = Console.ReadLine();
            var searchedFlights = FlightsManager.SearchFlights(
                departureCountryFilter,
                destinationCountryFilter,
                departureDateFilter,
                departureAirportFilter,
                arrivalAirportFilter
            );
            IFlightsPrinter consoleFlightsPrinter = new ConsoleFlightsPrinter();
            consoleFlightsPrinter.PrintFlights(searchedFlights);
        }
        private static async Task LoadCsvFilesData()
        {
            Task readFlightsTask = Task.Run(FlightsFileOperations.ReadFlightsFromFileAsync);
            Task readPassengersTask = Task.Run(PassengersFileOperations.ReadPassengersFromFileAsync);
            Task readManagersTask = Task.Run(ManagersFileOperations.ReadManagersFromFileAsync);//Singleton 
            Task readBookingsTask = Task.Run(BookingsFileOperations.ReadBookingsFromFileAsync);

            await Task.WhenAll(readFlightsTask, readPassengersTask, readManagersTask, readBookingsTask);

            Task loadPassengersBookingsTask = Task.Run(PassengersManager.LoadPassengersBookingsAsync);
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
        private static async Task FilterBookings()
        {
            Console.WriteLine("Enter filtering criteria (press Enter to skip):");
            Console.Write("Departure Country: ");
            string departureCountryFilter = Console.ReadLine();
            Console.Write("Destination Country: ");
            string destinationCountryFilter = Console.ReadLine();
            Console.Write("Departure Date (yyyy-MM-dd): ");
            string departureDateFilter = Console.ReadLine();
            Console.Write("Departure Airport: ");
            string departureAirportFilter = Console.ReadLine();
            Console.Write("Arrival Airport: ");
            string arrivalAirportFilter = Console.ReadLine();
            Console.WriteLine("Choose Flight Class:");
            Console.WriteLine("1 - Economy");
            Console.WriteLine("2 - Business");
            Console.WriteLine("3 - First");
            FlightClassType? classTypeFilter = Enum.TryParse(Console.ReadLine(), out FlightClassType classType) ? classType : (FlightClassType?)null;

            var filteredBookings = BookingsManager.FilterBookings(
                departureCountryFilter,
                destinationCountryFilter,
                departureDateFilter,
                departureAirportFilter,
                arrivalAirportFilter,
                classTypeFilter
            );
            await BookingsPrinter.PrintBookingsListAsync(filteredBookings);
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
                FlightsManager.AddNewFlight(newFlight);
            }
        }
        private static void SignUpOption()
        {
            int signUpOption;
            while (true)
            {
                Console.WriteLine("Sign up as :   ( 1 ) Passenger. ( 2 ) Manager.  ( 0 ) Back .");
                if (!int.TryParse(Console.ReadLine(), out signUpOption))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                if (signUpOption == 0) break;
                Console.WriteLine("Enter your name :  ");
                string NewUserName = Console.ReadLine();
                Console.WriteLine("Enter your Password :  ");
                string newUserPassword = Console.ReadLine();
                if (signUpOption == 1)
                {
                    ISignUpSurvice PassengersFileOperationsInstanse = PassengersFileOperations.Instance;
                    if (PassengersFileOperationsInstanse.AddNewUser(new Passenger(NewUserName, newUserPassword)))
                        Console.WriteLine("Success :) ");
                    else Console.WriteLine("Theres another account with the same name . ");
                }
                if (signUpOption == 2)
                {
                    ISignUpSurvice ManagersFileOperationsInstance = ManagersFileOperations.Instance;//Singleton 
                    if (ManagersFileOperationsInstance.AddNewUser(new Manager(NewUserName, newUserPassword)))
                        Console.WriteLine("Success :) ");
                    else Console.WriteLine("Theres another account with the same name . ");
                }
            }
        }
    }//class
}//namespace


