using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulearngame1
{
    class Level
    {
        public bool isTutorial;
        public string Map;
        private int monsterCount;
        public Level(string level, bool isTutorial, int monsterCount)
        {
            this.monsterCount = monsterCount;
            Map = level;
            this.isTutorial = isTutorial;
        }

        public IPlaceable[,] GetMapWithMonsterPosition(IPlaceable[,] map)
        {
            for(var i = 0; i < monsterCount; i++)
            {
                var random = new Random();
                var position = new Point(random.Next(0, map.GetLength(0)), random.Next(0, map.GetLength(1)));
                while (!(map[position.X, position.Y] is Floor)
                    || Math.Abs(GameModel.player.Position.X - position.X) <= 5
                    || Math.Abs(GameModel.player.Position.Y - position.Y) <= 5)
                {
                    position = new Point(random.Next(0, map.GetLength(0)), random.Next(0, map.GetLength(1)));
                }
                map[position.X, position.Y] = new Monster(position.X, position.Y);
            }
            return map;
        }
    }
}
