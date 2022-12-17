using ddd_lab_2.Interfaces;

namespace ddd_lab_2.Entities
{
    public class RussianBuyer : IBuyer
    {
        public string Name { get; set; }
        public string CardType { get; set; } = "MIR";
        public string PaymentMethod { get; set; } = "YooMoney";
    }
}
