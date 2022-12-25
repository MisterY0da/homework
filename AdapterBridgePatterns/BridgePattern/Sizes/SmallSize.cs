using BridgePattern.Interfaces;
namespace BridgePattern.Sizes
{
    public class SmallSize : ISize
    {
        public string GetSize()
        {
            return "маленький";
        }
    }
}
