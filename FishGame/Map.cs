using System;
using System.Collections.Generic;

namespace FishGame
{
    public class Map
    {
		private readonly string[] map;
		public int Height { get; set; }
		public int Width { get; set; }

		private readonly Dictionary<char, Func<int, int, IGameObject>> dictionary =
			new Dictionary<char, Func<int, int, IGameObject>>
			{
				{ 'p', (i, j) => new Player(i, j) },
				{ ' ', (i, j) => new Water(i, j)  },
				{ 'd', (i, j) => new DarkWater(i, j) },
				{ 's', (i, j) => new Shark(i, j) },
				{ 'g', (i, j) => new Grass(i, j) },
				{ 'f', (i, j) => new Food(i, j) },
				{ 'F', (i, j) => new Finish(i, j) }
			};
		public Map(string[] map)
		{
			this.map = map;
			Height = map.Length;
			Width = map[0].Length;
		}

		public IGameObject[,] GetMap()
		{
			var level = new IGameObject[Width, Height];
			for (int i = 0; i < Height; i++)
				for (int j = 0; j < Width; j++)
					level[j, i] = dictionary[map[i][j]](j, i);
			return level;
		}
	}
}
