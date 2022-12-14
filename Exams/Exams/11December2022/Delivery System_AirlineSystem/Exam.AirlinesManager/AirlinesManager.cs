using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private Dictionary<string, Airline> airlines;
        private Dictionary<string, Flight> flights;

        public AirlinesManager()
        {
            this.airlines = new Dictionary<string, Airline>();
            this.flights = new Dictionary<string, Flight>();
        }

        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!this.airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }
            this.flights.Add(flight.Id, flight);
            flight.Airline = airline;
            this.airlines[airline.Id].Flights.Add(flight);
        }

        public bool Contains(Airline airline)
        {
            return this.airlines.ContainsKey(airline.Id);
        }

        public bool Contains(Flight flight)
        {
            return this.flights.ContainsKey(flight.Id);
        }

        public void DeleteAirline(Airline airline)
        {
            if (!this.airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }
            this.airlines.Remove(airline.Id);
            foreach (var fight in airline.Flights)
            {
                this.flights.Remove(fight.Id);
            }
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            return this.airlines.Values
                            .OrderByDescending(r => r.Rating)
                            .ThenByDescending(c => c.Flights.Count)
                            .ThenBy(n => n.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            List<Airline> result = new List<Airline>();

            foreach (var airline in this.airlines)
            {
                if (airline.Value.Flights.Count >= 1)
                {
                    foreach (var flight in airline.Value.Flights)
                    {
                        if (flight.IsCompleted == false && flight.Origin == origin && flight.Destination == destination)
                        {
                            result.Add(airline.Value);
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this.flights.Values;
        }

        public IEnumerable<Flight> GetCompletedFlights()
        {
            return this.flights.Values.Where(f => f.IsCompleted == true);
        }

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            return this.flights.Values.OrderBy(f => f.IsCompleted).ThenBy(n => n.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!this.airlines.ContainsKey(airline.Id) || !this.flights.ContainsKey(flight.Id))
            {
                throw new ArgumentException();
            }
            flight.IsCompleted = true;
            return flight;
        }
    }
}
