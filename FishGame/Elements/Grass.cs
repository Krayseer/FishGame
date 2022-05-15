using System.Drawing;

namespace FishGame
{
    public class Grass : IGameObject
    {
		public Point Position { get; private set; }

		public Grass(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public string GetImageName() => "Grass";

		public bool IsBackground() => true;

		public string GetNameBackground() => new Water().GetImageName();
	}
}
