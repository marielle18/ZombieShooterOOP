using System;
using System.Drawing;
using System.Windows.Forms;

public class Enemy
{
    public string Type { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public PictureBox EnemyPictureBox { get; set; }
    public bool IsDead()
        {
            return Health <= 0;
        }



    public Enemy(int x, int y, string v, int v1, int v2)
    {
        EnemyPictureBox = new PictureBox
        {
            Location = new Point(x, y),
            Size = new Size(50, 50),
            BackColor = Color.Transparent,
            SizeMode = PictureBoxSizeMode.AutoSize
        };
    }
    public virtual void MoveTowardsPlayer(PictureBox player)
    {
        
    }



}

