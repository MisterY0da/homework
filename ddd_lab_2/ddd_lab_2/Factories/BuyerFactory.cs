using ddd_lab_2.Entities;
using ddd_lab_2.Interfaces;

namespace ddd_lab_2.Factories
{
    public class BuyerFactory : IBuyerFactory
    {
        public IBuyer CreateRussianBuyer(string name)
        {
            return new RussianBuyer() { Name = name };
        }

        public IBuyer CreateForeignBuyer(string name)
        {
            return new ForeignBuyer() { Name = name };
        }       
    }
}
