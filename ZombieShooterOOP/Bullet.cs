using System;
using System.Drawing;
using System.Windows.Forms;

public class Bullet
{
    public int BulletLeft { get; set; }
    public int BulletTop { get; set; }
    public string Direction { get; set; }
    public Panel BulletPanel { get; set; }
    public int Speed { get; set; } = 15;

    public Bullet(int left, int top, string direction)
    {
        BulletLeft = left;
        BulletTop = top;
        Direction = direction;

        BulletPanel = new Panel();
        BulletPanel.BackColor = Color.Transparent;
        BulletPanel.Size = new Size(10, 10);
        BulletPanel.Left = BulletLeft;
        BulletPanel.Top = BulletTop;
        BulletPanel.BorderStyle = BorderStyle.None;

        BulletPanel.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            using (Brush glowBrush = new SolidBrush(Color.FromArgb(60, Color.Gold)))
            {
                e.Graphics.FillEllipse(glowBrush, -3, -3, BulletPanel.Width + 6, BulletPanel.Height + 6);
            }

            
            using (Brush bulletBrush = new SolidBrush(Color.Gold))
            {
                e.Graphics.FillEllipse(bulletBrush, 0, 0, BulletPanel.Width, BulletPanel.Height);
            }
        };
    }

    public void MakeBullet(Form form)
    {
        form.Controls.Add(BulletPanel);
        BulletPanel.BringToFront();
        BulletPanel.Invalidate(); 
    }

    public void Move()
    {
        switch (Direction)
        {
            case "left":
                BulletPanel.Left -= Speed;
                break;
            case "right":
                BulletPanel.Left += Speed;
                break;
            case "up":
                BulletPanel.Top -= Speed;
                break;
            case "down":
                BulletPanel.Top += Speed;
                break;
        }
    }

    public bool IsOutOfBounds(Size clientSize)
    {
        return BulletPanel.Right < 0 || BulletPanel.Left > clientSize.Width ||
               BulletPanel.Bottom < 0 || BulletPanel.Top > clientSize.Height;
    }
}
