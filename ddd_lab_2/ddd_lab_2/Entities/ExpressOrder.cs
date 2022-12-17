using ddd_lab_2.Interfaces;
using System.Collections.Generic;

namespace ddd_lab_2.Entities
{
    internal class ExpressOrder : IOrder
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public List<string> Goods { get; set; }
        public string Address { get; set; }
        public string MaxDeliveryTime { get; set; } = "2h";
    }
}
