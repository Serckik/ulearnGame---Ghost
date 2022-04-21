using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class Move
    {
        public static void MoveX(Imoveble creature, int maximum, int minimum, int delta)
        {
            if ((creature.shiftY <= 10 || creature.shiftY >= -10) && creature.shiftX != maximum)
            {
                creature.X += creature.moveSpeed * delta;
                creature.shiftX += creature.moveSpeed * delta;
            }
            if (creature.shiftX == maximum && !(GameModel.map[creature.Position.X + delta, creature.Position.Y] is Wall))
            {
                creature.Position = new Point(creature.Position.X + delta, creature.Position.Y);
                creature.PositionChanged = true;
                creature.shiftX = minimum;
            }
        }

        public static void MoveY(Imoveble creature, int maximum, int minimum, int delta)
        {
            if ((creature.shiftX <= 10 || creature.shiftX >= -10) && creature.shiftY != maximum)
            {
                creature.Y += creature.moveSpeed * delta;
                creature.shiftY += creature.moveSpeed * delta;
            }
            if (creature.shiftY == maximum && !(GameModel.map[creature.Position.X, creature.Position.Y + delta] is Wall))
            {
                creature.Position = new Point(creature.Position.X, creature.Position.Y + delta);
                creature.PositionChanged = true;
                creature.shiftY = minimum;
            }
        }
    }
}
