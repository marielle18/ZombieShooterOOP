using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class ZombieFactory
    {
        public Enemy CreateEnemy(int x, int y)
        {
            return new Zombie(x, y);
        }
    }
}


