using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class Bullet
    {
        public string Direction { get; set; }
        public int BulletLeft { get; set; }
        public int BulletTop { get; set; }
        private int Speed = 20;
        public PictureBox BulletPictureBox;  
        private Timer BulletTimer;

        public Bullet(int left, int top, string direction)
        {
            BulletLeft = left;
            BulletTop = top;
            Direction = direction;

            BulletPictureBox = new PictureBox
            {
                BackColor = Color.White,
                Size = new Size(5, 5),
                Left = BulletLeft,
                Top = BulletTop,
                Tag = "bullet"
            };
        }

        
        public void CreateBullet(Form form)
        {
            form.Controls.Add(BulletPictureBox); 

            BulletTimer = new Timer { Interval = Speed };
            BulletTimer.Tick += BulletTimerEvent;
            BulletTimer.Start();
        }

 
        private void BulletTimerEvent(object sender, EventArgs e)
        {
            switch (Direction)
            {
                case "left":
                    BulletPictureBox.Left -= Speed;
                    break;
                case "right":
                    BulletPictureBox.Left += Speed;
                    break;
                case "up":
                    BulletPictureBox.Top -= Speed;
                    break;
                case "down":
                    BulletPictureBox.Top += Speed;
                    break;
            }


            if (BulletPictureBox.Left < 10 || BulletPictureBox.Left > 860 || BulletPictureBox.Top < 10 || BulletPictureBox.Top > 600)
            {
                BulletTimer.Stop();
                BulletTimer.Dispose();
                BulletPictureBox.Dispose();
                BulletTimer = null;
                BulletPictureBox = null;
            }
        }

        
        public Rectangle Bounds => BulletPictureBox.Bounds;
    }

}
