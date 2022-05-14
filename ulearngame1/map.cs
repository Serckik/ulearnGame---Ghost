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
WEEEWKKW
DPEEEEEW
WEEEWKKW
WWWWWWWW";

        private const string Run = @"
WWWWWWWWWWWWW
WMEEPEEEEEEED
WWWWWWWWWWWWW";

        private const string Defence = @"
WWWWWWWWWWWWW
WPEEEEEEEEEMD
WWWWWWWWWWWWW";

        private const string level01 = @"
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

        private const string level02 = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
/EEEEEEEWEEWEEEEWEEEEWEKEWEEEEW|
/EEEWWEEEEEEEWWEEEWEEEEEEEEWKEE|
/EEWWWWEEWWWWWWKWWWWWWWEWWWWWWE|
/EWWWWWWKWKEEWWEWEEEEEEEEEWWWWE|
/EEWEWWWWWWEEEEEEEWWWEWWWEEEEEE|
/EEEEEEEEEEEEEEWWWWKEEEKWWWWEEW|
/EEWEWWWWWWEEEEEEWWWWEWWWWEEEEW|
/EEEEEEEEEEEEWWWEEEEEEEEEEEWWWW|
/EWWWWEWWWWWEWEWWWWWWEWWWWWWWKE|
/EWWWWEWWWWEEEEEEWWEEEWWWEEEWWE|
/EEEEWEWWWWWWWWWWWEEEEEEEEEEEWE|
/WWEEWEEEEEEEEEWEEEWEEEKEEEWEEE|
/WWEEWEWEEWWEWEEEWEEEEEEEEEEEWW|
/EEEEEEWEEEEEWWWWWWEEEWWWEEEWWW|
/EWEEWEWEWWWWWKEEWWWWWWWWWWEWWW|
/KWEEWEEEEEEEEEEPDWWWWWWWWWEEEK|
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
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEPEEEEEEEEEEEEEEEE|
/EEEEEEEEEEEEEEEEEEEEEEEEEEWEEE|
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
EEEEEEMEEEW
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
        public static WaveOutEvent waveOut;

        private static List<Level> listMap = new List<Level>
        {
            new Level(WithoutDanger, true, 0),
            new Level(Run, true, 1),
            new Level(Defence, true, 1),
            new Level(level01, false, 2),
            new Level(level02, false, 2)
        };

        public static string[] StringNameLevels = new string[] { "Просто уровень", "Беги", "Защищайся", "Уровень 1", "Уровень 2" };

        public static IPlaceable[,] CreateMap(int level)
        {
            keysCount = 0;
            PlayMusic();
            return CreateMap(listMap[level]);
        }

        public static void PlayMusic()
        {
            if(waveOut == null)
            {
                var dir = Directory.GetParent(Directory.GetCurrentDirectory());
                var path = Directory.GetParent(dir.ToString()).ToString();
                WaveFileReader reader = new WaveFileReader(path + "/Resources/mainMusic.wav");
                LoopStream loop = new LoopStream(reader);
                waveOut = new WaveOutEvent();
                waveOut.Init(loop);
                waveOut.Play();
            }
            else
            {
                waveOut.Play();
                View.runMusic.Stop();
                View.RunMusicActivate = false;
                View.HeartSound.Stop();
                View.HeartBreak = false;
                View.HeartBreakTime = false;
                View.Bit = 0;
                GameModel.IsUpdated = true;
            }
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

        public class LoopStream : WaveStream
        {
            WaveStream sourceStream;
            public LoopStream(WaveStream sourceStream)
            {
                this.sourceStream = sourceStream;
                this.EnableLooping = true;
            }
            public bool EnableLooping { get; set; }
            public override WaveFormat WaveFormat
            {
                get { return sourceStream.WaveFormat; }
            }
            public override long Length
            {
                get { return sourceStream.Length; }
            }
            public override long Position
            {
                get { return sourceStream.Position; }
                set { sourceStream.Position = value; }
            }
            public override int Read(byte[] buffer, int offset, int count)
            {
                int totalBytesRead = 0;

                while (totalBytesRead < count)
                {
                    int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                    if (bytesRead == 0)
                    {
                        if (sourceStream.Position == 0 || !EnableLooping)
                        {
                            break;
                        }
                        sourceStream.Position = 0;
                    }
                    totalBytesRead += bytesRead;
                }
                return totalBytesRead;
            }
        }
    }
}
