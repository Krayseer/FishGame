using System.Drawing;

namespace FishGame
{
    public interface IGameObject
    {
        void Act();
        string GetImageName();
        Point Position { get; }
        bool IsBackground();
        string GetNameBackground();
    }
}
