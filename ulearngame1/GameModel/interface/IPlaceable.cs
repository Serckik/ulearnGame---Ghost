using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ulearngame1
{
    public interface IPlaceable
    {
        int X { get; set; }
        int Y { get; set; }
        Bitmap image { get; set; }
        string name { get; set; }

        void  PlayAnimation(Graphics g, bool keyPressed);
    }
}
