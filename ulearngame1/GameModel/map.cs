using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NAudio.Wave;
using System.IO;

namespace ulearngame1
{
    class Map
    {
        private const string WithoutDanger = @"
WWWWWWWW
/EEE.KK/
OPEEEEE/
/EEE.KK/
WWWWWWWW";

        private const string Run = @"
WWWWWWWWWWWWWW
/MEEPEEEEEEEO/
WWWWWWWWWWWWWW";

        private const string Defence = @"
WWWWWWWWWWWWWW
/PEEEEEEEEEMO/
WWWWWWWWWWWWWW";

        private const string level01 = @"
WWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEEEEEEEEE/
/ERWERWERWWEDEDEWWWWWWE/
/E/EE/EE/EEE/E/EK/EEEEE/
/E/E./EE/EEK/E/EE/EEKDE/
/E/EE/EE/ERW/E/EEUEWWlE/
/E/.EUEEUEUE/E/EEEEEEEE/
/E/EEEEEEEEE/E/WWLE.EDE/
/E/EK.EEEEDK/O/EK/EEE/E/
/ErWWWWWWErWWWlErWWEEUK/
/EEEEEEEEEEEEPEEEEEEEE./
WWWWWWWWWWWWWWWWWWWWWWW/";

        private const string level02 = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEE./KE/EEEEEEEEEE/
/E.ERWWWWWWWEDEEE/.E/ERWWLEDEWW/
/EEE/EEE.EEEE/EEE/EE/EUKEUE/EEE/
/EDE/E.EEE.KE/EE./EE/EEEEEE/EDE/
/E/E/EWWWWWWWWWEWlEWWWWWWWWlE/E/
/E/E/EEEEEEEEEEEEEEEEEEEEEEEE/E/
/E/EUEERWWWWWWWWWWWWWWWWWWWLE/E/
/E/EEEE/EEEEEEEEEEEEEEEE/KE/E/E/
/ErWWWWlERWWWWWWWWWWWWLE/EEUE/E/
/EEEEEEEE/OEEEEEEEEEEE/E/EEEE/E/
/WWWWWWWErWWWWWEWWWLEE/ErWWWEUE/
/EEEEEEEEEEEEEEEEEE/KE/EEEEEEEE/
/EWWWWWWEWWWWWLERWErWWlEWWWWWWW/
/E./EEEEEEEEEE/E/EEE.EEE.EEE.EE/
/EE/ERWWEWWWLE/P/EEEEE.EEE.EEEE/
/KE/E/EEEEEE/E/E/EEE.EEE.EEE.EK/
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string level03 = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEE.EE.EEEE.EEEE.EKE.EEEE./
/EEERLEEEEEEERLEEE.EEEEEEEE.KEE/
/EERWWLEERWWWW/KRWWWWWWEWWWWWLE/
/EWWWWWLK/KEErlEUEEEEEEEEErWWlE/
/EEUErWWWWWEEEEEEERWWEWWLEEEEEE/
/EEEEEEEEEEEEEEWWW/KEEEK/WWWEER/
/EE.EWWWWWWEEEEEErWWWEWWWlEEEE//
/EEEEEEEEEEEERWLEEEEEEEEEEERWWW/
/ERWWLERWWWWEUErWWWWWERWWWWW/KE/
/ErWW/E/WW/EEEEEE/.EEErWlEEE.LE/
/EEEE/ErWWWWWWWWWlEEEEEEEEEEEUE/
/WLEE/EEEEEEEEE.EEE.EEEKEEE.EEE/
/WlEEUEDEE..EDEEE.EEEEEEEEEEERW/
/EEEEEE/EEEEE/WWWWLEEERWLEEERWW/
/EDEEDEUEWWWWlKEE/WWWWWWWWLErWW/
/K/EE/EEEEEEEEEEPOWWWWWWWW/EEEK/
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string endLevel = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEEEEEEEEPEEEEEEEEEEEEEEE/
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

        private const string TestLevel = @"
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
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEPEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE/
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
EEEEEEMEEEW
EEEEEEEEEEW
EEEKEPEEEEW
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

        public static List<Level> listMap = new List<Level>
        {
            new Level(WithoutDanger, true, 0),
            new Level(Run, true, 1),
            new Level(Defence, true, 1),
            new Level(level01, false, 1),
            new Level(level02, false, 2),
            new Level(level03, false, 2),
            new Level(endLevel, true, 0)
        };

        public static string[] StringNameLevels = new string[] { "Просто уровень", "Беги", "Защищайся", "Уровень 1", "Уровень 2", "Уровень 3", "Победа?" };

        public static IPlaceable[,] CreateMap(int level)
        {
            keysCount = 0;
            GameModel.LevelIsStarted = true;
            return CreateMap(listMap[6]);
        }

        public static IPlaceable[,] CreateMap(Level levelInfo)
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
                        return GameModel.player;
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
                case '.':
                    return new Wall(Resource1.soloBlock, x, y);
                case 'U':
                    return new Wall(Resource1.oneblockup, x, y);
                case 'D':
                    return new Wall(Resource1.oneblockdown, x, y);
                case 'L':
                    return new Wall(Resource1.angleupleft, x, y);
                case 'R':
                    return new Wall(Resource1.angeupright, x, y);
                case 'l':
                    return new Wall(Resource1.angledownleft, x, y);
                case 'r':
                    return new Wall(Resource1.angledownright, x, y);
                case 'K':
                    {
                        keysCount++;
                        return new Key(Resource1.Key, x, y);
                    }
                case 'O':
                    return new ClosedDoor(Resource1.ClosedDoor, x, y);
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
