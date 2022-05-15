using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static FishGame.Levels;

namespace FishGame
{
	public class LevelManager
	{
		public static int LevelNumber { get; private set; }
		private static bool changeLevel = false;
		public static bool end = false;
		readonly static List<string[]> levels = new List<string[]>
		{
			LevelStart, LevelSecond, LevelThird, LevelFourth, LevelFifth, LevelSixth, LevelFinal
		};

		public static void NextLevel()
		{
			if (!changeLevel)
			{
				LevelNumber++;
				changeLevel = true;
			}
		}
		
		public static void Load(int level)
		{
            try
            {
				LevelNumber = level;
				while (!end)
				{
					Application.Run(new Window(new Game(new Map(levels[LevelNumber]))));
					changeLevel = false;
				}
			}
			catch (Exception)
            {
				Menu.play = false;
				return;
			}
		}
	}
}
