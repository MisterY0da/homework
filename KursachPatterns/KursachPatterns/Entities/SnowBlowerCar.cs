using KursachPatterns.Interfaces;

namespace KursachPatterns.Entities
{
    public class SnowBlowerCar : ICar
    {
        private ICar _decorated;

        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public SnowBlowerCar(ICar car)
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
            Console.WriteLine(Model + " began cleaning the snow with the snow blower!");
        }

        public void StopMoving()
        {
            _decorated.StopMoving();
        }
    }
}
