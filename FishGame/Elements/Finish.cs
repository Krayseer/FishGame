using System.Drawing;

namespace FishGame
{
    public class Finish : IGameObject
    {
		public Point Position { get; private set; }

		public Finish(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public string GetImageName() => "Finish.bmp";

		public string GetNameBackground() => new Water().GetImageName();

		public bool IsBackground() => true;
	}
}
