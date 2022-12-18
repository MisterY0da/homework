using Battle.Factories;
using Battle.Interfaces;

IFactory factory = new ArmyFactory();

var elvenArmy = factory.CreateElvesArmy();
var orcsArmy = factory.CreateOrcsArmy();

List<ICharacter> characters = new List<ICharacter>();
characters.Add(orcsArmy.Archer);
characters.Add(elvenArmy.Archer);
characters.Add(orcsArmy.Swordsman);
characters.Add(elvenArmy.Swordsman);
characters.Add(orcsArmy.Cleric);
characters.Add(elvenArmy.Cleric);

var rand = new Random();
for (int i = 0; i < 20; i++)
{
    var characterOne = characters[rand.Next(0,characters.Count)];
    var characterTwo = characters[rand.Next(0, characters.Count)];
    characterOne.InteractWith(characterTwo);    
}
