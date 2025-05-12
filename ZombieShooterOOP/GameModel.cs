using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    namespace oop
    {
        public class GameModel
        {
            private static GameModel instance;

            public int PlayerHealth { get; set; } = 100;
            public int Ammo { get; set; } = 10;
            public int Score { get; set; } = 0;
            public bool IsGameOver { get; set; } = false;
            public string PlayerFacing { get; set; } = "up";

            private GameModel() { }

            public static GameModel Instance
            {
                get
                {
                    if (instance == null)
                        instance = new GameModel();
                    return instance;
                }
            }

            public void Reset()
            {
                PlayerHealth = 100;
                Ammo = 10;
                Score = 0;
                IsGameOver = false;
            }
        }
    }


}   