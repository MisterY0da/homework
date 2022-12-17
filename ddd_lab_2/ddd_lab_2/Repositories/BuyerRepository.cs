using ddd_lab_2.Interfaces;
using System.Collections.Generic;

namespace ddd_lab_2.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private List<IBuyer> _buyersCollection = new List<IBuyer>();

        public void Add(IBuyer item)
        {
            _buyersCollection.Add(item);
        }

        public IBuyer GetBuyerByName(string name)
        {
            return _buyersCollection.Find(x => x.Name == name);
        }
    }
}
