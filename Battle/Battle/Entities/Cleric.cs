using Battle.Interfaces;

namespace Battle.Entities
{
    public class Cleric : ICharacter
    {
        public int HP { get; set; } = 50;
        public string Race { get; set; }
        public string Class { get; set; } = "cleric";

        public void InteractWith(ICharacter character)
        {
            if (character.Race.Equals(Race))
            {
                character.HP += 10;
                Console.WriteLine(Race + " " + Class + " healed " + character.Race + " " + character.Class);
                Console.WriteLine(character.Race + " " + character.Class + " HP is now: " + character.HP);
                Console.WriteLine("------------------------------");
            }
            else
            {
                character.HP -= 5;
                Console.WriteLine(Race + " " + Class + " poisoned " + character.Race + " " + character.Class);
                Console.WriteLine(character.Race + " " + character.Class + " HP is now: " + character.HP);
                Console.WriteLine("------------------------------");
            }
        }
    }
}
