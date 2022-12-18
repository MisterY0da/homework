namespace Battle.Entities
{
    public class ElvesArmy
    {
        public Archer Archer { get; set; } = new Archer() { Race = "Elven" };
        public Cleric Cleric { get; set; } = new Cleric() { Race = "Elven" };
        public Swordsman Swordsman { get; set; } = new Swordsman() { Race = "Elven" };
    }
}
