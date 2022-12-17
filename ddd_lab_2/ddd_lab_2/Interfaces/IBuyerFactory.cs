namespace ddd_lab_2.Interfaces
{
    public interface IBuyerFactory
    {
        public IBuyer CreateRussianBuyer(string name);
        public IBuyer CreateForeignBuyer(string name);
    }
}
