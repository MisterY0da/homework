using KursachPatterns.Interfaces;
using KursachPatterns.Entities;

namespace KursachPatterns.Factories
{
    public class TruckFactory : ICarFactory
    {
        public ICar CreateCar(string model)
        {
            return new Truck(model);
        }
    }
}
