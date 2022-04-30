using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    interface IMoveble : IPlaceable
    {
        int ShiftX { get; set; }
        int ShiftY { get; set; }
        Point Position { get; set; }
        bool PositionChanged { get; set; }
        int MoveSpeed { get; set; }
        int Vision { get; set; }
        bool IsVisible { get; set; }
    }
}
