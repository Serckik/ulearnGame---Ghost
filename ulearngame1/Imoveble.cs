using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    interface Imoveble : IPlaceable
    {
        int shiftX { get; set; }
        int shiftY { get; set; }
        Point Position { get; set; }
        bool PositionChanged { get; set; }
        int moveSpeed { get; set; }
        int vision { get; set; }
    }
}
