using System;

namespace Exam.DeliveriesManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var deliverSystem = new DeliveriesManager();
            var deliver = new Deliverer("Alex", "Alexandra");
            deliverSystem.AddDeliverer(deliver);

            var package = new Package("Bulgaria", "Test", "Plovdiv", "0877954787", 12);

            deliverSystem.AddPackage(package);
            deliverSystem.AssignPackage(deliver, package);

            ///Console.WriteLine(String.Join(", ", deliver));
            ///
            foreach (var pack in deliver.Packages)
            {
                Console.WriteLine(pack.ToString());
            }
        }
    }
}
