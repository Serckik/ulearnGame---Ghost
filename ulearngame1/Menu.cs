using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ulearngame1
{
    public class Menu
    {
        public static Image image = Resource1.ClosedDoor;
        public static Button buttonExit;
        public static Button buttonContinue;
        public static Button buttonSave;
        public static Button buttonAgain;
        public static Button buttonNextLevel;

        public static void StopGame()
        {
            if (Form1.timer.Enabled)
            {
                Form1.timer.Stop();
                Form1.isPused = true;
            }
            else
            {
                Form1.isPused = false;
                Form1.timer.Start();
                Form1.control.Clear();
            }
        }

        public static void MainMenu()
        {
            
            if (!Form1.control.Contains(buttonAgain))
            {
                buttonExit = new Button() { Location = new Point(768, 600), Size = new Size(384, 95), Name = "EXIT", BackColor = Color.White, Text = "Выйти", Font = new Font("Impact", 16) };
                buttonExit.Click += (s, e) => Application.Exit();
                buttonContinue = new Button() { Location = new Point(768, 400), Size = new Size(384, 95), Name = "CONTINUE", BackColor = Color.White, Text = "Продолжить", Font = new Font("Impact", 16) };
                buttonContinue.Click += (s, e) => StopGame();
                Form1.control.Add(buttonContinue);
                Form1.control.Add(buttonExit);
            }
        }
    }
}
