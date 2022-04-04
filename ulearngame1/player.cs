using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ulearngame1
{
    class Player : IPlaceable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int a;
        public int b;
        public int c;
        public int d;
        public int e;
        public int f;
        public int g;
        public int h;
        public int j;
        public int currentFrame;
        public static Bitmap walkImage = Resource1.Walk;
        public static Bitmap stayImage = Resource1.Idle;
        public static Bitmap stayImageRe = Resource1.IdleRe;
        public static Bitmap walkImageRe = Resource1.WalkRe;
        public int height = walkImage.Height;
        public int widht = walkImage.Width / 8;
        public bool left, right, top, bottom;
        public int state = 0;
        public Bitmap image;
        public Point position;

        Bitmap IPlaceable.image { get; set; }
        public string name { get; set; }

        public Player()
        {
            name = "Player";
            position = new Point(X / 60, Y / 60);
        }

        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            image = ChooseAnimation(keyPressed);
            g.DrawImage(image, new Rectangle(X, Y, widht, height), currentFrame * widht, 0, widht, height, GraphicsUnit.Pixel);
            if (currentFrame + 1 < 5)
                currentFrame++;
            else
                currentFrame = 0;
        }

        public Bitmap ChooseAnimation(bool keyPressed)
        {
            if (!keyPressed && state == 1) return stayImage;
            else if (!keyPressed && state == 0) return stayImageRe;
            if ((bottom || top) && state == 1) return walkImage;
            if ((bottom || top) && state == 0) return walkImageRe;
            if (right)
            {
                state = 1;
                return walkImage;
            }
            else if (left)
            {
                state = 0;
                return walkImageRe;
            }
            return image;
        }
    }
}
