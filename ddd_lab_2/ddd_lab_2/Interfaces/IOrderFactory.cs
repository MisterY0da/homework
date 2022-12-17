using System.Collections.Generic;

namespace ddd_lab_2.Interfaces
{
    public interface IOrderFactory
    {
        public IOrder CreateUsualOrder(int id, int price, string status, List<string> goods, string address, string maxDeliveryTime);
        public IOrder CreateExpressOrder(int id, int price, string status, List<string> goods, string address);
    }
}
