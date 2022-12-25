using BridgePattern.Interfaces;
namespace BridgePattern.Sizes
{
    public class BigSize : ISize
    {
        public string GetSize()
        {
            return "большой";
        }
    }
}
