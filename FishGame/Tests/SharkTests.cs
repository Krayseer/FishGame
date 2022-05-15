using NUnit.Framework;

namespace FishGame
{
	[TestFixture]
	class SharksTests
	{
		[Test]
		public void CheckOnFall()
		{
			var map = new Game(new Map(new string[] { "ss ",
													  "dd ",
													  "ddp"}));
			map.Update(-1, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is Shark);
			map.Update(1, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 2] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 2] is Shark);
			Assert.IsTrue(Game.LevelMap[2, 2] is Player);
		}

		[Test]
		public void CheckOnCrash()
		{
			var map = new Game(new Map(new string[] { "ss ",
													  "dd ",
													  "ddp"}));
			map.Update(-1, 0);
			map.Update(0, 0);
			map.Update(0, -1);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[0, 2] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 2] is DarkWater);
		}

		[Test]
		public void TestNotFall()
		{
			var map = new Game(new Map(new string[] { "ss ",
													  "dd ",
													  "dd ",
													  "dd ",
													  "dd ",
													  "ddp"}));
			map.Update(-1, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			map.Update(-1, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			map.Update(0, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
		}

		[Test]
		public void TestNotFallSecond()
		{
			var map = new Game(new Map(new string[] { "ss ",
													  "dd ",
													  "   ",
													  "ddp"}));
			map.Update(-1, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			map.Update(-1, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
			map.Update(0, 0);
			map.Update(0, 0);
			Assert.IsTrue(Game.LevelMap[0, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[1, 0] is Shark);
			Assert.IsTrue(Game.LevelMap[0, 1] is DarkWater);
			Assert.IsTrue(Game.LevelMap[1, 1] is DarkWater);
		}
	}
}
