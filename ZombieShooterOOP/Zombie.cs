using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class Zombies : Enemy
    {
        public Bitmap ZUp, ZDown, ZLeft, ZRight;

        public Zombies(int x, int y) : base(x, y, "Zombie", 100, 3)
        {
            ZUp = Properties.Resources.zup;
            ZDown = Properties.Resources.zdown;
            ZLeft = Properties.Resources.zleft;
            ZRight = Properties.Resources.zright;

            EnemyPictureBox.Image = ZDown;
        }

        public override void MoveTowardsPlayer(PictureBox player)
        {
            int playerX = player.Left + player.Width / 2;
            int playerY = player.Top + player.Height / 2;
            int enemyX = EnemyPictureBox.Left + EnemyPictureBox.Width / 2;
            int enemyY = EnemyPictureBox.Top + EnemyPictureBox.Height / 2;

            int deltaX = playerX - enemyX;
            int deltaY = playerY - enemyY;

            if (System.Math.Abs(deltaX) > System.Math.Abs(deltaY))
            {
                if (deltaX < 0)
                {
                    EnemyPictureBox.Image = ZLeft;
                    EnemyPictureBox.Left -= Speed;
                }
                else
                {
                    EnemyPictureBox.Image = ZRight;
                    EnemyPictureBox.Left += Speed;
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    EnemyPictureBox.Image = ZUp;
                    EnemyPictureBox.Top -= Speed;
                }
                else
                {
                    EnemyPictureBox.Image = ZDown;
                    EnemyPictureBox.Top += Speed;
                }
            }
        }
    }
}