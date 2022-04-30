using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ulearngame1
{
    class Player : IMoveble
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int currentFrame;
        public static Bitmap walkImage = Resource1.Walk;
        public static Bitmap stayImage = Resource1.Idle;
        public static Bitmap stayImageRe = Resource1.IdleRe;
        public static Bitmap walkImageRe = Resource1.WalkRe;
        public int height = walkImage.Height;
        public int widht = walkImage.Width / 8;
        public bool left, right, top, bottom;
        public int state = 0;

        public Bitmap image { get; set; }
        public string name { get; set; }
        public int ShiftX { get; set; }
        public int ShiftY { get; set; }
        public Point Position { get; set; }
        public bool PositionChanged { get; set; }
        public int MoveSpeed { get; set; }
        public int Vision { get; set; }
        public bool IsVisible { get; set; }

        public Player() 
        {
            name = "Player";
        }

        public Player(int x, int y)
        {
            name = "Player";
            X = (x * GameModel.ElementSize) - 30;
            Y = (y * GameModel.ElementSize) - 30;
            Position = new Point(x, y);
            Vision = 5;
            MoveSpeed = 5;
        }

        public void PlayerMove()
        {
            if (GameModel.player.left == true)
                Move.MoveX(GameModel.player, 0, 60, -1);
            else if (GameModel.player.right == true)
                Move.MoveX(GameModel.player, 60, 0, 1);
            else if (GameModel.player.top == true)
                Move.MoveY(GameModel.player, 0, 60, -1);
            else if (GameModel.player.bottom == true)
                Move.MoveY(GameModel.player, 60, 0, 1);
            else GameModel.keyPressed = false;
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
            if (!keyPressed && state == 1) return Resource1.Idle;
            else if (!keyPressed && state == 0) return Resource1.IdleRe;
            if ((bottom || top) && state == 1) return Resource1.Walk;
            if ((bottom || top) && state == 0) return Resource1.WalkRe;
            if (right)
            {
                state = 1;
                return Resource1.Walk;
            }
            else if (left)
            {
                state = 0;
                return Resource1.WalkRe;
            }
            return image;
        }
    }
}
