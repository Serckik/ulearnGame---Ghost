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
                    else if(item is Monster)
                    {
                        var monster = (Monster)item;
                        monster.MonsterMove();
                        if (monster.IsVisible)
                            monster.PlayAnimation(g, keyPressed);
                    }
                }
            g.DrawString("Осталось ключей: " + (Map.keysCount - GameModel.KeysFound).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 0, 0);
            g.DrawString("Оглушение: " + (GameModel.player.Power).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 300, 0);
            g.DrawString("Сканирование: " + (GameModel.player.MonstersAreVisible).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 600, 0);
            g.DrawString("| " + Map.StringNameLevels[GameModel.level] + " |", new Font("Impact", 16), new SolidBrush(Color.Gold), 900, 0);
        }
    }
}
