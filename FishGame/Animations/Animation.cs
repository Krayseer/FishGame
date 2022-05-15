using System.Collections.Generic;
using System.Drawing;
using static FishGame.Game;

namespace FishGame
{
	public class Animation
	{
		public const int FramesCount = 8;
		public static Dictionary<IAnim, AnimationState> Anim = new Dictionary<IAnim, AnimationState>();

		public static void CheckAnimation()
		{
			Anim.Clear();
			for (int i = 0; i < MapWidth; i++)
				for (int j = 0; j < MapHeight; j++)
                    if (LevelMap[i, j] is IAnim anim)
                        Anim[anim] = new AnimationState { PreviousPosition = new Point(i * 100, j * 100) };
		}

		public static void UpdateAnimation()
		{
			var anim = new Dictionary<IAnim, AnimationState>();
			foreach (var key in Anim.Keys)
				if (key.IsAnimation)
				{
					var animationElement = (IGameObject)key;
					Anim[key].Dx = (animationElement.Position.X * 100 - Anim[key].PreviousPosition.X) / FramesCount;
					Anim[key].Dy = (animationElement.Position.Y * 100 - Anim[key].PreviousPosition.Y) / FramesCount;
					anim.Add(key, Anim[key]);
				}
			Anim = anim;
		}
	}
}
