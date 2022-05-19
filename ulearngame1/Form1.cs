using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{

    public partial class Form1 : Form
    {
        public static Timer timer;
        public static Timer secondTimer;
        public static bool isPused;
        public static Control.ControlCollection control;
        public Form1()
        {
            timer = new Timer();
            secondTimer = new Timer();
            secondTimer.Interval = 50;
            secondTimer.Tick += SecondUpdate;
            secondTimer.Start();
            control = Controls;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            timer.Interval = 50;
            timer.Tick += Update;
            timer.Start();
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(!isPused)
                Controller.PlayerKey(e.KeyCode, false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isPused)
            {
                GameModel.keyPressed = true;
                Controller.PlayerKey(e.KeyCode, true);
            }
        }

        public void Update(object sender, EventArgs e)
        {
            GameModel.GetVision();
        }

        public void SecondUpdate(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            View.UpdateTextures(e.Graphics, GameModel.keyPressed);
        }
    }
}
