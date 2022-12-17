using ddd_lab_2.Interfaces;
using System.Collections.Generic;

namespace ddd_lab_2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private List<IOrder> _ordersCollection = new List<IOrder>();

        public void Add(IOrder item)
        {
            _ordersCollection.Add(item);
        }

        public IOrder GetOrderById(int id)
        {
            return _ordersCollection.Find(x => x.Id == id);
        }
    }
}
