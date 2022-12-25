namespace BridgePattern.Interfaces
{
    public interface IShape
    {
        public IColor Color { get; set; }
        public ISize Size { get; set; }
        public void GetShape();
    }
}
