using NUnit.Framework;

namespace FishGame
{
	[TestFixture]
	class PlayerTests
	{
		[Test]
		public void OnMove()
		{
			new Game(new Map(new string[] { "p ",
											"  "}));
			var player = (Player)Game.LevelMap[0, 0];
			player.Move(1, 0);
			Assert.AreEqual(player.Position.X, 1);
			Assert.AreEqual(player.Position.Y, 0);
			Assert.IsTrue(Game.LevelMap[1, 0] is Player);
		}

		[Test]
		public void MoveToEnd()
		{
			new Game(new Map(new string[] { "p ",
											"  "}));
			var player = (Player)Game.LevelMap[0, 0];
			player.Move(1, 0);
			player.Move(1, 0);
			player.Move(1, 0);
			Assert.AreEqual(player.Position.X, 1);
			Assert.AreEqual(player.Position.Y, 0);
			player.Move(0, 1);
			player.Move(0, 1);
			player.Move(0, 1);
			Assert.AreEqual(player.Position.X, 1);
			Assert.AreEqual(player.Position.Y, 1);
			player.Move(-1, -1);
			player.Move(-1, -1);
			player.Move(-1, -1);
			Assert.AreEqual(player.Position.X, 0);
			Assert.AreEqual(player.Position.Y, 0);
			Assert.IsTrue(Game.LevelMap[player.Position.X, player.Position.Y] is Player);
		}

		[Test]
		public void OnEnergy()
		{
			var map = new Game(new Map(new string[] { "p ",
													  "  "}));
			var player = (Player)Game.LevelMap[0, 0];
			map.Update(1, 0);
			Assert.AreEqual(player.Energy, 100 - player.speedEnergy);
			map.Update(0, 0);
			Assert.AreEqual(player.Energy, 100 - player.speedEnergy);
		}

		[Test]
		public void GetEnergy()
		{
			var map = new Game(new Map(new string[] { "pf",
													  "  "}));
			var player = (Player)Game.LevelMap[0, 0];
			var energy = player.Energy;
			map.Update(1, 0);
			Assert.AreNotEqual(player.Energy - player.speedEnergy, energy);
		}
	}
}