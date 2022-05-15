using System.Drawing;
using static FishGame.ScrollMap;

namespace FishGame
{
	public class AnimationState
	{
		public Point PreviousPosition;
		public Point CurrentPosition;
		public int Dx;
		public int Dy;

		public void UpdatePosition(int scale)
		{
			scale = scale == 0 ? 8 : scale;
			CurrentPosition = new Point( 
				PreviousPosition.X + Dx * scale - Start.X * 100, 
				PreviousPosition.Y + Dy * scale - Start.Y * 100);
		}
	}
}
