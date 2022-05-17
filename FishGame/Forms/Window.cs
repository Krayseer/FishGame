using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections;
using static FishGame.Properties.Resources;

namespace FishGame
{
    public partial class Window : Form
    {
		private readonly Game game;
		private readonly Timer timer = new Timer();
		private ProgressBar Energy;

		private bool enableEscape = true;
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

			var images = ResourceManager
					   .GetResourceSet(CultureInfo.CurrentCulture, true, true)
					   .Cast<DictionaryEntry>()
					   .Where(x => x.Value.GetType() == typeof(Bitmap))
					   .Select(x => new { Name = x.Key.ToString(), Image = x.Value })
					   .ToList();
			images.ForEach(x => bitmaps[x.Name] = (Bitmap)x.Image);

			InitialiseElements();
			TimerRun();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawImage(bitmaps["Background"], new Point(0, 0));
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
				if (player.OnStay is Finish)
				{
					player.Delete(0);
					if (LevelManager.LevelNumber == LevelManager.levels.Count - 1)
						CreateWindow(ImageFinal, "Победа");
                    else
						CreateWindow(ImageLevelNext, "Продолжить");
				}
				if (player.death && !player.changeLevel)
					CreateWindow(ImageRestart, "Заново");
			}
			Invalidate();
		}

		protected override void OnKeyDown(KeyEventArgs e)
        {
			keysPressed.Add(e.KeyCode);
			if (e.KeyCode == Keys.Escape && enableEscape)
				SwitchToMenu();
		}

		protected override void OnKeyUp(KeyEventArgs e) => keysPressed.Remove(e.KeyCode);

        private void InitialiseElements()
		{
			var energyBox = new TextBox();
			SetSettingsText(energyBox, "энергия");
			energyBox.SetBounds(1100, 10, 70, 20);

			var escBox = new TextBox();
			SetSettingsText(escBox, "ESC - выход в главное меню");
			escBox.SetBounds(1110, 37, 250, 20);

			Energy = new ProgressBar();
			Energy.SetBounds(1170, 10, 200, 24);
			Energy.ForeColor = Color.Green;
			Controls.Add(Energy);
		}

        private void TimerRun()
		{
			timer.Interval = 50;
			timer.Tick += TimerTick;
			timer.Start(); 
		}

		private void CreateWindow(Bitmap image, string text)
        {
			enableEscape = false;

			var button = new Button();
			SetSettingsButton(button, text);
			button.SetBounds(570, 370, 400, 50);
			button.Visible = text != "Победа";
			if (text == "Заново")
				button.Click += (x, y) => Close();
			else
				button.Click += (x, y) =>
				{
					LevelManager.NextLevel();
					Close();
				};

			var menuButton = new Button();
			SetSettingsButton(menuButton, "Главное меню");
			menuButton.Click += (x, y) => SwitchToMenu();
			if (text == "Победа")
				menuButton.SetBounds(640, 500, 400, 50);
			else
				menuButton.SetBounds(570, 450, 400, 50);

			var picture = new PictureBox
			{
				Image = image,
				Location = text == "Победа" ? new Point(320, 100) : new Point(520, 250),
				Size = text == "Победа" ? new Size(1200, 700) : new Size(500, 350),
				BackColor = Color.Transparent
			};
			Controls.Add(picture);
		}

		private void SetSettingsButton(Button button, string text)
		{
			button.Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
			button.Text = text;
			button.ForeColor = Color.Blue;
			Controls.Add(button);
		}

		private void SetSettingsText(TextBox textBox, string text)
        {
			textBox.Text = text;
			textBox.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Bold);
			textBox.BackColor = Color.White;
			textBox.ForeColor = Color.Black;
			textBox.Enabled = false;
			Controls.Add(textBox);
		}

		private void SwitchToMenu()
        {
			LevelManager.end = true;
			Close();
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

