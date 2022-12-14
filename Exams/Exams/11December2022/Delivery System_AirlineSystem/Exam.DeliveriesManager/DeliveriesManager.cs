using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private Dictionary<string, Deliverer> deliverers;
        private Dictionary<string, Package> packages;

        public DeliveriesManager()
        {
            this.deliverers = new Dictionary<string, Deliverer>();
            this.packages = new Dictionary<string, Package>();
        }

        public void AddDeliverer(Deliverer deliverer)
        {
            this.deliverers.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {
            this.packages.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!this.deliverers.ContainsKey(deliverer.Id) || !this.packages.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }

            package.Deliverer = deliverer;
            this.deliverers[deliverer.Id].Packages.Add(package);
        }

        public bool Contains(Deliverer deliverer)
        {
            return this.deliverers.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
            return this.packages.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        {
            return this.deliverers.Values;
        }

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            return this.deliverers.Values.OrderByDescending(p => p.Packages.Count).ThenBy(n => n.Name);
        }

        public IEnumerable<Package> GetPackages()
        {
            return this.packages.Values;
        }

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return this.packages.Values.OrderByDescending(w => w.Weight).ThenBy(r => r.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
            return this.packages.Values.Where(p => p.Deliverer == null);
        }
    }
}
