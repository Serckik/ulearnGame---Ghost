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
    }
}
