﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    class view
    {
        public static void UpdateTextures(Graphics g, bool keyPressed)
        {
            foreach (var item in GameModel.animations)
                if (!(item is Imoveble))
                    item.PlayAnimation(g, keyPressed);
            foreach (var item in GameModel.animations)
                if (item is Imoveble)
                {
                    if(item is Player)
                    {
                        GameModel.player = (Player)item;
                        GameModel.player.PlayAnimation(g, keyPressed);
                    }
                    else
                    {
                        GameModel.monster = (Monster)item;
                        GameModel.monster.PlayAnimation(g, keyPressed);
                    }
                }
        }
    }
}