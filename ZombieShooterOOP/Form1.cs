using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace ZombieShooterOOP
{
    public partial class Form1 : Form, IGameView
    {
        private List<Bullet> bullets = new List<Bullet>();
        private List<Enemy> zombies = new List<Enemy>();
        private string playerDirection = "up";

        private bool goLeft, goRight, goUp, goDown;
        private int playerSpeed = 5;

        public PictureBox Player => player;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Load += Form1_Load;
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
            playerDirection = direction;
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
                player.Left -= playerSpeed;
            if (goRight && player.Right < this.ClientSize.Width)
                player.Left += playerSpeed;
            if (goUp && player.Top > 0)
                player.Top -= playerSpeed;
            if (goDown && player.Bottom < this.ClientSize.Height)
                player.Top += playerSpeed;


            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();

                if (bullets[i].IsOutOfBounds(this.ClientSize))
                {
                    this.Controls.Remove(bullets[i].BulletPanel);
                    bullets.RemoveAt(i);
                }
            }


            foreach (Enemy zombie in zombies)
            {
                zombie.MoveTowardsPlayer(player);
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                UpdatePlayerImage("left");
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                UpdatePlayerImage("right");
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                UpdatePlayerImage("up");
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                UpdatePlayerImage("down");
            }
            if (e.KeyCode == Keys.Space)
            {
                ShootBullet(playerDirection);
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                goLeft = false;
            if (e.KeyCode == Keys.Right)
                goRight = false;
            if (e.KeyCode == Keys.Up)
                goUp = false;
            if (e.KeyCode == Keys.Down)
                goDown = false;
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
            ZombiesFactory factory = new ZombiesFactory();
            Enemy zombie = GetZombie(factory);

            this.Controls.Add(zombie.EnemyPictureBox);
            zombie.EnemyPictureBox.BringToFront();
            zombies.Add(zombie);
        }

        private Enemy GetZombie(ZombiesFactory factory)
        {
            return factory.GetZombie(
                            x: new Random().Next(0, this.ClientSize.Width - 50),
                            y: new Random().Next(0, this.ClientSize.Height - 50)
                        );
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

    internal class ZombiesFactory
    {
        internal Enemy CreateEnemy(int x, int y)
        {
            return new Zombies(x, y)
            {
                Type = "Zombie",
                Health = 100,
                Speed = 2,
                EnemyPictureBox = new PictureBox
                {
                    Location = new Point(x, y),
                    Size = new Size(50, 50),
                    BackColor = Color.Green
                }
            };
        }

        internal Enemy GetZombie(int x, int y)
        {
           return CreateEnemy(x, y);
        }
    }
}
