using System.Collections.Generic;

namespace FishGame
{
    public class Game
    {
		public static IGameObject[,] LevelMap { get; private set; }
		public static int MapHeight { get; private set; }
		public static int MapWidth { get; private set; }

		private static Player player;

		public Game(Map map)
		{
            LevelMap = map.GetMap();
			player = FindPlayer();
			MapHeight = map.Height;
			MapWidth = map.Width;
			ScrollMap.Update();
		}

		public static Player FindPlayer()
		{
			foreach (var position in LevelMap)
				if (position is Player playerPosition)
					player = playerPosition;
			return player;
		}

		public void Update(int horizontal, int vertical)
		{
			if (!player.death)
				player.Move(horizontal, vertical);
			MapToList().ForEach(element => element.Act());
		}

		private List<IGameObject> MapToList()
		{
			var list = new List<IGameObject>();
			foreach (var position in LevelMap) 
				list.Add(position);
			return list;
		}
	}
}
