using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private Dictionary<string, Company> companiesByName = new Dictionary<string, Company>();
        private Dictionary<string, Trip> tripsByName = new Dictionary<string, Trip>();

        public void AddCompany(Company c)
        {
            if (companiesByName.ContainsKey(c.Name))
            {
                throw new ArgumentException();
            }
            companiesByName.Add(c.Name, c);
        }

        public void AddTrip(Company c, Trip t)
        {
            if (!companiesByName.ContainsKey(c.Name))
            {
                throw new ArgumentException();
            }
            tripsByName.Add(t.Id, t);
            t.Company = c;
            companiesByName[c.Name].Trips.Add(t);
        }

        public bool Exist(Company c)
        {
            return companiesByName.ContainsKey(c.Name) ? true : false;
        }

        public bool Exist(Trip t)
        {
            return tripsByName.ContainsKey(t.Id) ? true : false;
        }

        public void RemoveCompany(Company c)
        {
            if (!Exist(c))
            {
                throw new ArgumentException();
            }

            var deletedCompany = companiesByName[c.Name];
            foreach (var trip in deletedCompany.Trips)
            {
                tripsByName.Remove(trip.Id);
            }
            companiesByName.Remove(c.Name);
        }

        public IEnumerable<Company> GetCompanies()
        {
            return companiesByName.Values;
        }

        public IEnumerable<Trip> GetTrips()
        {
            return tripsByName.Values;
        }

        public void ExecuteTrip(Company c, Trip t)
        {
            if (!Exist(c) || !Exist(t))
            {
                throw new ArgumentException();
            }

            if (!c.Trips.Contains(t))
            {
                throw new ArgumentException();
            }

            tripsByName.Remove(t.Id);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
        {
            return companiesByName.Values
                        .Where(t => t.Trips.Count > n);
        }

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
        {
            return tripsByName.Values
                    .Where(trip => trip.Transportation == t).ToList();
        }

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
        {
            return tripsByName.Values
                    .Where(t => t.Price >= lo && t.Price <= hi);
        }
    }
}
