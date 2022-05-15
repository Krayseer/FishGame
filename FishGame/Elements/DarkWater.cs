using System;
using System.Drawing;

namespace FishGame
{
    public class DarkWater : IGameObject
    {
		public Point Position { get; private set; }

		public DarkWater() { }

		public DarkWater(Point objectPosition) => Position = objectPosition;

		public DarkWater(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public string GetImageName() => "DarkWater";

		public bool IsBackground() => false;

		public string GetNameBackground() => throw new NotImplementedException();
	}
}
