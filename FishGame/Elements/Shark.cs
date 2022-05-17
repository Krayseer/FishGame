using System;
using System.Drawing;
using static FishGame.Game;
using static System.Math;

namespace FishGame
{
	public class Shark : IGameObject, IAnim
	{
		public Shark(int x, int y) => Position = new Point(x, y);

		public Point Position { get; private set; }

		public bool IsAnimation { get; private set; }

		public Point PositionOnWindow { get; set; }

		private int Height = 5;

		private bool isFall = false;

		public void Act()
		{
			IsAnimation = false;
            switch (isFall)
            {
				case false:
					CheckOnFall();
					break;
				case true:
					Fall();
					break;
            }
		}

		public string GetImageName() => "Shark";

		public bool IsBackground() => true;

		public string GetNameBackground() => new DarkWater().GetImageName();

		private void Fall()
		{
			int x = Position.X;
			int nextY = Position.Y + 1;

			if (nextY >= MapHeight || !(LevelMap[x, nextY] is DarkWater || LevelMap[x, nextY] is Player))
			{
				Death();
				return;
			}
			if (Height > 0)
				if (LevelMap[x, nextY] is DarkWater)
				{
					Height--;
					LevelMap[x, nextY] = this;
					LevelMap[x, Position.Y] = new DarkWater(Position);
					Position = new Point(x, nextY);
					IsAnimation = true;
				}
				else
				{
					var player = (Player)LevelMap[x, nextY];
					if (player.OnStay is DarkWater)
						player.Delete(1);
					Death();
				}
			else 
				Death();
		}

		private void CheckOnFall()
		{
			int i = Position.X;
			for (int j = Position.Y + 1; j < Min(Position.Y + Height, MapHeight); j++)
			{
				if (LevelMap[i, j] is DarkWater || LevelMap[i, j] is Player)
				{
					if (LevelMap[i, j] is Player)
					{
						var player = (Player)LevelMap[i, j];
						if (player.OnStay is DarkWater)
							isFall = true;
					}
				}
				else break;
			}
		}

		private void Death()
		{
			LevelMap[Position.X, Position.Y] = new DarkWater(Position);
			IsAnimation = false;
		}
	}
}
