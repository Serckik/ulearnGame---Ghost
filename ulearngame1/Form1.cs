using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    public partial class Form1 : Form
    {
        Timer timer;
        public Form1()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Update;
            timer.Start();
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            //GameModel.DevVision();
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Controller.PlayerKey(e.KeyCode, false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameModel.keyPressed = true;
            Controller.PlayerKey(e.KeyCode, true);
        }

        public void Update(object sender, EventArgs e)
        {
            GameModel.GetVision();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            View.UpdateTextures(e.Graphics, GameModel.keyPressed);
        }
    }
}
