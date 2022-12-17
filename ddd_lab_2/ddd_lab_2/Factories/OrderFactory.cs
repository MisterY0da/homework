using ddd_lab_2.Entities;
using ddd_lab_2.Interfaces;
using System.Collections.Generic;

namespace ddd_lab_2.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public IOrder CreateUsualOrder(int id, int price, string status, List<string> goods, string address, string maxDeliveryTime)
        {
            return new UsualOrder()
            {
                Id = id,
                Price = price,
                Status = status,
                Goods = goods,
                Address = address,
                MaxDeliveryTime = maxDeliveryTime
            };
        }

        public IOrder CreateExpressOrder(int id, int price, string status, List<string> goods, string address)
        {
            price += 500;

            return new ExpressOrder()
            {
                Id = id,
                Price = price,
                Status = status,
                Goods = goods,
                Address = address
            };
        }       
    }
}
