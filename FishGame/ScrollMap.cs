using System.Drawing;
using static FishGame.Game;
using static System.Math;

namespace FishGame
{
    public class ScrollMap
    {
		private const int Width = 14;
		private const int Height = 8;
		public static Point Start { get; private set; }
		public static Point End { get; private set; }

		public static void Update()
		{
			var player = FindPlayer();

			var startX = Max(player.Position.X - Width / 2, 0);
			var endX = Min(startX + Width, MapWidth);
			var startY = Max(player.Position.Y - Height / 2, 0);
			var endY = Min(startY + Height, MapHeight);

			startX = endX == MapWidth && MapWidth > Width ? 
				startX + MapWidth - player.Position.X - Width / 2 : startX;
			startY = endY == MapHeight && MapHeight > Height ? 
				startY + MapHeight - player.Position.Y - Height / 2 + 1 : startY;

			Start = new Point(startX, startY);
			End = new Point(endX, endY);
		}
	}
}
