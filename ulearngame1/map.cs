﻿using System;
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
/EEEEEEEEEEEEPEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEMEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string Maze = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEEEEEEEEEEEEMEEE|
/WWWWWWWWWWWWWWWWWWWWWWWWWWWWWP|
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
/MEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string CellMap = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEM|
/WEWEWEWEWEWEWEWEWEWEWEWEWPWEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWEWEWEWEWEWEWEWEWEWEWEWEWEWEW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WEWEWEWEWEWEWEWEWEWEWEWEWEWEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWEWEWEWEWEWEWEWEWEWEWEWEWEWEW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WEWEWEWEWEWEWEWEWEWEWEWEWEWEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWEWEWEWEWEWEWEWEWEWEWEWEWEWEW|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WEWEWEWEWEWEWEWEWEWEWEWEWEWEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        public static IPlaceable[,] CreateMap()
        {
            return CreateMap(CellMap);
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
                    {
                        GameModel.player = new Player(x, y);
                        return new Player(x, y);
                    }
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
