using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class Monster : Imoveble
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Bitmap image { get; set; }
        public string name { get; set; }
        public int shiftX { get; set; }
        public int shiftY { get; set; }
        public Point Position { get; set; }

        public Monster(int x, int y)
        {
            name = "Monster";
            X = (x * GameModel.ElementSize) - 30;
            Y = (y * GameModel.ElementSize) - 30;
            Position = new Point(x, y);
        }

        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            g.DrawImage(Resource1.monster, new Point(X, Y));
        }
    }
}
