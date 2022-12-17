using System;
using System.Collections.Generic;
using System.Text;

namespace ddd_lab_2.Interfaces
{
    public interface IBuyer
    {
        public string Name { get; set; }
        public string CardType { get; set; }
        public string PaymentMethod { get; set; }
    }
}
