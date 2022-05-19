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
        public static WaveOutEvent outputSound;
        public static WaveOutEvent HeartSound;
        public static WaveOutEvent runMusic;
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
            outputSound.Volume = 0.1f;
            if(sound != "heartBreak" && sound != "run")
                outputSound.Play();
        }

        public static void UpdateTextures(Graphics g, bool keyPressed)
        {
            if (!Form1.isPused)
            {
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
                                if(HeartSound != null)
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
                                Map.waveOut.Pause();
                                UpdateSound("run");
                            }
                            else if (!anyMonsterSee && !HeartBreakTime && RunMusicActivate)
                            {
                                Map.waveOut.Play();
                                runMusic.Stop();
                                RunMusicActivate = false;
                            }
                        }
                    }
                g.DrawString("Осталось ключей: " + (Map.keysCount - GameModel.KeysFound).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 0, 0);
                g.DrawString("Оглушение: " + (GameModel.player.Power).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 300, 0);
                g.DrawString("Сканирование: " + (GameModel.player.MonstersAreVisible).ToString(), new Font("Impact", 16), new SolidBrush(Color.Gold), 600, 0);
                g.DrawString("| " + Map.StringNameLevels[GameModel.level] + " |", new Font("Impact", 16), new SolidBrush(Color.Gold), 900, 0);
            }
            else
            {
                g.DrawImage(Resource1.Background, new Point(0, 0));
                Menu.MainMenu();
            }
        }
    }
}
