using Battle.Entities;

namespace Battle.Interfaces
{
    public interface IFactory
    {
        public ElvesArmy CreateElvesArmy();
        public OrcsArmy CreateOrcsArmy();
    }
}
