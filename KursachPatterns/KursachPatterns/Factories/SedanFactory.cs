using KursachPatterns.Entities;
using KursachPatterns.Interfaces;

namespace KursachPatterns.Factories
{
    public class SedanFactory : ICarFactory
    {
        public ICar CreateCar(string model)
        {
            return new Sedan(model);
        }
    }
}
