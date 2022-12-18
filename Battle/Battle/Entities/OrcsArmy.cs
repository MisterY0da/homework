namespace Battle.Entities
{
    public class OrcsArmy
    {
        public Archer Archer { get; set; } = new Archer() { Race = "Orc" };
        public Cleric Cleric { get; set; } = new Cleric() { Race = "Orc" };
        public Swordsman Swordsman { get; set; } = new Swordsman() { Race = "Orc" };
    }
}
