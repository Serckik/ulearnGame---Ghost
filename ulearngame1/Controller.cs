using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    class Controller
    {
        private static Random random = new Random();

        public static void PlayerKey(Keys pressed, bool IsActive)
        {
            if (pressed == Keys.A) { GameModel.player.left = IsActive; };
            if (pressed == Keys.D) { GameModel.player.right = IsActive; };
            if (pressed == Keys.W) { GameModel.player.top = IsActive; };
            if (pressed == Keys.S) { GameModel.player.bottom = IsActive; };
        }    

        private static void MoveX(Imoveble creature, int maximum, int minimum, int delta)
        {
            if((creature.shiftY <= 10 || creature.shiftY >= -10) && creature.shiftX != maximum)
            {
                creature.X += GameModel.MoveSpeed * delta;
                creature.shiftX += GameModel.MoveSpeed * delta;
            }
            if(creature.shiftX == maximum && !(GameModel.map[creature.Position.X + delta, creature.Position.Y] is Wall))
            {
                creature.Position = new Point(creature.Position.X + delta, creature.Position.Y);
                creature.shiftX = minimum;
            }
        }

        private static void MoveY(Imoveble creature, int maximum, int minimum, int delta)
        {
            if ((creature.shiftX <= 10 || creature.shiftX >= -10) && creature.shiftY != maximum)
            {
                creature.Y += GameModel.MoveSpeed * delta;
                creature.shiftY += GameModel.MoveSpeed * delta;
            }
            if (creature.shiftY == maximum && !(GameModel.map[creature.Position.X, creature.Position.Y + delta] is Wall))
            {
                creature.Position = new Point(creature.Position.X, creature.Position.Y + delta);
                creature.shiftY = minimum;
            }
        }

        public static void PlayerMove()
        {
            if (GameModel.player.left == true)
                MoveX(GameModel.player, 0, 60, -1);
            else if (GameModel.player.right == true)
                MoveX(GameModel.player, 60, 0, 1);
            else if (GameModel.player.top == true)
                MoveY(GameModel.player, 0, 60, -1);
            else if (GameModel.player.bottom == true)
                MoveY(GameModel.player, 60, 0, 1);
            else GameModel.keyPressed = false;
        }

        private static int number;
        public static void MonsterMove()
        {
            if((GameModel.monster.shiftX == 0 && GameModel.monster.shiftY == 0) 
                || (GameModel.monster.shiftX == 60 && GameModel.monster.shiftY == 0) 
                || (GameModel.monster.shiftX == 0 && GameModel.monster.shiftY == 60) 
                || (GameModel.monster.shiftX == 60 && GameModel.monster.shiftY == 60))
                number = random.Next(1, 5);
            if (number == 1)
                MoveX(GameModel.monster, 0, 60, -1);
            else if (number == 2)
                MoveX(GameModel.monster, 60, 0, 1);
            else if (number == 3)
                MoveY(GameModel.monster, 0, 60, -1);
            else if (number == 4)
                MoveY(GameModel.monster, 60, 0, 1);
        }
    }
}
