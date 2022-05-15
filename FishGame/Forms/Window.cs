using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace FishGame
{
    public partial class Window : Form
    {
		private readonly Game game;
		private readonly Timer timer = new Timer();
		private ProgressBar Energy;

		private int time;
		private int horizontalComponent;
		private int verticalComponent;

		private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
		private readonly HashSet<Keys> keysPressed = new HashSet<Keys>();
		private readonly Dictionary<Keys, int> keys = new Dictionary<Keys, int>
		{
			{ Keys.W, -1},
			{ Keys.S, 1},
			{ Keys.A, -1},
			{ Keys.D, 1}
		};

		public Window(Game game)
		{
			this.game = game;
			InitialiseForm.InitialiseFormSettings(this);
			DoubleBuffered = true;

			FormClosing += (x, y) =>
			{
				timer.Stop();
				keysPressed.Clear();
				File.WriteAllText("lvl.txt", LevelManager.LevelNumber.ToString());
			};

			bitmaps["b1.bmp"] = Properties.Resources.b1;
			bitmaps["s.bmp"] = Properties.Resources.s;
			bitmaps["b2.bmp"] = Properties.Resources.b2;
			bitmaps["r1.bmp"] = Properties.Resources.r1;
			bitmaps["r2.bmp"] = Properties.Resources.r2;
			bitmaps["l1.bmp"] = Properties.Resources.l1;
			bitmaps["l2.bmp"] = Properties.Resources.l2;
			bitmaps["f1.bmp"] = Properties.Resources.f1;
			bitmaps["f2.bmp"] = Properties.Resources.f2;
			bitmaps["Background.bmp"] = Properties.Resources.Background;
			bitmaps["BackgroundMenu.bmp"] = Properties.Resources.BackgroundMenu;
			bitmaps["Water.bmp"] = Properties.Resources.Water;
			bitmaps["DarkWater.bmp"] = Properties.Resources.DarkWater;
			bitmaps["Finish.bmp"] = Properties.Resources.Finish;
			bitmaps["Food.bmp"] = Properties.Resources.Food;
			bitmaps["Grass.bmp"] = Properties.Resources.Grass;
			bitmaps["Shark.bmp"] = Properties.Resources.Shark;





			InitialiseElements();
			TimerRun();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawImage(bitmaps["Background.bmp"], new Point(0, 0));
			DrawBackground(e);
			DrawStaticObjects(e);
			DrawDynamicObjects(e);
			e.Graphics.ResetTransform();
		}

		private void TimerTick(object sender, EventArgs args)
		{
			if (time++ == 0)
			{
				Animation.CheckAnimation();
				horizontalComponent = verticalComponent = 0;
				GetDirection();
				game.Update(horizontalComponent, verticalComponent);
				timer.Interval = Game.FindPlayer().speed;
				ScrollMap.Update();
				Animation.UpdateAnimation();
				Energy.Value = (int)Game.FindPlayer().Energy;
			}

			if (time == 8)
			{
				time = 0;
				var player = Game.FindPlayer();
				if (player.OnStay is Finish || player.death)
				{
					if (player.OnStay is Finish) 
						LevelManager.NextLevel();
					Close();
				}
			}
			Invalidate();
		}

		protected override void OnKeyDown(KeyEventArgs e) => keysPressed.Add(e.KeyCode);

		protected override void OnKeyUp(KeyEventArgs e) => keysPressed.Remove(e.KeyCode);

        private void InitialiseElements()
		{
			var textEnergy = new TextBox()
			{
				Text = "энергия",
				BackColor = Color.White,
				ForeColor = Color.Black,
				Enabled = false
			};
			textEnergy.SetBounds(1100, 10, 50, 20);
			Controls.Add(textEnergy);

			Energy = new ProgressBar();
			Energy.SetBounds(1150, 10, 200, 20);
			Energy.ForeColor = Color.Green;
			Controls.Add(Energy);

			var button = new Button()
			{
				Text = "главное меню",
				ForeColor = Color.Blue
			};
			button.SetBounds(1355, 10, 100, 20);
			button.Click += (x, y) => LevelManager.end = true;
			Controls.Add(button);
		}

        private void TimerRun()
		{
			timer.Interval = 50;
			timer.Tick += TimerTick;
			timer.Start();
		}

		private void GetDirection()
		{
			if (keysPressed.Count() != 0)
			{
				horizontalComponent = keysPressed.Last() == Keys.A || keysPressed.Last() == Keys.D ?
					keys[keysPressed.Last()] : 0;
				verticalComponent = keysPressed.Last() == Keys.W || keysPressed.Last() == Keys.S ?
					keys[keysPressed.Last()] : 0;
			}
		}

		private void DrawDynamicObjects(PaintEventArgs e)
		{
			foreach (var anim in Animation.Anim)
			{
				var element = (IGameObject)anim.Key;
				anim.Value.UpdatePosition(time);
				e.Graphics.DrawImage(bitmaps[element.GetImageName()], anim.Value.CurrentPosition);
			}
		}

		private void DrawStaticObjects(PaintEventArgs e)
		{
			for (int i = ScrollMap.Start.X; i < ScrollMap.End.X; i++)
				for (int j = ScrollMap.Start.Y; j < ScrollMap.End.Y; j++)
					if (Game.LevelMap[i, j].IsBackground() && 
						(!(Game.LevelMap[i, j] is IAnim) || !((IAnim)Game.LevelMap[i, j]).IsAnimation))
						DrawImage(e, Game.LevelMap[i, j].GetImageName(),
							new Point(i - ScrollMap.Start.X, j - ScrollMap.Start.Y));
		}

		private void DrawBackground(PaintEventArgs e)
		{
			for (int i = ScrollMap.Start.X; i < ScrollMap.End.X; i++)
				for (int j = ScrollMap.Start.Y; j < ScrollMap.End.Y; j++)
                {
					if (Game.LevelMap[i, j].IsBackground())
					{
						if (Game.LevelMap[i, j] is Player player)
							if (player.OnStay is Food || player.OnStay is Finish)
								DrawImage(e, player.OnStay.GetNameBackground(),
									new Point(i - ScrollMap.Start.X, j - ScrollMap.Start.Y));

						DrawImage(e, Game.LevelMap[i, j].GetNameBackground(),
							new Point(i - ScrollMap.Start.X, j - ScrollMap.Start.Y));

						bitmaps[Game.LevelMap[i, j].GetImageName()].MakeTransparent(Color.White);
					}
					else
						DrawImage(e, Game.LevelMap[i, j].GetImageName(),
							new Point(i - ScrollMap.Start.X, j - ScrollMap.Start.Y));
				}
		}

		private void DrawImage(PaintEventArgs e, string name, Point position) =>
			e.Graphics.DrawImage(bitmaps[name], new Point(position.X * 100, position.Y * 100));
	}
}

