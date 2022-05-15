using System.Drawing;

namespace FishGame
{
    public interface IAnim
    {
        bool IsAnimation { get; }
        Point PositionOnWindow { get; set; }
    }
}
