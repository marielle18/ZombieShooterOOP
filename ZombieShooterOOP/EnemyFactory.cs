using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class Zombie : Enemy
    {
        private Bitmap zUp, zDown, zLeft, zRight;

        public Zombie(int x, int y) : base(x, y, "Zombie", 100, 3)
        {
            zUp = Properties.Resources.zup;
            zDown = Properties.Resources.zdown;
            zLeft = Properties.Resources.zleft;
            zRight = Properties.Resources.zright;

            EnemyPictureBox.Image = zDown;
        }

        public override void MoveTowardsPlayer(PictureBox player)
        {
            int playerX = player.Left + player.Width / 2;
            int playerY = player.Top + player.Height / 2;
            int enemyX = EnemyPictureBox.Left + EnemyPictureBox.Width / 2;
            int enemyY = EnemyPictureBox.Top + EnemyPictureBox.Height / 2;

            int deltaX = playerX - enemyX;
            int deltaY = playerY - enemyY;

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                if (deltaX < 0)
                {
                    EnemyPictureBox.Image = zLeft;
                    EnemyPictureBox.Left -= Speed;
                }
                else
                {
                    EnemyPictureBox.Image = zRight;
                    EnemyPictureBox.Left += Speed;
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    EnemyPictureBox.Image = zUp;
                    EnemyPictureBox.Top -= Speed;
                }
                else
                {
                    EnemyPictureBox.Image = zDown;
                    EnemyPictureBox.Top += Speed;
                }
            }
        }
    }
}