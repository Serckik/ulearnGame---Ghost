using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class Key : InotShadow
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Bitmap image { get; set; }
        public string name { get; set; }
        public bool IsCollect = false;

        public Key(Bitmap image, int x, int y)
        {
            this.image = image;
            name = "Key";
            X = x * GameModel.ElementSize;
            Y = y * GameModel.ElementSize;
        }

        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            if(GameModel.player.Position.X == X / 60 && GameModel.player.Position.Y == Y / 60 && !IsCollect)
            {
                g.DrawImage(Resource1.Floor, new Point(X, Y));
                GameModel.map[X / 60, Y / 60] = new Floor(X / 60, Y / 60);
                GameModel.KeysFound += 1;
                IsCollect = true;
                GameModel.player.IsKeyCollect = true;
            }
            else
            {
                g.DrawImage(Resource1.Floor, new Point(X, Y));
                g.DrawImage(image, new Point(X, Y));
            }
        }
    }
}
