using BridgePattern.Interfaces;

namespace BridgePattern.Colors
{
    internal class RedColor : IColor
    {
        public string GetColor()
        {
            return "красный";
        }
    }
}
