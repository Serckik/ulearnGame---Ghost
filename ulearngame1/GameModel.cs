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
        public static void DevVision()
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
            //var shadows = new List<Point>();
            var shadows = FindShadows(player.Position.Y - vision, new HashSet<Point>()); ;
            DoSectorVisibility(player.Position.Y - vision, shadows);
            FindSingleObject(shadows);
            animations.Add(player);
        }

        private static bool InBounds(int point, int MaxValue)
        {
            return point < 0 || point >= MaxValue;
        }

        private static bool IsShadow(HashSet<Point> shadows, int position, int topPoint)
        {
            return !shadows.Contains(new Point(position, topPoint)) &&
                        HaveSpace(position, topPoint, shadows);
        }

        private static void DoSectorVisibility(int topPoint, HashSet<Point> shadows)
        {
            var shift = 0;
            while (topPoint != player.Position.Y + vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (InBounds(topPoint, MapHeight)) continue;

                    if (!InBounds(player.Position.X - i, MapWidth) && IsShadow(shadows, player.Position.X - i, topPoint))
                        animations.Add(map[player.Position.X - i, topPoint]);

                    if (!InBounds(player.Position.X + i, MapWidth) && map[player.Position.X + i, topPoint] is Player)
                        map[player.Position.X, player.Position.Y] = new Floor(player.Position.X, player.Position.Y);

                    if (!InBounds(player.Position.X + i, MapWidth) && IsShadow(shadows, player.Position.X + i, topPoint))
                        animations.Add(map[player.Position.X + i, topPoint]);
                }
                topPoint++;
                shift = topPoint <= player.Position.Y ? shift + 1 : shift - 1;
            }
        }

        private static void FindSingleObject(HashSet<Point> shadows)
        {
            var ischange = true;
            while (ischange)
            {
                ischange = false;
                for (var i = 0; i < animations.Count; i++)
                {
                    if (!shadows.Contains(new Point(animations[i].X / 60, animations[i].Y / 60)) && !HaveSpace(animations[i].X / 60, animations[i].Y / 60, shadows))
                    {
                        ischange = true;
                        animations.Remove(animations[i]);
                    }
                }
            }
        }

        private static HashSet<Point> FindShadows(int topPoint, HashSet<Point> shadows)
        {
            var shift = 0;
            while (topPoint != player.Position.Y + vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (InBounds(topPoint, MapHeight)) continue;

                    if (!InBounds(player.Position.X - i, MapWidth) && map[player.Position.X - i, topPoint] is Wall)
                        SetShadows(player.Position.X - i, topPoint, shadows);

                    if (!InBounds(player.Position.X + i, MapWidth) && map[player.Position.X + i, topPoint] is Wall)
                        SetShadows(player.Position.X + i, topPoint, shadows);
                }
                topPoint++;
                shift = topPoint <= player.Position.Y ? shift + 1 : shift - 1;
            }
            return shadows;
        }

        public static void SetShadows(int x, int y, HashSet<Point> shadows)
        {
            var deltaX = x - player.Position.X != 0 ? (x - player.Position.X) / Math.Abs(x - player.Position.X) : 0;
            var deltaY = y - player.Position.Y != 0 ? (y - player.Position.Y) / Math.Abs(y - player.Position.Y) : 0;
            var shift = new Point(deltaX, deltaY);
            while(Math.Abs(x - player.Position.X) + Math.Abs(y - player.Position.Y) <= vision)
            {
                x += shift.X;
                y += shift.Y;
                if ((x < 0 || x >= MapWidth) || (y < 0 || y >= MapHeight))
                    break;
                shadows.Add(new Point(x, y));
                if(!InBounds(x + 1, MapWidth) && !(map[x + 1, y] is Wall))
                    shadows.Add(new Point(x + 1, y));
                if(!InBounds(x - 1, MapWidth) && !(map[x - 1, y] is Wall))
                    shadows.Add(new Point(x - 1, y));
                if(!InBounds(y + 1, MapHeight) && !(map[x, y + 1] is Wall))
                    shadows.Add(new Point(x, y + 1));
                if (!InBounds(y - 1, MapHeight) && !(map[x, y - 1] is Wall))
                    shadows.Add(new Point(x, y - 1));
            }
        }

        private static bool HaveSpace(int x, int y, HashSet<Point> shadows)
        {
            if (x + 1 < MapWidth && Math.Abs(x + 1 - player.Position.X) + Math.Abs(y - player.Position.Y) <= vision && map[x + 1, y] is Floor && !shadows.Contains(new Point(x + 1, y)))
                return true;
            if (x - 1 > 0 && Math.Abs(x - 1 - player.Position.X) + Math.Abs(y - player.Position.Y) <= vision && map[x - 1, y] is Floor && !shadows.Contains(new Point(x - 1, y)))
                return true;
            if (y + 1 < MapHeight && Math.Abs(x - player.Position.X) + Math.Abs(y + 1 - player.Position.Y) <= vision && map[x, y + 1] is Floor && !shadows.Contains(new Point(x, y + 1)))
                return true;
            if (y - 1 > 0 && Math.Abs(x - player.Position.X) + Math.Abs(y - 1 - player.Position.Y) <= vision &&  map[x, y - 1] is Floor && !shadows.Contains(new Point(x, y - 1)))
                return true;
            shadows.Add(new Point(x, y));
            return false;
        }
    }
}
