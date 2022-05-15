using System.Drawing;
using System.Windows.Forms;
using static FishGame.Properties.Resources;

namespace FishGame
{
	public static class InitialiseForm
	{
		public static void InitialiseFormSettings(this Form form)
		{
			form.Bounds = new Rectangle(0, 0, Screen.FromControl(form).WorkingArea.Width, Screen.FromControl(form).WorkingArea.Height);
			form.Icon = FishIcon;
			form.Text = "Приключения рыбки";
			form.KeyPreview = true;
		}
	}
}
