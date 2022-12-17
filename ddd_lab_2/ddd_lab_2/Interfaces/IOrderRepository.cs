namespace ddd_lab_2.Interfaces
{
    public interface IOrderRepository
    {
        void Add(IOrder item);
        IOrder GetOrderById(int id);
    }
}
