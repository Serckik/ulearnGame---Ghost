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
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEMEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEPEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
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
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        public static IPlaceable[,] CreateMap()
        {
            return CreateMap(emptyMap);
        }

        private static IPlaceable[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            var result = new IPlaceable[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = CreateCreatureBySymbol(rows[y][x], x, y);
            return result;
        }
        private static IPlaceable CreateCreatureBySymbol(char c, int x, int y)
        {
            switch (c)
            {
                case 'P':
                    return new Player(x, y);
                case 'M':
                    return new Monster(x, y);
                case 'E':
                    return new Floor(x, y);
                case 'W':
                    return new Wall(Resource1.Wall, x, y);
                case '/':
                    return new Wall(Resource1.WallLeft, x, y);
                case '|':
                    return new Wall(Resource1.WallRight, x, y);
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
