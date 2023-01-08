using KursachPatterns.Interfaces;
using KursachPatterns.Entities;

namespace KursachPatterns.Factories
{
    public class SportCarFactory : ICarFactory
    {
        public ICar CreateCar(string model)
        {
            return new SportCar(model);
        }
    }
}
