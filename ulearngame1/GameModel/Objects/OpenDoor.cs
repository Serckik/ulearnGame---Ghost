using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class OpenDoor : IPlaceable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Bitmap image { get; set; }
        public string name { get; set; }

        public OpenDoor(Bitmap image, int x, int y)
        {
            this.image = image;
            name = "Key";
            X = x * GameModel.ElementSize;
            Y = y * GameModel.ElementSize;
        }

        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            g.DrawImage(image, new Point(X, Y));
        }
    }
}
