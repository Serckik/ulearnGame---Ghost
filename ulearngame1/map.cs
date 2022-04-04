using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ulearngame1
{
    class Map
    {
        private const string emptyMap = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEPEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEPEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string Maze = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/PEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WWWWWWWWWWWWWWWWWWWWWWWWWWWWWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWWWWWWWWWWWWWWWWWWWWWWWWWWWWW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WWWWWWWWWWWWWWWWWWWWWWWWWWWWWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWWWWWWWWWWWWWWWWWWWWWWWWWWWWW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WWWWWWWWWWWWWWWWWWWWWWWWWWWWWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWWWWWWWWWWWWWWWWWWWWWWWWWWWWW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WWWWWWWWWWWWWWWWWWWWWWWWWWWWWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        public static IPlaceable[,] map;
        public static int MapWidth => map.GetLength(0);
        public static int MapHeight => map.GetLength(1);

        public static void CreateMap()
        {
            map = CreateMap(emptyMap);
        }

        public static IPlaceable[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            var result = new IPlaceable[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = CreateCreatureBySymbol(rows[y][x]);
            return result;
        }
        private static IPlaceable CreateCreatureBySymbol(char c)
        {
            switch (c)
            {
                case 'P':
                    return new Player();
                case 'E':
                    return new Floor();
                case 'W':
                    return new Wall(Resource1.Wall);
                case '/':
                    return new Wall(Resource1.WallLeft);
                case '|':
                    return new Wall(Resource1.WallRight);
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
