using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    class Controller
    {
        public static int shiftX;
        public static int shiftY;

        public static void PlayerKey(Keys pressed, bool IsActive)
        {
            if (pressed == Keys.A) { GameModel.player.left = IsActive; };
            if (pressed == Keys.D) { GameModel.player.right = IsActive; };
            if (pressed == Keys.W) { GameModel.player.top = IsActive; };
            if (pressed == Keys.S) { GameModel.player.bottom = IsActive; };
        }    

        public static void PlayerMove()
        {
            if (GameModel.player.left == true && !(GameModel.map[GameModel.player.Position.X - 1, GameModel.player.Position.Y] is Wall))
            {
                if (shiftY == 0)
                {
                    GameModel.player.X -= 5;
                    shiftX -= 5;
                }
                if (shiftX == -60)
                {
                    GameModel.player.Position.X -= 1;
                    shiftX = 0;
                }
            }
            else if (GameModel.player.right == true && !(GameModel.map[GameModel.player.Position.X + 1, GameModel.player.Position.Y] is Wall))
            {
                if (shiftY == 0)
                {
                    GameModel.player.X += 5;
                    shiftX += 5;
                }
                if (shiftX == 60)
                {
                    GameModel.player.Position.X += 1;
                    shiftX = 0;
                }
            }
            else if (GameModel.player.top == true && !(GameModel.map[GameModel.player.Position.X, GameModel.player.Position.Y - 1] is Wall))
            {
                if (shiftX == 0)
                {
                    GameModel.player.Y -= 5;
                    shiftY -= 5;
                }
                if (shiftY == -60)
                {
                    GameModel.player.Position.Y -= 1;
                    shiftY = 0;
                }
            }
            else if (GameModel.player.bottom == true && !(GameModel.map[GameModel.player.Position.X, GameModel.player.Position.Y + 1] is Wall))
            {
                if (shiftX == 0)
                {
                    GameModel.player.Y += 5;
                    shiftY += 5;
                }
                if (shiftY == 60)
                {
                    GameModel.player.Position.Y += 1;
                    shiftY = 0;
                }
            }
            else GameModel.keyPressed = false;
        }
    }
}
