using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class Zombie : Enemy
    {
        public Zombie(int x, int y) : base(x, y, "Zombie", 100, 3)
        {

        }

        public override void MoveTowardsPlayer(PictureBox player)
        {

        }

    }
}
