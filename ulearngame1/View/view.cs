using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using NAudio.Wave;
using System.IO;
using static ulearngame1.Map;

namespace ulearngame1
{
    class View
    {
        private static bool VisionMusicActive;
        private static bool VisionMusicDisactive = true;
        private static bool AtackMusicActive;
        public static bool MonsterSeeMusic;
        public static bool HeartBreak;
        public static bool HeartBreakTime;
        public static bool RunMusicActivate;
        public static int Bit;
        public static WaveOutEvent outputSound = new WaveOutEvent();
        public static WaveOutEvent HeartSound = new WaveOutEvent();
        public static WaveOutEvent runMusic = new WaveOutEvent();
        public static WaveOutEvent waveOut;
        public static void UpdateSound(string sound)
        {
            outputSound = new WaveOutEvent();
            var dir = Directory.GetParent(Directory.GetCurrentDirectory());
            var path = Directory.GetParent(dir.ToString()).ToString();
            if(sound == "run")
            {
                WaveFileReader reader = new WaveFileReader(path + "/Resources/runMusic.wav");
                LoopStream loop = new LoopStream(reader);
                runMusic = new WaveOutEvent();
                runMusic.Init(loop);
                runMusic.Play();
            }
            if (sound == "atack")
            {
                var audioFile = new AudioFileReader(path + "/Resources/AtackMusic3.wav");
                outputSound.Init(audioFile);
            }
            if (sound == "visionOn")
            {
                var audioFile = new AudioFileReader(path + "/Resources/nightvision-on.wav");
                outputSound.Init(audioFile);
            }
            if (sound == "visionOff")
            {
                var audioFile = new AudioFileReader(path + "/Resources/nightvision-off.wav");
                outputSound.Init(audioFile);
            }
            if (sound == "key")
            {
                var audioFile = new AudioFileReader(path + "/Resources/key.wav");
                outputSound.Init(audioFile);
            }
            if (sound == "door")
            {
                var audioFile = new AudioFileReader(path + "/Resources/door.wav");
                outputSound.Init(audioFile);
            }
            if (sound == "heartBreak")
            {
                WaveFileReader reader = new WaveFileReader(path + "/Resources/heartbeat.wav");
                LoopStream loop = new LoopStream(reader);
                HeartSound = new WaveOutEvent();
                HeartSound.Init(loop);
                HeartSound.Play();
            }
            outputSound.Volume = 0.5f;
            if(sound != "heartBreak" && sound != "run")
                outputSound.Play();
        }

        public static void PlayMusic()
        {
            if (waveOut == null)
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
                runMusic.Stop();
                RunMusicActivate = false;
                HeartSound.Stop();
                HeartBreak = false;
                HeartBreakTime = false;
                Bit = 0;
                GameModel.IsUpdated = true;
                MonsterSeeMusic = false;
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

        public static void UpdateTextures(Graphics g, bool keyPressed)
        {
            if (!Form1.isPused)
            {
                if (GameModel.LevelIsStarted)
                {
                    PlayMusic();
                    GameModel.LevelIsStarted = false;
                }

                var anyMonsterSee = GameModel.VisionObjects.Where(x => x is Monster).Where(x => ((Monster)x).IsVisible).ToList().Count != 0;
                foreach (var item in GameModel.VisionObjects)
                    if (!(item is IMoveble))
                        item.PlayAnimation(g, keyPressed);
                foreach (var item in GameModel.VisionObjects)
                    if (item is IMoveble)
                    {
                        if (item is Player)
                        {
                            GameModel.player = (Player)item;
                            GameModel.player.PlayAnimation(g, keyPressed);
                            GameModel.player.PlayerMove();
                            if (GameModel.IsUpdated)
                            {
                                GameModel.IsUpdated = false;
                                return;
                            };
                            if (GameModel.player.atack && !AtackMusicActive)
                            {
                                AtackMusicActive = true;
                                UpdateSound("atack");
                            }
                            else if (!GameModel.player.atack)
                                AtackMusicActive = false;

                            if (GameModel.player.VisionActivate && !VisionMusicActive && GameModel.player.MonstersAreVisible != 0)
                            {
                                VisionMusicActive = true;
                                VisionMusicDisactive = false;
                                UpdateSound("visionOn");
                            }
                            else if (!GameModel.player.VisionActivate)
                            {
                                VisionMusicActive = false;
                                if (!VisionMusicDisactive)
                                {
                                    UpdateSound("visionOff");
                                    anyMonsterSee = false;
                                }
                                VisionMusicDisactive = true;
                            }

                            if (GameModel.player.IsKeyCollect)
                            {
                                GameModel.player.IsKeyCollect = false;
                                UpdateSound("key");
                            }

                            if (GameModel.player.DoorOpen)
                            {
                                GameModel.player.DoorOpen = false;
                                UpdateSound("door");
                            }

                            if (anyMonsterSee && !MonsterSeeMusic && !VisionMusicActive)
                            {
                                MonsterSeeMusic = true;
                                Bit = 0;
                                if (HeartSound != null)
                                    HeartSound.Stop();
                            }
                            else if (!anyMonsterSee && (MonsterSeeMusic || HeartBreakTime))
                            {
                                if (!HeartBreakTime && Bit == 0)
                                {
                                    UpdateSound("heartBreak");
                                    HeartBreakTime = true;
                                }

                                if (HeartBreakTime)
                                    Bit++;
                                if (Bit == 100)
                                {
                                    Bit = 0;
                                    HeartSound.Stop();
                                    HeartBreakTime = false;
                                    MonsterSeeMusic = false;
                                }
                            }
                        }
                        else if (item is Monster)
                        {
                            var monster = (Monster)item;
                            monster.MonsterMove();
                            if (monster.IsVisible)
                                monster.PlayAnimation(g, keyPressed);
                            if (GameModel.IsUpdated)
                            {
                                GameModel.IsUpdated = false;
                                return;
                            };

                            if (anyMonsterSee && !RunMusicActivate && !VisionMusicActive)
                            {
                                RunMusicActivate = true;
                                waveOut.Pause();
                                UpdateSound("run");
                            }
                            else if (!anyMonsterSee && !HeartBreakTime && RunMusicActivate)
                            {
                                waveOut.Play();
                                runMusic.Stop();
                                RunMusicActivate = false;
                            }
                        }
                    }
                g.DrawString("Осталось ключей: " + (Map.keysCount - GameModel.KeysFound).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 0, 0);
                g.DrawString("Оглушение: " + (GameModel.player.Power).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 300, 0);
                g.DrawString("Сканирование: " + (GameModel.player.MonstersAreVisible).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 600, 0);
                g.DrawString("| " + Map.StringNameLevels[GameModel.level] + " |", new Font("Impact", 16), new SolidBrush(Color.Gold), 900, 0);
                if (GameModel.level == 0)
                {
                    g.DrawString("Движение W A S D или стрелочки", new Font("Impact", 16), new SolidBrush(Color.Gold), 500, 500);
                    g.DrawString("Чтобы пройти уровень, нужно собрать все ключи", new Font("Impact", 16), new SolidBrush(Color.Gold), 450, 600);
                }
                if(GameModel.level == 1)
                    g.DrawString("На каждом уровне вас будет ждать голодный монстр, не дайте ему вас поймать", new Font("Impact", 16), new SolidBrush(Color.Gold), 500, 500);
                if (GameModel.level == 2)
                    g.DrawString("Если от монстра не убежать, то используйте оглушение на Q", new Font("Impact", 16), new SolidBrush(Color.Gold), 500, 500);
                if (GameModel.level == 3)
                    g.DrawString("Если хотите узнать где монстр, то используйте просветку на E", new Font("Impact", 16), new SolidBrush(Color.Gold), 500, 800);
                if (GameModel.level == listMap.Count - 1)
                {
                    g.DrawString("Поздравляю, ты сбежал из этого пугающего места, теперь ты можешь отдохнуть. Спасибо за то, что поиграли!!!", new Font("Impact", 16), new SolidBrush(Color.Gold), 400, 500);
                    g.DrawString("И отсюда нет выхода... Вы уверены, что сбежали?", new Font("Impact", 16), new SolidBrush(Color.Gold), 700, 600);
                }
            }
            else
            {
                g.DrawImage(Resource1.Background, new Point(0, 0));
                Menu.MainMenu();
            }
        }
    }
}
