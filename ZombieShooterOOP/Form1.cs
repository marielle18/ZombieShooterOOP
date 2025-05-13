using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public partial class Form1 : Form, IGameView
    {
        private List<Bullet> bullets = new List<Bullet>();
        private List<Zombie> zombiesList = new List<Zombie>(); 

        private string facing = "up";
        private int playerHealth = 100;
        private bool goLeft, goRight, goUp, goDown;
        private int speed = 10;
        private int ammo = 10;
        private int score;
        private Random randNum = new Random();

        public PictureBox Player => player;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
            GameTimer.Interval = 20;
            GameTimer.Tick += MainTimerEvent;
            GameTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                MakeZombie();
            }
        }

        public void UpdateHealthBar(int health)
        {
            health = Math.Max(0, Math.Min(health, healthBar.Maximum));
            healthBar.Value = health;
        }

        public void UpdateAmmo(int ammo)
        {
            txtAmmo.Text = "Ammo: " + ammo;
        }

        public void UpdateScore(int score)
        {
            txtScore.Text = "Kills: " + score;
        }

        public void UpdatePlayerImage(string direction)
        {
            facing = direction;
            switch (direction)
            {
                case "left":
                    player.Image = Properties.Resources.left;
                    break;
                case "right":
                    player.Image = Properties.Resources.right;
                    break;
                case "up":
                    player.Image = Properties.Resources.up;
                    break;
                case "down":
                    player.Image = Properties.Resources.down;
                    break;
            }
        }

        public void DisplayGameOver()
        {
            MessageBox.Show("Game Over!");
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (goLeft && player.Left > 0)
                player.Left -= speed;
            if (goRight && player.Right < this.ClientSize.Width)
                player.Left += speed;
            if (goUp && player.Top > 0)
                player.Top -= speed;
            if (goDown && player.Bottom < this.ClientSize.Height)
                player.Top += speed;

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();

                if (bullets[i].IsOutOfBounds(this.ClientSize))
                {
                    this.Controls.Remove(bullets[i].BulletPanel);
                    bullets.RemoveAt(i);
                }
            }

            
            for (int i = zombiesList.Count - 1; i >= 0; i--)
            {
                Zombie zombie = zombiesList[i];  

                zombie.MoveTowardsPlayer(player);

                if (zombie.IsDead())
                {
                    this.Controls.Remove(zombie.EnemyPictureBox);
                    zombiesList.RemoveAt(i);
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = true;
                    UpdatePlayerImage("left");
                    break;
                case Keys.Right:
                    goRight = true;
                    UpdatePlayerImage("right");
                    break;
                case Keys.Up:
                    goUp = true;
                    UpdatePlayerImage("up");
                    break;
                case Keys.Down:
                    goDown = true;
                    UpdatePlayerImage("down");
                    break;
                case Keys.Space:
                    ShootBullet(facing);
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = false;
                    break;
                case Keys.Right:
                    goRight = false;
                    break;
                case Keys.Up:
                    goUp = false;
                    break;
                case Keys.Down:
                    goDown = false;
                    break;
            }
        }

        private void ShootBullet(string direction)
        {
            int bulletX = player.Left + (player.Width / 2) - 4;
            int bulletY = player.Top + (player.Height / 2) - 4;

            Bullet bullet = new Bullet(bulletX, bulletY, direction);
            bullet.MakeBullet(this);
            bullets.Add(bullet);
        }

        private void MakeZombie()
        {
            ZombieFactory factory = new ZombieFactory();

            Zombie zombie = (Zombie)factory.CreateEnemy(
                randNum.Next(0, this.ClientSize.Width - 50),
                randNum.Next(0, this.ClientSize.Height - 50)
            );

            zombiesList.Add(zombie); 

            PictureBox zombiePictureBox = zombie.EnemyPictureBox;
            zombiePictureBox.Tag = zombie;

            this.Controls.Add(zombiePictureBox);
            zombiePictureBox.BringToFront();
        }

        public bool IsGameOver()
        {
            return healthBar.Value <= 0;
        }

        public Size GetClientSize()
        {
            return this.ClientSize;
        }
    }

}
