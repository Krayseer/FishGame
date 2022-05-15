using NUnit.Framework;

namespace FishGame
{
	[TestFixture]
	class GameTests
	{
		[Test]
		public void CreateMap()
		{
			new Game(new Map(new string[] { "pd",
											"g "}));
			Assert.IsTrue(Game.LevelMap[0, 0] is Player);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is Grass);
			Assert.IsTrue(Game.LevelMap[1, 1] is Water);
		}

		[Test]
		public void ChangeMap()
		{
			new Game(new Map(new string[] { "pd",
											"  "}));
			var player = (Player)Game.LevelMap[0, 0];
			player.Move(1, 0);
			player.Move(0, 1);
			Assert.IsTrue(Game.LevelMap[0, 0] is Water);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is Water);
			Assert.IsTrue(Game.LevelMap[1, 1] is Player);
		}

		[Test]
		public void UpdateMap()
		{
			var map = new Game(new Map(new string[] { "pd",
													  "  "}));
			map.Update(1,1);
			Assert.IsTrue(Game.LevelMap[0, 0] is Water);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is Water);
			Assert.IsTrue(Game.LevelMap[1, 1] is Player);
		}

		[Test]
		public void SharksOnMap()
		{
			var map = new Game(new Map(new string[] { "ss ",
													  "dd ",
													  "ddp"}));
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
		}

		[Test]
		public void FoodOnMap()
		{
			var map = new Game(new Map(new string[] { "ff",
													  "p "}));
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Food);
			Assert.IsTrue(Game.LevelMap[1, 0] is Food);
		}

		[Test]
		public void MultipleTest()
		{
			var map = new Game(new Map(new string[] { "  ss",
													  "  dd",
													  "pfdd",
													  "g d "}));
			var player = (Player)Game.LevelMap[0, 2];
			Assert.IsTrue(Game.LevelMap[2, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[3, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 2] is Player);
			Assert.IsTrue(Game.LevelMap[1, 2] is Food);
			Assert.IsTrue(Game.LevelMap[0, 3] is Grass);
			map.Update(0, 1);
			Assert.IsTrue(Game.LevelMap[2, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[3, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 2] is Player);
			Assert.IsTrue(Game.LevelMap[1, 2] is Food);
			Assert.IsTrue(Game.LevelMap[0, 3] is Grass);
			map.Update(1, 0);
			map.Update(1, 0);
			map.Update(1, 0);
			map.Update(0, 1);
			Assert.IsTrue(Game.LevelMap[2, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[3, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[2, 2] is Shark);
			Assert.IsTrue(Game.LevelMap[3, 1] is Shark);
			Assert.IsTrue(Game.LevelMap[3, 3] is Player);
			Assert.IsTrue(Game.LevelMap[1, 2] is Food);
			Assert.IsTrue(Game.LevelMap[0, 3] is Grass);
			map.Update(0, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[2, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[3, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[2, 3] is DarkWater);
			Assert.IsTrue(Game.LevelMap[3, 2] is DarkWater);
			Assert.IsTrue(Game.LevelMap[3, 3] is Player);
			Assert.IsTrue(Game.LevelMap[1, 2] is Food);
			Assert.IsTrue(Game.LevelMap[0, 3] is Grass);
		}
	}
}
