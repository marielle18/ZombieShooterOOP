using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieShooterOOP
{
    public class ZombieFactory
    {
        public Enemy CreateEnemy(int x, int y) => new Zombies(x, y);
    }
}
