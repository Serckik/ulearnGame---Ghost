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
            var shadows = FindAndSetShadows();
            //var shadows = new List<Point>();
            var shift = 0;
            var start = player.Position.Y - vision;
            while(shift != vision + 1)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth) && 
                        (!shadows.Contains(new Point(player.Position.X - i, start))) &&
                        HaveSpace(player.Position.X - i, start, shadows))
                        animations.Add(map[player.Position.X - i, start]);
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) && map[player.Position.X + i, start] == map[player.Position.X, player.Position.Y])
                    {
                        map[player.Position.X, player.Position.Y] = new Floor(player.Position.X, player.Position.Y);
                        animations.Add(new Floor(player.Position.X, player.Position.Y));
                    }
                    if(!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X + i, start))) &&
                        HaveSpace(player.Position.X + i, start, shadows))
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
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X - i, start))) &&
                        HaveSpace(player.Position.X - i, start, shadows))
                        animations.Add(map[player.Position.X - i, start]);
                    if(!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X + i, start))) &&
                        HaveSpace(player.Position.X + i, start, shadows))
                        animations.Add(map[player.Position.X + i, start]);
                }
                start--;
                shift++;
            }
            animations.Add(player);

            shift = 0;
            start = player.Position.Y - vision;
            while (shift != vision + 1)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X - i, start))) &&
                        !HaveSpace(player.Position.X - i, start, shadows))
                        animations.Remove(map[player.Position.X - i, start]);
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X + i, start))) &&
                        !HaveSpace(player.Position.X + i, start, shadows))
                    {
                        animations.Remove(map[player.Position.X + i, start]);
                    }
                }
                start++;
                shift++;
            }
            shift = 0;
            start = player.Position.Y + vision;
            while (shift != vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X - i, start))) &&
                        !HaveSpace(player.Position.X - i, start, shadows))
                        animations.Remove(map[player.Position.X - i, start]);
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth) &&
                        (!shadows.Contains(new Point(player.Position.X + i, start))) &&
                        !HaveSpace(player.Position.X + i, start, shadows))
                        animations.Remove(map[player.Position.X + i, start]);
                }
                start--;
                shift++;
            }
        }

        public static List<Point> FindAndSetShadows()
        {
            var shift = 0;
            var start = player.Position.Y - vision;
            var shadows = new List<Point>();
            while (shift != vision + 1)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth))
                    {
                        if (map[player.Position.X - i, start] is Wall)
                            SetShadows(player.Position.X - i, start, shadows);
                    }
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth))
                    {
                        if (map[player.Position.X + i, start] is Wall)
                            SetShadows(player.Position.X + i, start, shadows);
                    }
                }
                start++;
                shift++;
            }
            shift = 0;
            start = player.Position.Y + vision;
            while (shift != vision)
            {
                for (var i = 0; i <= shift; i++)
                {
                    if (start < 0 || start >= MapHeight) continue;
                    if (i != 0 && !(player.Position.X - i < 0 || player.Position.X - i >= MapWidth))
                    {
                        if (map[player.Position.X - i, start] is Wall)
                            SetShadows(player.Position.X - i, start, shadows);
                    }
                    if (!(player.Position.X + i < 0 || player.Position.X + i >= MapWidth))
                    {
                        if (map[player.Position.X + i, start] is Wall)
                            SetShadows(player.Position.X + i, start, shadows);
                    }
                }
                start--;
                shift++;
            }
            return shadows;
        }

        public static void SetShadows(int x, int y, List<Point> shadows)
        {
            var xor = x;
            var yor = y;
            var delta = new Point();
            if (x - player.Position.X == 0 && y - player.Position.Y < 0)
                delta = new Point(0, -1);
            else if (x - player.Position.X == 0 && y - player.Position.Y > 0)
                delta = new Point(0, 1);
            else if (x - player.Position.X > 0 && y - player.Position.Y == 0)
                delta = new Point(1, 0);
            else if (x - player.Position.X < 0 && y - player.Position.Y == 0)
                delta = new Point(-1, 0);
            else if (x - player.Position.X > 0 && y - player.Position.Y > 0)
                delta = new Point(1, 1);
            else if (x - player.Position.X < 0 && y - player.Position.Y > 0)
                delta = new Point(-1, 1);
            else if (x - player.Position.X < 0 && y - player.Position.Y < 0)
                delta = new Point(-1, -1);
            else if (x - player.Position.X > 0 && y - player.Position.Y < 0)
                delta = new Point(1, -1);
            x += delta.X;
            y += delta.Y;
            while(Math.Abs(x - player.Position.X) + Math.Abs(y - player.Position.Y) <= vision)
            {
                if ((x < 0 || x >= MapWidth) && (y < 0 || y >= MapHeight))
                    break;
                shadows.Add(new Point(x, y));
                if((x + 1 != xor || y != yor) && !(x  + 1 < 0 || x + 1 >= MapWidth) && !(y < 0 || y >= MapHeight) && !(map[x + 1, y] is Wall))
                    shadows.Add(new Point(x + 1, y));
                if((x - 1 != xor || y != yor) && !(x - 1 < 0 || x - 1 >= MapWidth) && !(y < 0 || y >= MapHeight) && !(map[x - 1, y] is Wall))
                    shadows.Add(new Point(x - 1, y));
                if((x != xor || y + 1 != yor) && !(x < 0 || x >= MapWidth) && !(y + 1 < 0 || y + 1 >= MapHeight) && !(map[x, y + 1] is Wall))
                    shadows.Add(new Point(x, y + 1));
                if ((x != xor || y - 1 != yor) && !(x < 0 || x >= MapWidth) && !(y - 1 < 0 || y - 1 >= MapHeight) && !(map[x, y - 1] is Wall))
                    shadows.Add(new Point(x, y - 1));
                x += delta.X;
                y += delta.Y;
            }
        }

        public static bool HaveSpace(int x, int y, List<Point> shadows)
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
