using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class Enemy
    {
        public string Type { get; set; }
        public int Health { get; set; }
        public PictureBox EnemyPictureBox { get; set; }
        public int Speed { get; set; }

        public Enemy(int x, int y, string type, int health, int speed)
        {
            Type = type;
            Health = health;
            Speed = speed;
            EnemyPictureBox = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(x, y),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
        }

        public virtual void MoveTowardsPlayer(PictureBox player)
        {
            int playerX = player.Left + player.Width / 2;
            int playerY = player.Top + player.Height / 2;
            int enemyX = EnemyPictureBox.Left + EnemyPictureBox.Width / 2;
            int enemyY = EnemyPictureBox.Top + EnemyPictureBox.Height / 2;

            if (playerX < enemyX) EnemyPictureBox.Left -= Speed;
            else if (playerX > enemyX) EnemyPictureBox.Left += Speed;

            if (playerY < enemyY) EnemyPictureBox.Top -= Speed;
            else if (playerY > enemyY) EnemyPictureBox.Top += Speed;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}