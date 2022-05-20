using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ulearngame1
{
    class Monster : IMoveble
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Bitmap image { get; set; }
        public string name { get; set; }
        public int ShiftX { get; set; }
        public int ShiftY { get; set; }
        public Point Position { get; set; }
        public bool PositionChanged { get; set; }
        public int MoveSpeed { get; set; }
        public int Vision { get; set; }
        public List<IPlaceable> VisionObjects { get; set; }
        public bool IsVisible { get; set; }
        public Point LastSeePlayer = new Point(-1, -1);
        public bool SeePlayer;
        public Directions direction = Directions.None;
        public bool IsStanned;
        private int delta;
        public int currentFrame;
        public int height = 60;
        public int widht;
        public bool left, right, top, bottom, atack;

        public Monster(int x, int y)
        {
            name = "Monster";
            X = (x * GameModel.ElementSize) - 30;
            Y = (y * GameModel.ElementSize) - 30;
            
            Position = new Point(x, y);
            Vision = 6;
            MoveSpeed = 5;
            PositionChanged = true;
            IsVisible = false;
        }

        private Directions action;
        private Directions preDir = Directions.None;
        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            right = !(GameModel.map[Position.X + 1, Position.Y] is Wall) && !(GameModel.map[Position.X + 1, Position.Y] is ClosedDoor);

            left = !(GameModel.map[Position.X - 1, Position.Y] is Wall) && !(GameModel.map[Position.X - 1, Position.Y] is ClosedDoor);

            bottom = !(GameModel.map[Position.X, Position.Y + 1] is Wall) && !(GameModel.map[Position.X, Position.Y + 1] is ClosedDoor);

            top = !(GameModel.map[Position.X, Position.Y - 1] is Wall) && !(GameModel.map[Position.X, Position.Y - 1] is ClosedDoor);

            image = ChooseAnimation(keyPressed) == null ? Resource1.MIdle : ChooseAnimation(keyPressed);
            var newDelta = image.Width / 60;
            if (preDir != action)
            {
                delta = newDelta;
                currentFrame = direction == Directions.right ? 0 : delta - 1;
                widht = image.Width / delta;
            }
            preDir = action;
            g.DrawImage(image, new Rectangle(X, Y, widht, height), currentFrame * widht, 0, widht, height, GraphicsUnit.Pixel);
            if (currentFrame + 1 < delta && action == Directions.right)
                currentFrame++;
            else if (action == Directions.left && currentFrame - 1 >= 0)
                currentFrame--;
            else
            {
                currentFrame = action == Directions.right ? 0 : delta - 1;
                atack = false;
            }
        }

        public Bitmap ChooseAnimation(bool keyPressed)
        {
            //if (atack && direction == Directions.right)
            //{
            //    action = Directions.Atack;
            //    frame = 90;
            //    return Resource1.MIdle;
            //}

            //if (atack && direction == Directions.left)
            //{
            //    action = Directions.Atack;
            //    frame = 90;
            //    return Resource1.MIdle;
            //}

            if (IsStanned && preDir == Directions.right)
            {
                action = Directions.right;
                return Resource1.MIdle;
            }

            if (IsStanned && preDir == Directions.left)
            {
                action = Directions.left;
                return Resource1.MIdleRe;
            }

            if ((direction == Directions.bottom || direction == Directions.top) && preDir == Directions.right)
            {
                action = Directions.right;
                return Resource1.MWalk;
            }

            if ((direction == Directions.bottom || direction == Directions.top) && preDir == Directions.left)
            {
                action = Directions.left;
                return Resource1.MWalkRe;
            }

            if (direction == Directions.right)
            {
                action = Directions.right;
                return Resource1.MWalk;
            }
            else if (direction == Directions.left)
            {
                action = Directions.left;
                return Resource1.MWalkRe;
            }
            return image;
        }

        private int number;
        private readonly Random random = new Random();
        private int StayTime;
        public void MonsterMove()
        {
            if (IsStanned && StayTime != 100) 
            {
                SeePlayer = false;
                direction = Directions.None;
                StayTime++;
                return;
            }

            IsStanned = false;
            StayTime = 0;

            right = !(GameModel.map[Position.X + 1, Position.Y] is Wall) && !(GameModel.map[Position.X + 1, Position.Y] is ClosedDoor);

            left = !(GameModel.map[Position.X - 1, Position.Y] is Wall) && !(GameModel.map[Position.X - 1, Position.Y] is ClosedDoor);

            bottom = !(GameModel.map[Position.X, Position.Y + 1] is Wall) && !(GameModel.map[Position.X, Position.Y + 1] is ClosedDoor);

            top = !(GameModel.map[Position.X, Position.Y - 1] is Wall) && !(GameModel.map[Position.X, Position.Y - 1] is ClosedDoor);

            var freeSpace = 0;
            freeSpace = !right ? freeSpace : freeSpace + 1;
            freeSpace = !left ? freeSpace : freeSpace + 1;
            freeSpace = !bottom ? freeSpace : freeSpace + 1;
            freeSpace = !top ? freeSpace : freeSpace + 1;

            if (VisionObjects.Where(x => x.X / 60 == GameModel.player.Position.X && x.Y / 60 == GameModel.player.Position.Y).Count() != 0)
            {
                LastSeePlayer = new Point(GameModel.player.Position.X, GameModel.player.Position.Y);
                SeePlayer = true;
            }
            if (SeePlayer)
            {
                if (LastSeePlayer.X < Position.X)
                {
                    direction = 0;
                    Move.MoveX(this, 0, 60, -1);
                }
                if (LastSeePlayer.X > Position.X)
                {
                    direction = (Directions)1;
                    Move.MoveX(this, 60, 0, 1);
                }
                if (LastSeePlayer.Y < Position.Y)
                {
                    direction = (Directions)2;
                    Move.MoveY(this, 0, 60, -1);
                }
                if (LastSeePlayer.Y > Position.Y)
                {
                    direction = (Directions)3;
                    Move.MoveY(this, 60, 0, 1);
                }
                if (LastSeePlayer == new Point(Position.X, Position.Y))
                    SeePlayer = false;
            }
            else
            {
                if (freeSpace == 1 || direction == Directions.None || ((left || right) && (top || bottom) && PositionChanged))
                {
                    PositionChanged = false;
                    while (true)
                    {
                        number = random.Next(1, 5);
                        if ((number == 1 && left && direction != Directions.right) || (freeSpace == 1 && direction == Directions.left && right && number == 2))
                            break;
                        if ((number == 2 && right && direction != Directions.left) || (freeSpace == 1 && direction == Directions.right && left && number == 1))
                            break;
                        if ((number == 3 && top && direction != Directions.bottom) || (freeSpace == 1 && direction == Directions.top && bottom && number == 4))
                            break;
                        if ((number == 4 && bottom && direction != Directions.top) || (freeSpace == 1 && direction == Directions.bottom && top && number == 3))
                            break;
                    }
                }

                if (number == 1)
                {
                    Move.MoveX(this, 0, 60, -1);
                    direction = 0;
                }
                else if (number == 2)
                {
                    Move.MoveX(this, 60, 0, 1);
                    direction = (Directions)1;
                }
                else if (number == 3)
                {
                    Move.MoveY(this, 0, 60, -1);
                    direction = (Directions)2;
                }
                else if (number == 4)
                {
                    Move.MoveY(this, 60, 0, 1);
                    direction = (Directions)3;
                }
            }

            if (Position.X == GameModel.player.Position.X && Position.Y == GameModel.player.Position.Y)
                GameModel.GameIsOver();
        }
    }
}
