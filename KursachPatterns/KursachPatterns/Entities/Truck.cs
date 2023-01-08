using KursachPatterns.Interfaces;

namespace KursachPatterns.Entities
{
    public class Truck : ICar
    {
        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public Truck(string model)
        {
            Model = model;
            EngineType = "V6";
            Horsepower = 300;
            DoorsCount = 2;
            FuelType = "diesel";            
        }

        public void StartMoving()
        {
            Console.WriteLine("Truck " + Model + " started moving slowly");
        }

        public void StopMoving()
        {
            Console.WriteLine("Truck " + Model + " stopped slowly");
        }
    }
}
