using ddd_lab_2.Interfaces;

namespace ddd_lab_2.Entities
{
    public class ForeignBuyer : IBuyer
    {
        public string Name { get; set; }
        public string CardType { get; set; } = "VISA";
        public string PaymentMethod { get; set; } = "PayPal";
    }
}
