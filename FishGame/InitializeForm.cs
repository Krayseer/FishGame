using System.Drawing;
using System.Windows.Forms;
using static FishGame.Properties.Resources;

namespace FishGame
{
	public static class InitialiseForm
	{
		public static void InitialiseFormSettings(this Form form)
		{
			form.FormBorderStyle = FormBorderStyle.None;
			form.WindowState = FormWindowState.Maximized;
			form.TopMost = true;
			form.Icon = FishIcon;
			form.Text = "Приключения рыбки";
			form.KeyPreview = true;
		}
	}
}
