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
        public static void MoveX(IMoveble creature, int maximum, int minimum, int delta)
        {
            if ((creature.ShiftY <= 10 || creature.ShiftY >= -10) && creature.ShiftX != maximum)
            {
                creature.X += creature.MoveSpeed * delta;
                creature.ShiftX += creature.MoveSpeed * delta;
            }
            if (creature.ShiftX == maximum && !(GameModel.map[creature.Position.X + delta, creature.Position.Y] is Wall))
            {
                creature.Position = new Point(creature.Position.X + delta, creature.Position.Y);
                creature.PositionChanged = true;
                creature.ShiftX = minimum;
            }
        }

        public static void MoveY(IMoveble creature, int maximum, int minimum, int delta)
        {
            if ((creature.ShiftX <= 10 || creature.ShiftX >= -10) && creature.ShiftY != maximum)
            {
                creature.Y += creature.MoveSpeed * delta;
                creature.ShiftY += creature.MoveSpeed * delta;
            }
            if (creature.ShiftY == maximum && !(GameModel.map[creature.Position.X, creature.Position.Y + delta] is Wall))
            {
                creature.Position = new Point(creature.Position.X, creature.Position.Y + delta);
                creature.PositionChanged = true;
                creature.ShiftY = minimum;
            }
        }
    }
}
