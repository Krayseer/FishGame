using System;
using System.Windows.Forms;

namespace FishGame
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            while (!Menu.сloseForm)
            {
                Application.Run(new Menu());
                if (Menu.play)
                    LevelManager.Load(Menu.lvl);
            }
            Application.Exit();
        }
    }
}
