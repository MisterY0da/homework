namespace KursachPatterns.Interfaces
{
    public interface ICar
    {
        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public void StartMoving();

        public void StopMoving();
    }
}
