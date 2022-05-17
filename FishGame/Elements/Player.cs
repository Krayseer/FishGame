using System;
using System.Drawing;
using static FishGame.PlayerAnimation;
using static FishGame.Game;
using static System.Math;

namespace FishGame
{
    public class Player : IGameObject, IAnim
    {
		public Point Position { get; private set; }
		public IGameObject OnStay { get; private set; }
		public float Energy { get; private set; }
		public bool IsAnimation { get; private set; }
		public Point PositionOnWindow { get; set; }

		public float speedEnergy = 0.9f;

		public int speed = 50;

		public bool death = false;

		public bool changeLevel = false;

		public Player(int x, int y)
		{
			Position = new Point(x, y);
			OnStay = new Water(x, y);
			Energy = 100;
		}

		public void Move(int horizontal, int vertical)
		{
			IsAnimation = false;
			CheckCorrectMoves(horizontal, vertical);
			var newX = Position.X + horizontal;
			var newY = Position.Y + vertical;
			if (CheckCorrectCoordinates(newX, newY) && CheckOnCollision(newX, newY))
			{
				if (horizontal != 0 || vertical != 0)
				{
					Energy -= speedEnergy;
					IsAnimation = true;
				}
				MoveAround(newX, newY);
				SetDirection(horizontal, vertical);
			}
		}

		public void Act()
		{
            if (OnStay is Food player)
                player.AddEnergy();
			if (Energy > 100) 
				Energy = 100;
			else if (Energy < 0) 
				Delete(1);
		}

		public string GetImageName() => PlayerAnimation.GetImageName();

		public void Delete(int number) // 0 - переход на следующий уровень, 1 - перезапуск уровня
		{
			LevelMap[Position.X, Position.Y] = number == 0 ? OnStay : new PlayerDead(Position.X, Position.Y);
			changeLevel = number == 0;
			death = true;
			IsAnimation = false;
		}

		public void ChangeEnergy(float energy) => Energy += energy;

		public bool IsBackground() => true;

		public string GetNameBackground() => OnStay.GetImageName();

		private bool CheckOnCollision(int posX, int posY) =>
			!(LevelMap[posX, posY] is Shark || LevelMap[posX, posY] is Grass);

		private bool CheckCorrectCoordinates(int posX, int posY) =>
			posX < MapWidth && posX >= 0 && posY < MapHeight && posY >= 0;

		private void CheckCorrectMoves(int horizontal, int vertical)
		{
			if (Abs(horizontal) != 0 && Abs(horizontal) != 1 && Abs(vertical) != 0 && Abs(vertical) != 1)
				throw new FormatException();
		}

		private void MoveAround(int moveX, int moveY)
		{
			LevelMap[Position.X, Position.Y] = OnStay;
			OnStay = LevelMap[moveX, moveY];
			LevelMap[moveX, moveY] = this;
			Position = new Point(moveX, moveY);
		}
	}
}
