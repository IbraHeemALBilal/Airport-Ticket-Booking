﻿using System.Globalization;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace AirportTicketBooking
{
    internal static class Program
    {
             public static int numOfBookings;
             static void Main(string[] args)
             {
             StaticMethods.ReadFlightsFromFile();
             StaticMethods.ReadPassengersFromFile();
             StaticMethods.ReadManagersFromFile();
             StaticMethods.ReadBookingsFromFile();
             StaticMethods.LoadPassengersBookings();
             numOfBookings = StaticMethods.Bookings.Count + 1;
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
                        Passenger passenger = StaticMethods.Passengers.FirstOrDefault(p => p.Name == userName && p.Password == userPassword);
                        if (passenger != null)
                        {
                            Console.WriteLine("Welcome, " + passenger.Name + "!");
                            Console.WriteLine("- - - - - - - - - - - - - ");

                            while(true)
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
                                    case 1 :
                                        StaticMethods.PrintFlights(StaticMethods.Flights);
                                        Console.WriteLine("Enter the number of the flight : ");
                                        int numberOfFlight;
                                        if (!int.TryParse(Console.ReadLine() , out numberOfFlight))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        Flight flight = StaticMethods.Flights[numberOfFlight - 1];
                                        Console.WriteLine("What the class you need : ");
                                        Console.WriteLine("1 - Economy class : ");
                                        Console.WriteLine("2 - Business class : ");
                                        Console.WriteLine("3 - First class : ");
                                        int flightClass;
                                        if (!int.TryParse(Console.ReadLine(), out flightClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        if (flightClass==1)
                                        {
                                            if (passenger.AddBooking(new Booking(0,passenger, flight, "economy class")))
                                                Console.WriteLine("Flight booked ! ");
                                            else Console.WriteLine("You booked it befor . ");

                                        }
                                        else if (flightClass==2)
                                        {
                                            if(passenger.AddBooking(new Booking(0,passenger, flight, "business class")))
                                                Console.WriteLine("Flight booked ! ");
                                            else Console.WriteLine("You booked it befor . ");

                                        }
                                        else if (flightClass == 3)
                                        {
                                            if (passenger.AddBooking(new Booking(0,passenger, flight, "first class")))
                                                Console.WriteLine("Flight booked ! ");
                                            else Console.WriteLine("You booked it befor . ");
                                        }
                                        break;

                                        case 2:
                                        Console.WriteLine("Inter the destination name :  ");
                                        string destinationName = Console.ReadLine();
                                        if (StaticMethods.Flights.Any(f => f.DestinationCountry == destinationName))
                                        {
                                            List<Flight> destinationFlights = StaticMethods.Flights.Where(f => f.DestinationCountry == destinationName).ToList();
                                            StaticMethods.PrintFlights(destinationFlights);
                                        }
                                        else Console.WriteLine("Theres no flights to this destination :( :  ");
                                        break;


                                    case 3:
                                        passenger.PrintBookings();
                                        Console.WriteLine("Select number of booking to cancel :  ");
                                        int cancelBooking;
                                        if (!int.TryParse(Console.ReadLine(), out cancelBooking))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        if(passenger.CancelBooking(cancelBooking))
                                            Console.WriteLine("Booking canceled .");
                                        else Console.WriteLine("Theres no booking with this number . ");
                                        break;
                                    case 4:
                                        passenger.PrintBookings();
                                        Console.WriteLine("Enter the number of booking to modify : ");
                                        int newClass;
                                        if (!int.TryParse(Console.ReadLine(), out newClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        Console.WriteLine("What the new class you need : ");
                                        Console.WriteLine("1 - Economy class : ");
                                        Console.WriteLine("2 - Business class : ");
                                        Console.WriteLine("3 - First class : ");
                                        int newFlightClass;
                                        if (!int.TryParse(Console.ReadLine(), out newFlightClass))
                                        {
                                            Console.WriteLine("Invalid input.");
                                            continue;
                                        }
                                        if (newFlightClass == 1)
                                        {
                                            if (passenger.ModifyBooking(newClass, "economy class"))
                                                Console.WriteLine("Flight modified  ! ");
                                            else Console.WriteLine("Not exists . . ");

                                        }
                                        else if (newFlightClass == 2)
                                        {
                                            if (passenger.ModifyBooking(newClass, "business class"))
                                                Console.WriteLine("Flight modified ! ");
                                            else Console.WriteLine("Not exists . ");

                                        }
                                        else if (newFlightClass == 3)
                                        {
                                            if (passenger.ModifyBooking(newClass, "first class"))
                                                Console.WriteLine("Flight modified ! ");
                                            else Console.WriteLine("Not exists . ");
                                        }

                                        break;
                                    case 5:
                                        passenger.PrintBookings();
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
                        Manager manager = StaticMethods.Managers.FirstOrDefault(m => m.Name == managerName && m.Password == managerPassword);
                        if (manager != null)
                        {
                            Console.WriteLine("Welcome, " + manager.Name + "!");
                            Console.WriteLine("- - - - - - - - - - - - - ");
                            while(true)
                            {
                                Console.WriteLine("Choose what you want to do :  ");
                                Console.WriteLine("1 - Show all flights :  ");
                                Console.WriteLine("2 - Show all bookings :  ");
                                Console.WriteLine("3 - Show all bookings for destination :  ");
                                Console.WriteLine("0 - back :   ");
                                Console.WriteLine("- - - - - - - - - - - - - ");

                                int option;
                                if (!int.TryParse(Console.ReadLine(), out option))
                                {
                                    Console.WriteLine("Invalid input.");
                                    continue;
                                }
                                if (option == 0) break;
                                switch(option)
                                {
                                    case 1:
                                        manager.ViewAllFlights(StaticMethods.Flights);
                                        break;
                                    case 2:
                                        StaticMethods.PrintBookingsList(StaticMethods.Bookings);
                                        break;
                                    case 3:
                                        Console.WriteLine("Enter the name of destination country : ");
                                        string destinationName=Console.ReadLine();
                                        List<Booking> bookingsForDestination = new List<Booking>();
                                        bookingsForDestination= StaticMethods.Bookings.Where(b=>b.BookedFlight.DestinationCountry== destinationName).ToList();
                                        StaticMethods.PrintBookingsList(bookingsForDestination);
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
    }//class
}//namespace


