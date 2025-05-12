using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public interface IGameView
    {
        PictureBox Player { get; }
        void UpdateHealthBar(int health);
        void UpdateAmmo(int ammo);
        void UpdateScore(int score);
        void UpdatePlayerImage(string direction);
        void DisplayGameOver();
        bool IsGameOver();
        Size GetClientSize();
    }
}