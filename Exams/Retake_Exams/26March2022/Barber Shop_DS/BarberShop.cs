using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop
{
    public class BarberShop : IBarberShop
    {
        private Dictionary<string, Barber> barbersByName = new Dictionary<string, Barber>();
        private Dictionary<string, Client> clientsByName = new Dictionary<string, Client>();

        public void AddBarber(Barber b)
        {
            if (barbersByName.ContainsKey(b.Name))
            {
                throw new ArgumentException();
            }

            barbersByName.Add(b.Name, b);
        }

        public void AddClient(Client c)
        {
            if (clientsByName.ContainsKey(c.Name))
            {
                throw new ArgumentException();
            }
            clientsByName.Add(c.Name, c);
        }

        public bool Exist(Barber b)
        {
            return barbersByName.ContainsKey(b.Name) ? true : false;
        }

        public bool Exist(Client c)
        {
            return clientsByName.ContainsKey(c.Name) ? true : false;
        }

        public IEnumerable<Barber> GetBarbers()
        {
            return barbersByName.Values;
        }

        public IEnumerable<Client> GetClients()
        {
            return clientsByName.Values;
        }

        public void AssignClient(Barber b, Client c)
        {
            if (!Exist(b) && !Exist(c))
            {
                throw new ArgumentException();
            }
            c.Barber = b;
            barbersByName[b.Name].Clients.Add(c);
        }

        public void DeleteAllClientsFrom(Barber b)
        {
            if (!Exist(b))
            {
                throw new ArgumentException();
            }

            foreach (var client in b.Clients)
            {
                clientsByName.Remove(client.Name);
            }
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
        {
            return clientsByName.Values.Where(b => b.Barber == null);
        }

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
        {
            return barbersByName.Values
                .OrderByDescending(c => c.Clients.Count);
        }

        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
        {
            return barbersByName.Values
                .OrderByDescending(b => b.Stars)
                .ThenBy(b => b.HaircutPrice);
        }

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
        {
            return clientsByName.Values
                .Where(c => c.Barber != null)
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Barber.Stars);
        }
    }
}
