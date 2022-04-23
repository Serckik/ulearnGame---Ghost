using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class GameModel
    {
        public const int ElementSize = 60;
        public static List<IPlaceable> animations = new List<IPlaceable>();
        public static IPlaceable[,] map = Map.CreateMap();
        public static int MapWidth => map.GetLength(0);
        public static int MapHeight => map.GetLength(1);
        public static bool keyPressed = false;
        public static int MoveSpeed = 5;
        public static Player player;
        public static int vision = 5;
        public static void GetAnimations()
        {
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    var creature = map[x, y];

                    if (creature == null) continue;
                    if (creature is Imoveble)
                    {
                        animations.Add(creature);
                        player = new Player(x, y);
                        animations.Add(new Floor(x, y));
                    }
                    else
                        animations.Add(creature);
                }
            }
        }

        public static void GetVision()
        {
            animations.Clear();
            var shift = 0;
            var start = player.Position.Y - vision;
            while(shift != vision + 1)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth))
                        animations.Add(map[player.Position.X - i, start]);
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) && map[player.Position.X + i, start] == map[player.Position.X, player.Position.Y])
                    {
                        map[player.Position.X, player.Position.Y] = new Floor(player.Position.X, player.Position.Y);
                        animations.Add(new Floor(player.Position.X, player.Position.Y));
                    }
                    if(!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth))
                    {
                        animations.Add(map[player.Position.X + i, start]);
                    }
                }
                start++;
                shift++;
            }
            shift = 0;
            start = player.Position.Y + vision;
            while(shift != vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth))
                        animations.Add(map[player.Position.X - i, start]);
                    if(!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth))
                        animations.Add(map[player.Position.X + i, start]);
                }
                start--;
                shift++;
            }
            animations.Add(player);
        }
    }
}
