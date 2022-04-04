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
        public const int ElementSize = 60;
        public List<IPlaceable> animations = new List<IPlaceable>();
        Timer timer;
        public bool keyPressed;
        public Keys key;
        public int shiftX;
        public int shiftY;
        public Form1()
        {
            timer = new Timer();
            Unit();
            timer.Interval = 50;
            timer.Tick += Update;
            timer.Start();
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Map.CreateMap();
            for (var x = 0; x < Map.MapWidth; x++)
                for (var y = 0; y < Map.MapHeight; y++)
                {
                    var creature = Map.map[x, y];
                    if (creature == null) continue;
                    if(!(creature is Player))
                    {
                        creature.X = x * ElementSize;
                        creature.Y = y * ElementSize;
                        animations.Add(creature);
                    }
                    else
                    {
                        var player = new Player();
                        player.X = x * ElementSize;
                        player.Y = y * ElementSize;
                        player.position.X = x;
                        player.position.Y = y;
                        var floor = new Floor();
                        floor.X = x * ElementSize;
                        floor.Y = y * ElementSize;
                        animations.Add(player);
                        animations.Add(floor);
                    }
                }
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { player.left = false; };
            if (e.KeyCode == Keys.D) { player.right = false; };
            if (e.KeyCode == Keys.W) { player.top = false; };
            if (e.KeyCode == Keys.S) { player.bottom = false; };
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyPressed = true;
            if (e.KeyCode == Keys.A) { player.left = true; };
            if (e.KeyCode == Keys.D) { player.right = true; };
            if (e.KeyCode == Keys.W) { player.top = true; };
            if (e.KeyCode == Keys.S) { player.bottom = true; };
        }

        public void Update(object sender, EventArgs e)
        {
            if (player.left == true && !(Map.map[player.position.X - 1, player.position.Y] is Wall)) 
            {
                if(shiftY == 0)
                {
                    player.X -= 5;
                    shiftX -= 5;
                }
                if (shiftX == -60)
                {
                    player.position.X -= 1;
                    shiftX = 0;
                }
            }
            else if (player.right == true && !(Map.map[player.position.X + 1, player.position.Y] is Wall)) 
            {
                if(shiftY == 0)
                {
                    player.X += 5;
                    shiftX += 5;
                }
                if (shiftX == 60)
                {
                    player.position.X += 1;
                    shiftX = 0;
                }
            }
            else if (player.top == true && !(Map.map[player.position.X, player.position.Y - 1] is Wall)) 
            {
                if(shiftX == 0)
                {
                    player.Y -= 5;
                    shiftY -= 5;
                }
                if (shiftY == -60)
                {
                    player.position.Y -= 1;
                    shiftY = 0;
                }
            }
            else if (player.bottom == true && !(Map.map[player.position.X, player.position.Y + 1] is Wall)) 
            {
                if (shiftX == 0)
                {
                    player.Y += 5;
                    shiftY += 5;
                }
                if (shiftY == 60)
                {
                    player.position.Y += 1;
                    shiftY = 0;
                }
            }
            else keyPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var item in animations)
                if(!(item is Player))
                item.PlayAnimation(e.Graphics, keyPressed);
            foreach (var item in animations)
                if (item is Player)
                {
                    player = (Player)item;
                    player.PlayAnimation(e.Graphics, keyPressed);
                }
        }

        Player player;
        public void Unit()
        {
            player = new Player();
        }
    }
}
