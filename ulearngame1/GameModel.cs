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
        public static IPlaceable[,] map = Map.CreateMap(level);
        public static List<IPlaceable> Imoveble = FindImoveble();
        public static List<IPlaceable> VisionObjects = new List<IPlaceable>();
        public static int MapWidth => map.GetLength(0);
        public static int MapHeight => map.GetLength(1);
        public static int KeysFound = 0;
        public static bool keyPressed = false;
        public static Player player;
        public static int vision;
        public static int level = 0;

        public static void GameIsOver()
        {
            map = Map.CreateMap(level);
            Imoveble = FindImoveble();
            KeysFound = 0;
        }

        public static void GameIsWin()
        {
            map = Map.CreateMap(level += 1);
            Imoveble = FindImoveble();
            KeysFound = 0;
        }

        private static List<IPlaceable> FindImoveble()
        {
            var animations = new LinkedList<IPlaceable>();
            foreach (var item in map)
            {
                if (item is Player)
                {
                    map[player.Position.X, player.Position.Y] = new Floor(player.Position.X, player.Position.Y);
                    animations.AddFirst(item);
                }
                else if (item is IMoveble)
                {
                    var creature = (IMoveble)item;
                    map[creature.Position.X, creature.Position.Y] = new Floor(creature.Position.X, creature.Position.Y);
                    animations.AddLast(item);
                }
            }
            return animations.ToList();
        }

        public static void GetVision()
        {
            VisionObjects.Clear();
            foreach (var item in Imoveble)
            {
                var animations = new List<IPlaceable>();
                var creature = (IMoveble)item;
                vision = creature.Vision;
                var shadows = FindShadows(creature.Position.Y - vision, new HashSet<Point>(), creature);
                //if (item is Player) shadows = new HashSet<Point>();
                DoSectorVisibility(creature.Position.Y - vision, shadows, animations, creature);
                FindSingleObject(shadows, animations, creature);

                if (item is IPlaceable)
                    VisionObjects = VisionObjects.Concat(animations).ToList();

                creature.VisionObjects = animations;

                creature.IsVisible = item is Monster &&
                    VisionObjects.Where(x => x.X / 60 == creature.Position.X && x.Y / 60 == creature.Position.Y).Count() != 0;

            }
            VisionObjects = VisionObjects.Concat(Imoveble).ToList();
        }

        private static bool InBounds(int point, int MaxValue)
        {
            return point < 0 || point >= MaxValue;
        }

        private static bool IsShadow(HashSet<Point> shadows, int position, int topPoint, IMoveble creature)
        {
            return !shadows.Contains(new Point(position, topPoint)) &&
                        HaveSpace(position, topPoint, shadows, creature);
        }

        private static void DoSectorVisibility(int topPoint, HashSet<Point> shadows, List<IPlaceable> animations, IMoveble creature)
        {
            var shift = 0;
            while (topPoint != creature.Position.Y + vision + 1)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (InBounds(topPoint, MapHeight)) continue;

                    if (!InBounds(creature.Position.X - i, MapWidth) && IsShadow(shadows, creature.Position.X - i, topPoint, creature))
                        animations.Add(map[creature.Position.X - i, topPoint]);

                    if (!InBounds(creature.Position.X + i, MapWidth) && IsShadow(shadows, creature.Position.X + i, topPoint, creature))
                        animations.Add(map[creature.Position.X + i, topPoint]);
                }
                topPoint++;
                shift = topPoint <= creature.Position.Y ? shift + 1 : shift - 1;
            }
        }

        private static void FindSingleObject(HashSet<Point> shadows, List<IPlaceable> animations, IMoveble creature)
        {
            var ischange = true;
            while (ischange)
            {
                ischange = false;
                for (var i = 0; i < animations.Count; i++)
                {
                    if (!shadows.Contains(new Point(animations[i].X / 60, animations[i].Y / 60)) && !HaveSpace(animations[i].X / 60, animations[i].Y / 60, shadows, creature))
                    {
                        ischange = true;
                        animations.Remove(animations[i]);
                    }
                }
            }
        }

        private static HashSet<Point> FindShadows(int topPoint, HashSet<Point> shadows, IMoveble creature)
        {
            var shift = 0;
            while (topPoint != creature.Position.Y + vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (InBounds(topPoint, MapHeight)) continue;

                    if (!InBounds(creature.Position.X - i, MapWidth) && map[creature.Position.X - i, topPoint] is Wall)
                        SetShadows(creature.Position.X - i, topPoint, shadows, creature);

                    if (!InBounds(creature.Position.X + i, MapWidth) && map[creature.Position.X + i, topPoint] is Wall)
                        SetShadows(creature.Position.X + i, topPoint, shadows, creature);
                }
                topPoint++;
                shift = topPoint <= creature.Position.Y ? shift + 1 : shift - 1;
            }
            return shadows;
        }

        public static void SetShadows(int x, int y, HashSet<Point> shadows, IMoveble creature)
        {
            var deltaX = x - creature.Position.X != 0 ? (x - creature.Position.X) / Math.Abs(x - creature.Position.X) : 0;
            var deltaY = y - creature.Position.Y != 0 ? (y - creature.Position.Y) / Math.Abs(y - creature.Position.Y) : 0;
            var shift = new Point(deltaX, deltaY);
            while (Math.Abs(x - creature.Position.X) + Math.Abs(y - creature.Position.Y) <= vision)
            {
                x += shift.X;
                y += shift.Y;
                if ((x < 0 || x >= MapWidth) || (y < 0 || y >= MapHeight))
                    break;
                shadows.Add(new Point(x, y));
                if (!InBounds(x + 1, MapWidth) && !(map[x + 1, y] is Wall))
                    shadows.Add(new Point(x + 1, y));
                if (!InBounds(x - 1, MapWidth) && !(map[x - 1, y] is Wall))
                    shadows.Add(new Point(x - 1, y));
                if (!InBounds(y + 1, MapHeight) && !(map[x, y + 1] is Wall))
                    shadows.Add(new Point(x, y + 1));
                if (!InBounds(y - 1, MapHeight) && !(map[x, y - 1] is Wall))
                    shadows.Add(new Point(x, y - 1));
            }
        }

        private static bool HaveSpace(int x, int y, HashSet<Point> shadows, IMoveble creature)
        {
            if (x + 1 < MapWidth && (Math.Abs(x + 1 - creature.Position.X) + Math.Abs(y - creature.Position.Y) <= vision && map[x + 1, y] is InotShadow) && !shadows.Contains(new Point(x + 1, y)))
                return true;
            if (x - 1 > 0 && (Math.Abs(x - 1 - creature.Position.X) + Math.Abs(y - creature.Position.Y) <= vision && map[x - 1, y] is InotShadow)&& !shadows.Contains(new Point(x - 1, y)))
                return true;
            if (y + 1 < MapHeight && (Math.Abs(x - creature.Position.X) + Math.Abs(y + 1 - creature.Position.Y) <= vision && map[x, y + 1] is InotShadow) && !shadows.Contains(new Point(x, y + 1)))
                return true;
            if (y - 1 > 0 && (Math.Abs(x - creature.Position.X) + Math.Abs(y - 1 - creature.Position.Y) <= vision && map[x, y - 1] is InotShadow) && !shadows.Contains(new Point(x, y - 1)))
                return true;
            shadows.Add(new Point(x, y));
            return false;
        }
    }
}
