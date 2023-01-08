using KursachPatterns.Interfaces;

namespace KursachPatterns.Entities
{
    internal class CarWithCart : ICar
    {
        private ICar _decorated;

        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public CarWithCart(ICar car)
        {
            Model = car.Model;
            EngineType = car.EngineType;
            Horsepower = car.Horsepower;
            DoorsCount = car.DoorsCount;
            FuelType = car.FuelType;
            _decorated = car;
        }

        public void StartMoving()
        {
            _decorated.StartMoving();
            Console.WriteLine(Model + " began pulling the cart!");
        }

        public void StopMoving()
        {
            _decorated.StopMoving();
        }
    }
}
