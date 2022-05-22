using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ulearngame1
{
    [TestFixture]
    class GameTests
    {
        [Test]
        public void PlayerNotGoInWall()
        {
            var map = @"
WWWWW
WEEEW
WEPEW
WEEEW
WWWWW";

            GameModel.map = Map.CreateMap(new Level(map, true, 0));

            GameModel.player.right = true;
            GoToWall();
            GameModel.player.right = false;

            GameModel.player.left = true;
            GoToWall();
            GameModel.player.left = false;

            GameModel.player.top = true;
            GoToWall();
            GameModel.player.top = false;

            GameModel.player.bottom = true;
            GoToWall();
            GameModel.player.bottom = false;
        }

        private static void GoToWall()
        {
            while (true)
            {
                var playerLastPosition = new Point(GameModel.player.X, GameModel.player.Y);
                GameModel.player.PlayerMove();
                if (playerLastPosition == new Point(GameModel.player.X, GameModel.player.Y))
                    break;
                if (GameModel.map[GameModel.player.Position.X, GameModel.player.Position.Y] is Wall)
                    throw new AssertionException("Игрок находится в стене");
            }
        }

        [Test]
        public static void MonsterKillPlayer()
        {
            var map = @"
WWWW
WPMW
WWWW";
            GameModel.map = Map.CreateMap(new Level(map, true, 0));
            GameModel.LevelIsStarted = false;
            GameModel.Imoveble = GameModel.FindImoveble();
            GameModel.GetVision();
            var monsters = GameModel.Imoveble.Where(x => x is Monster).ToList();
            ((Monster)monsters[0]).MonsterMove();
            if (GameModel.LevelIsStarted == false)
                throw new AssertionException("Монстр не убил игрока");
        }

        [Test]
        public static void MonsterRunAndKillPlayer()
        {
            var map = @"
WWWWWWW
WPEEEMW
WWWWWWW";
            GameModel.map = Map.CreateMap(new Level(map, true, 0));
            GameModel.LevelIsStarted = false;
            GameModel.Imoveble = GameModel.FindImoveble();
            GameModel.GetVision();
            var monsters = GameModel.Imoveble.Where(x => x is Monster).ToList();
            while (!GameModel.LevelIsStarted)
            {
                ((Monster)monsters[0]).MonsterMove();
                if (GameModel.player.Position == ((Monster)monsters[0]).Position && !GameModel.LevelIsStarted)
                    throw new AssertionException("Монстр не убил игрока");
            }
        }

        [Test]
        public static void PlayerStunNearMonsters()
        {
            var map = @"
WWWWW
WMMMW
WMPMW
WMMMW
WWWWW";

            GameModel.map = Map.CreateMap(new Level(map, true, 0));
            GameModel.Imoveble = GameModel.FindImoveble();
            GameModel.GetVision();
            var monsters = GameModel.Imoveble.Where(x => x is Monster).ToList();
            GameModel.player.Power = 100;
            GameModel.player.CanStanMonster();
            if (monsters.Where(x => !((Monster)x).IsStanned).ToList().Count != 0)
                throw new AssertionException("Игрок не застанил монстра, который рядом с ним");

        }

        [Test]
        public static void PlayerNotStunNotNearMonsters()
        {
            var map = @"
WWWWWWW
WMMMMMW
WMEEEMW
WMEPEMW
WMEEEMW
WMMMMMW
WWWWWWW";

            GameModel.map = Map.CreateMap(new Level(map, true, 0));
            GameModel.Imoveble = GameModel.FindImoveble();
            GameModel.GetVision();
            var monsters = GameModel.Imoveble.Where(x => x is Monster).ToList();
            GameModel.player.Power = 100;
            GameModel.player.CanStanMonster();
            if (monsters.Where(x => ((Monster)x).IsStanned).ToList().Count != 0)
                throw new AssertionException("Игрок застанил монстра, который не рядом с ним");
        }
    }
}
