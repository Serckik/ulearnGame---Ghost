using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    class View
    {
        public static void UpdateTextures(Graphics g, bool keyPressed)
        {
            foreach (var item in GameModel.VisionObjects)
                if (!(item is IMoveble))
                    item.PlayAnimation(g, keyPressed);
            foreach (var item in GameModel.VisionObjects)
                if (item is IMoveble)
                {
                    if(item is Player)
                    {
                        GameModel.player = (Player)item;
                        GameModel.player.PlayAnimation(g, keyPressed);
                        GameModel.player.PlayerMove();
                    }
                    else
                    {
                        var monster = (Monster)item;
                        if(monster.IsVisible)
                            monster.PlayAnimation(g, keyPressed);
                        monster.MonsterMove();
                    }
                }
        }
    }
}
