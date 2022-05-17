using System.Collections.Generic;
using System.Drawing;

namespace FishGame
{
	public class PlayerDead : IGameObject
	{
		public Point Position { get; private set; }

		public PlayerDead(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public string GetImageName() => "dead";

		public bool IsBackground() => true;

		public string GetNameBackground() => new Water().GetImageName();
	}
}
