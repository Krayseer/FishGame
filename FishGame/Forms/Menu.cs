using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static FishGame.Properties.Resources;

namespace FishGame
{
    public partial class Menu : Form
    {
		public static int lvl = 0;
		public static bool play = false;
		public static bool сloseForm = false;

		public Menu()
        {
            InitializeComponent();
			InitialiseForm.InitialiseFormSettings(this);

			var continueButton = new Button();
			continueButton.SetBounds(3, 80, 200, 100);
			SetSettingsButton(continueButton, "Продолжить");
			continueButton.Click += (x, y) => StartGame(int.Parse(File.ReadAllText("lvl.txt")));

			var startButton = new Button();
			startButton.SetBounds(3, 185, 200, 100);
			SetSettingsButton(startButton, "Новая игра");
			startButton.Click += (x, y) => StartGame(0);

			var trainingButton = new Button();
			var trainingBox = new PictureBox();
			trainingButton.SetBounds(3, 290, 200, 100);
			CreateWindowFromButton(trainingButton, trainingBox, "Обучение", ImageTraining);

			var controlButton = new Button();
			var controlBox = new PictureBox();
			controlButton.SetBounds(3, 395, 200, 100);
			CreateWindowFromButton(controlButton, controlBox, "Управление", ImageControl);

			var authorButton = new Button();
			var authorBox = new PictureBox();
			authorButton.SetBounds(3, 500, 200, 100);
			CreateWindowFromButton(authorButton, authorBox, "Об авторе", ImageAuthor);

			var closedButton = new Button();
			closedButton.SetBounds(3, 605, 200, 100);
			SetSettingsButton(closedButton, "Выйти");
			closedButton.Click += (x, y) => ExitGame();
		}

		protected override void OnPaint(PaintEventArgs e) => e.Graphics.DrawImage(BackgroundMenu, new Point(0, 0));

		private void SetSettingsButton(Button button, string text)
		{
			button.Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
			button.Text = text;
			button.BackColor = Color.White;
			button.ForeColor = Color.Blue;
			Controls.Add(button);
		}

		private void SetSettingsPicture(PictureBox pictureBox, Image image)
		{
			pictureBox.Image = image;
			pictureBox.Location = new Point(350, 100);
			pictureBox.Size = new Size(1200, 700);
			pictureBox.BackColor = Color.Transparent;
			pictureBox.Visible = false;
		}

		private void CreateWindowFromButton(Button button, PictureBox picture, string text, Bitmap image)
        {
			SetSettingsButton(button, text);
			SetSettingsPicture(picture, image);
			button.Click += (x, y) => picture.Visible = picture.Visible != true;
			Controls.Add(picture);
			Controls.Add(button);
		}

		private void StartGame(int numberLevel)
        {
			lvl = numberLevel;
			play = true;
			LevelManager.end = false;
			Close();
		}

		private void ExitGame()
        {
			сloseForm = true;
			Application.Exit();
		}
	}
}
