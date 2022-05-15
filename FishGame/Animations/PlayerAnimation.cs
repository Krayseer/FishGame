﻿using System.Collections.Generic;

namespace FishGame
{
	public enum Direction
	{
		Stay,
		Forward, 
		Back,
		Right,
		Left
	}

	public class PlayerAnimation
    {
		private static Direction direction;
		private static readonly int speed = 5;
		private static int iteration = 0;
		private static int index = 0;
		private static readonly Dictionary<Direction, List<string>> State =
			new Dictionary<Direction, List<string>>
			{
				{ Direction.Stay, new List<string>{ "s" } }, 
				{ Direction.Forward, new List<string>{ "f1", "f2"} },
				{ Direction.Back, new List<string>{ "b1", "b2" } },
				{ Direction.Left, new List<string>{ "l1", "l2" } },
				{ Direction.Right, new List<string>{ "r1", "r2"} }
			};

		public static void SetDirection(int horizontal, int vertical)
		{
			if (horizontal == vertical) 
				direction = Direction.Stay;

			switch (horizontal)
            {
				case 1:
					direction = Direction.Right;
					break;
				case -1:
					direction = Direction.Left;
					break;

            }

            switch (vertical)
            {
				case 1:
					direction = Direction.Back;
					break;
				case -1:
					direction = Direction.Forward;
					break;
            }
		}

		public static string GetImageName()
		{
			if (direction == Direction.Stay) 
				return State[direction][0];
			StartIterations();
			return State[direction][index];
		}

		private static void StartIterations()
		{
			iteration++;
			if (iteration >= speed)
			{
				iteration = 0;
				index++;
				index = index > 1 ? 0 : index;
			}
		}
	}
}
