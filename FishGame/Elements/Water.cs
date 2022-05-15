using System;
using System.Drawing;

namespace FishGame
{
    public class Water : IGameObject
    {
		public Point Position { get; private set; }

		public Water() { }

		public Water(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public string GetImageName() => "Water.bmp";
		
		public bool IsBackground() => false;

		public string GetNameBackground() => throw new NotImplementedException();
	}
}
