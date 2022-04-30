﻿using System;
using System.Drawing;

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
        public bool IsVisible { get; set; }

        public bool left, right, top, bottom;
        public Monster(int x, int y)
        {
            name = "Monster";
            X = (x * GameModel.ElementSize) - 30;
            Y = (y * GameModel.ElementSize) - 30;
            Position = new Point(x, y);
            Vision = 4;
            MoveSpeed = 6;
            PositionChanged = true;
            IsVisible = false;
        }

        public void PlayAnimation(Graphics g, bool keyPressed)
        {
            g.DrawImage(Resource1.monster, new Point(X, Y));
        }

        private int number;
        private readonly Random random = new Random();
        public void MonsterMove()
        {
            right = !(GameModel.map[Position.X + 1, Position.Y] is Wall);

            left = !(GameModel.map[Position.X - 1, Position.Y] is Wall);

            bottom = !(GameModel.map[Position.X, Position.Y + 1] is Wall);

            top = !(GameModel.map[Position.X, Position.Y - 1] is Wall);

            if (GameModel.player != null && Math.Abs(GameModel.player.Position.X - Position.X) <= 5 && Position.Y == GameModel.player.Position.Y)
            {
                if (GameModel.player.Position.X >= Position.X)
                    Move.MoveX(this, 60, 0, 1);
                else
                    Move.MoveX(this, 0, 60, -1);
            }
            else if (GameModel.player != null && Math.Abs(GameModel.player.Position.Y - Position.Y) <= 5 && Position.X == GameModel.player.Position.X)
            {
                if (GameModel.player.Position.Y >= Position.Y)
                    Move.MoveY(this, 60, 0, 1);
                else
                    Move.MoveY(this, 0, 60, -1);
            }
            else
            {
                if (PositionChanged)
                {
                    PositionChanged = false;
                    number = random.Next(1, 5);
                }

                if (number == 1)
                {
                    if (left)
                        Move.MoveX(this, 0, 60, -1);
                    else
                        PositionChanged = true;
                }
                else if (number == 2)
                {
                    if (right)
                        Move.MoveX(this, 60, 0, 1);
                    else
                        PositionChanged = true;
                }
                else if (number == 3)
                {
                    if (top)
                        Move.MoveY(this, 0, 60, -1);
                    else
                        PositionChanged = true;
                }
                else if (number == 4)
                {
                    if (bottom)
                        Move.MoveY(this, 60, 0, 1);
                    else
                        PositionChanged = true;
                }
            }

        }
    }
}
