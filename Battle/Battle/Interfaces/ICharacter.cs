namespace Battle.Interfaces
{
    public interface ICharacter
    {
        public int HP { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public void InteractWith(ICharacter character);
    }
}
