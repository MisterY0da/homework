using Battle.Entities;
using Battle.Interfaces;

namespace Battle.Factories
{
    public class ArmyFactory : IFactory
    {
        public ElvesArmy CreateElvesArmy()
        {
            return new ElvesArmy();
        }

        public OrcsArmy CreateOrcsArmy()
        {
            return new OrcsArmy();
        }
    }
}
