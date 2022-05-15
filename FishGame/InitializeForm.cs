using System.Drawing;
using System.Windows.Forms;
using static FishGame.Properties.Resources;

namespace FishGame
{
	public static class InitialiseForm
	{
		public static void InitialiseFormSettings(this Form form)
		{
			form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			form.WindowState = FormWindowState.Maximized;
			form.ClientSize = new Size(1500, 750);
			form.Icon = FishIcon;
			form.Text = "Приключения рыбки";
			form.KeyPreview = true;
		}
	}
}
