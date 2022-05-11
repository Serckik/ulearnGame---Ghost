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
        private const string WithoutDanger = @"
WWWWWWWW
WEEEWKKW
DPEEEEEW
WEEEWKKW
WWWWWWWW";

        private const string Run = @"
WWWWWWWWWWWWW
WMEEPKKKKEEED
WWWWWWWWWWWWW";

        private const string Defence = @"
WWWWWWWWWWWWW
WPEEEEKKKEEMD
WWWWWWWWWWWWW";

        private const string FirstLevel = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEWWKEWEEEEEEEEEE|
/EWEWWWWWWWWEWEEEWWEWEWWWWEWEWW|
/EEEWEEEWEEEEWEEEWEEWEWKEWEWEEE|
/EWEWEWEEEWKEWEEWWEEWEEEEEEWEWE|
/EWEWEWWWWWWWWWEWWEWWWWWWWWWEWE|
/EWEWEEEEEEEEEEEEEEEEEEEEEEEEWE|
/EWEWEEWWWWWWWWWWWWWWWWWWWWWEWE|
/EWEEEEWEEEEEEEEEEEEEEEEWKEWEWE|
/EWWWWWWEWWWWWWWWWWWWWWEWEEWEWE|
/EEEEEEEEWDEEEEEEEEEEEWEWEEEEWE|
/WWWWWWWEWWWWWWEWWWWEEWEWWWWEWE|
/EEEEEEEEEEEEEEEEEEWKEWEEEEEEEE|
/EWWWWWWEWWWWWWEWWEWWWWEWWWWWWW|
/EWWEEEEEEEEEEWEWEEEWEEEWEEEWEE|
/EEWEWWWEWWWWEWPWEWEEEWEEEWEEEE|
/KEWEWEEEEEEWEWEWEEEWEEEWEEEWEK|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string SecondLevel = @"
WWWWWWWWWWWWWWWDWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEPEEEEEEEEEEEEEEE|
/EWWWWWWWWWWWWWEWWWWWWWWWWWWWWE|
/EWKEEEEEEEWEEEEEEEWEEEEEEEEKWE|
/EWWWWEEEEEEEEEEEEEEEEEEEEWWWWE|
/EWEEWEEEEEWWWWEWWWWEEEEEEWEEWE|
/EWEEWEEEEEWEEEEEEEWEEEEEEWEEWE|
/EWEEWWWEWWWEEEEEEEWWWWEWWWEEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EWEEWWWEWWWEEEEEEEWWWWEWWWEEWE|
/EWEEWEEEEEWEEEEEEEWEEEEEEWEEWE|
/EWEEWEEEEEWEEEEEEEWEEEEEEWEEWE|
/EWWWWEEEEEWWWWEWWWWEEEEEEWWWWE|
/EWEEEEEEEEEEEEEEEEEEEEEEEEEEWE|
/EWKEEEEEEEWEEEEEEEWEEEEEEEEKWE|
/EWWWWWWWWWWWWWEWWWWWWWWWWWWWWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string emptyMap = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEPEEEEEEEEEEEEEEEE|
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
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string Maze = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEMEEEPEEEEMEEEEMEEE|
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
/MEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string CellMap = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/WEWEWEWEWEWEWEWEWEWEWEWEWEWEWE|
/EEEEEEEEEEEEEEEEEEEEEEEEEPEEEE|
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

        private const string TestShadow = @"
WWWWWWWWWW
WMEEEEEEPW
WWEWWWWEWW
EWEEEEEEEW
EWWWWWWWWW";

        private const string OneBlock = @"
EEEEEEEEEEW
EEEEEEEEEEW
EEEEEEEEEEW
EEEEEEEEEEW
EEEEEPEEEEW
EEEEEEEEEEW
EEEEEEEEEEW
EEEEEEEEEEW
EEEEEEEEEEW";

        private const string KeyAndDoorsTest = @"
WWWWWWWWWWWW
WEEEEEKEEWWW
WEEKEPEEEDWW
WKEEEEEKEWWW
WWWWWWWWWWWW";

        public static int keysCount = 0;

        private static List<Level> listMap = new List<Level>
        {
            new Level(WithoutDanger, true, 0),
            new Level(Run, true, 1),
            new Level(Defence, true, 1),
            new Level(FirstLevel, false, 2),
            new Level(SecondLevel, false, 1)
        };

        public static IPlaceable[,] CreateMap(int level)
        {
            keysCount = 0;
            return CreateMap(new Level(FirstLevel, false, 2));
        }

        private static IPlaceable[,] CreateMap(Level levelInfo)
        {
            var map = levelInfo.Map;
            string separator = map[0] == '\r' ? "\r\n" : "\n";
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            var result = new IPlaceable[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = CreateCreatureBySymbol(rows[y][x], x, y);
            if (levelInfo.isTutorial) return result;
            return levelInfo.GetMapWithMonsterPosition(result);
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
                case 'K':
                    {
                        keysCount++;
                        return new Key(Resource1.Key, x, y);
                    }
                case 'D':
                    return new ClosedDoor(Resource1.ClosedDoor, x, y);
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
