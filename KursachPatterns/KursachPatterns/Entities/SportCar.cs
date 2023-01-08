using KursachPatterns.Interfaces;

namespace KursachPatterns.Entities
{
    public class SportCar : ICar
    {
        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public SportCar(string model)
        {
            Model = model;
            EngineType = "V8";
            Horsepower = 500;
            DoorsCount = 2;           
            FuelType = "petrol";
        }

        public void StartMoving()
        {
            Console.WriteLine("Sportcar " + Model + " reached 100 km/h for 4 seconds!");
        }

        public void StopMoving()
        {
            Console.WriteLine("Sportcar " + Model + " braked hard!");
        }
    }
}
