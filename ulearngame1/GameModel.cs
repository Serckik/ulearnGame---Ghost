using System;
using System.Collections.Generic;
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
        public static Player player;
        public static Monster monster;
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
                        animations.Add(new Floor(x, y));
                    }
                    else
                        animations.Add(creature);
                }
            }
        }
    }
}
