using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ulearngame1
{
    enum Directions
    {
        left,
        right,
        top,
        bottom,
        None,
        Atack
    }

    class Player : IMoveble
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int currentFrame;
        public int height = 60;
        public int widht;
        public bool left, right, top, bottom, atack;
        public Directions direction = Directions.right;
        public int Power = 2;
        public int MonstersAreVisible = 100;
        public int VisionTime;
        public bool IsKeyCollect;
        public bool DoorOpen = true;

        public Bitmap image { get; set; }
        public string name { get; set; }
        public int ShiftX { get; set; }
        public int ShiftY { get; set; }
        public Point Position { get; set; }
        public bool PositionChanged { get; set; }
        public int MoveSpeed { get; set; }
        public int Vision { get; set; }
        public bool IsVisible { get; set; }
        public List<IPlaceable> VisionObjects { get; set; }
        private int delta;
        public bool VisionActivate;

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
            Vision = 4;
            MoveSpeed = 6;
        }

        public void CanStanMonster()
        {
            if (Power == 0) return;

            var monsters = GameModel.VisionObjects.Where(x => x is Monster).ToList();
            foreach(var item in monsters)
            {
                var monster = (Monster)item;
                if (Math.Abs(GameModel.player.Position.X - monster.Position.X) <= 1
                    && Math.Abs(GameModel.player.Position.Y - monster.Position.Y) <= 1 && !monster.IsStanned)
                {
                    monster.IsStanned = true;
                    if (!atack)
                        Power--;
                    atack = true;
                }
            }
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

            if (GameModel.map[Position.X, Position.Y] is OpenDoor)
                GameModel.GameIsWin();
        }

        private Directions action;
        private Directions preDir = Directions.None;
        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            var a = ChooseAnimation(keyPressed);
            image = a == null ? Resource1.Idle : a;
            var newDelta = image.Width / image.Height;
            if (preDir != action || delta == 0)
            {
                delta = newDelta;
                currentFrame = direction == Directions.right ? 0 : delta - 1;
                widht = image.Width / delta;
            }
            preDir = action;
            g.DrawImage(image, new Rectangle(X, Y, widht, height), currentFrame * widht, 0, widht, height, GraphicsUnit.Pixel);
            if (currentFrame + 1 < delta && direction == Directions.right)
                currentFrame++;
            else if (direction == Directions.left && currentFrame - 1 > 0)
                currentFrame--;
            else
            {
                currentFrame = direction == Directions.right ? 0 : delta - 1;
                atack = false;
            }
        }

        public Bitmap ChooseAnimation(bool keyPressed)
        {
            if (atack && direction == Directions.right) 
            {
                action = Directions.Atack;
                return Resource1.Atack;
            }

            if (atack && direction == Directions.left) 
            {
                action = Directions.Atack;
                return Resource1.AtackRe;
            }

            if ((bottom || top) && direction == Directions.right)
            {
                action = Directions.right;
                return Resource1.Walk;
            }

            if ((bottom || top) && direction == Directions.left)
            {
                action = Directions.left;
                return Resource1.WalkRe;
            }

            if (right)
            {
                direction = Directions.right;
                action = Directions.right;
                return Resource1.Walk;
            }
            else if (left)
            {
                direction = Directions.left;
                action = Directions.left;
                return Resource1.WalkRe;
            }

            if (!keyPressed && direction == Directions.right) 
            {
                action = Directions.None;
                return Resource1.Idle;
            }

            if (!keyPressed && direction == Directions.left) 
            {
                action = Directions.None;
                return Resource1.IdleRe;
            }
            return image;
        }
    }
}
