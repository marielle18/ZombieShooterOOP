using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(string type, int x, int y)
        {
            switch (type)
            {
                case "Zombie": return new Zombie(x, y);
                default: return new Enemy(x, y, type, 50, 2);
            }
        }
    }

}
