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
        public static void PlayerKey(Keys pressed, bool IsActive)
        {
            if (pressed == Keys.A || pressed == Keys.Left) { GameModel.player.left = IsActive; };
            if (pressed == Keys.D || pressed == Keys.Right) { GameModel.player.right = IsActive; };
            if (pressed == Keys.W || pressed == Keys.Up) { GameModel.player.top = IsActive; };
            if (pressed == Keys.S || pressed == Keys.Down) { GameModel.player.bottom = IsActive; };
            if (pressed == Keys.Q) { GameModel.player.CanStanMonster(); };
        }
    }
}
