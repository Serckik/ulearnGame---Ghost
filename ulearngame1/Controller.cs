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
        public static int MoveSpeed = 5;

        public static void PlayerKey(Keys pressed, bool IsActive)
        {
            if (pressed == Keys.A) { GameModel.player.left = IsActive; };
            if (pressed == Keys.D) { GameModel.player.right = IsActive; };
            if (pressed == Keys.W) { GameModel.player.top = IsActive; };
            if (pressed == Keys.S) { GameModel.player.bottom = IsActive; };
        }    

        public static void PlayerMove()
        {
            if (GameModel.player.left == true)
            {
                if ((shiftY <= 10 || shiftY >= -10) && shiftX != 0)
                {
                    GameModel.player.X -= MoveSpeed;
                    shiftX -= MoveSpeed;
                }
                if (shiftX == 0 && !(GameModel.map[GameModel.player.Position.X - 1, GameModel.player.Position.Y] is Wall))
                {
                    GameModel.player.Position.X -= 1;
                    shiftX = 60;
                }
            }
            else if (GameModel.player.right == true)
            {
                if ((shiftY <= 10 || shiftY >= -10) && shiftX != 60)
                {
                    GameModel.player.X += MoveSpeed;
                    shiftX += MoveSpeed;
                }
                if (shiftX == 60 && !(GameModel.map[GameModel.player.Position.X + 1, GameModel.player.Position.Y] is Wall))
                {
                    GameModel.player.Position.X += 1;
                    shiftX = 0;
                }
            }
            else if (GameModel.player.top == true)
            {
                if ((shiftX <= 10 || shiftX >= -10) && shiftY != 0)
                {
                    GameModel.player.Y -= MoveSpeed;
                    shiftY -= MoveSpeed;
                }
                if (shiftY == 0 && !(GameModel.map[GameModel.player.Position.X, GameModel.player.Position.Y - 1] is Wall))
                {
                    GameModel.player.Position.Y -= 1;
                    shiftY = 60;
                }
            }
            else if (GameModel.player.bottom == true)
            {
                if ((shiftX <= 10 || shiftX >= -10) && shiftY != 60)
                {
                    GameModel.player.Y += MoveSpeed;
                    shiftY += MoveSpeed;
                }
                if (shiftY == 60 && !(GameModel.map[GameModel.player.Position.X, GameModel.player.Position.Y + 1] is Wall))
                {
                    GameModel.player.Position.Y += 1;
                    shiftY = 0;
                }
            }
            else GameModel.keyPressed = false;
        }

        public static void MonsterMove()
        {

        }
    }
}
