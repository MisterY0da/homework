namespace ddd_lab_2.Interfaces
{
    public interface IBuyerRepository
    {
        void Add(IBuyer item);
        IBuyer GetBuyerByName(string name);
    }
}
