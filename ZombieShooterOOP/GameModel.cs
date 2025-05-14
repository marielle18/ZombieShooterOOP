using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class GameModel
    {
        public int PlayerHealth { get; set; }
        public int Score { get; set; }
        public int Ammo { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public string Facing { get; set; } = "up";

        public bool GameOver { get; set; }
        public List<PictureBox> Zombies { get; set; }
        public List<PictureBox> Bullets { get; set; } = new List<PictureBox>();
        public List<PictureBox> AmmoItems { get; set; } = new List<PictureBox>();
        public Timer GameTimer { get; set; }
        public Random RandNum { get; set; }

        public GameModel()
        {
            PlayerHealth = 100;
            Score = 0;
            Ammo = 10;
            Zombies = new List<PictureBox>();
            Bullets = new List<PictureBox>();
            GameTimer = new Timer { Interval = 20 };
            RandNum = new Random();
        }

        public void AddZombie(PictureBox zombie)
        {
            Zombies.Add(zombie);
        }

        public void RemoveZombie(PictureBox zombie)
        {
            Zombies.Remove(zombie);
        }

        public void AddAmmo(PictureBox ammo)
        {
            AmmoItems.Add(ammo);
        }

        public void RemoveAmmo(PictureBox ammo)
        {
            AmmoItems.Remove(ammo);
        }

        public void UpdateZombieMovement(PictureBox zombie, PictureBox player)
        {
            int speed = 1;

            if (zombie.Left > player.Left) zombie.Left -= speed;
            else if (zombie.Left < player.Left) zombie.Left += speed;

            if (zombie.Top > player.Top) zombie.Top -= speed;
            else if (zombie.Top < player.Top) zombie.Top += speed;
        }


        public void RestartGame()
        {
            PlayerHealth = 100;
            Score = 0;
            Ammo = 10;
            GameOver = false;
            Zombies.Clear();
            Bullets.Clear();
            AmmoItems.Clear();
        }


    }
}
