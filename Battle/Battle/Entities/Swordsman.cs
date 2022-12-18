using Battle.Interfaces;

namespace Battle.Entities
{
    public class Swordsman : ICharacter
    {
        public int HP { get; set; } = 40;
        public string Race { get; set; }
        public string Class { get; set; } = "swordsman";

        public void InteractWith(ICharacter character)
        {
            if (!character.Race.Equals(Race))
            {
                character.HP -= 15;
                Console.WriteLine(Race + " " + Class + " attacked " + character.Race + " " + character.Class);
                Console.WriteLine(character.Race + " " + character.Class + " HP is now: " + character.HP);
                Console.WriteLine("------------------------------");
            }
        }
    }
}
