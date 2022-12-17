using System;
using System.Collections.Generic;
using System.Text;

namespace ddd_lab_2.Interfaces
{
    public interface IOrder
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public List<string> Goods { get; set; }
        public string Address { get; set; }
        public string MaxDeliveryTime { get; set; }
    }
}
