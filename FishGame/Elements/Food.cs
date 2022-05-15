using System;
using System.Drawing;

namespace FishGame
{
    public class Food : IGameObject
    {
		private bool canUse = true;
		public Point Position { get; private set; }

		public Food(int x, int y) => Position = new Point(x, y);

		public void Act() { }

		public void AddEnergy()
		{
			if (canUse)
			{
				canUse = false;
				var player = (Player)Game.LevelMap[Position.X, Position.Y];
				player.ChangeEnergy(new Random().Next(25, 80));
			}
		}

		public string GetImageName() => "Food";

		public bool IsBackground() => true;

		public string GetNameBackground() => new Water().GetImageName();
	}
}
