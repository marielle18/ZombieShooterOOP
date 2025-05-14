using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public interface IGameView
    {
        void UpdateUI(int health, int score, int ammo);
        PictureBox GetPlayer();
        void ShowGameOver();
        void AddZombieToUI(PictureBox zombie);
        void RemoveZombieFromUI(PictureBox zombie);
        void AddBulletToUI(PictureBox bullet);
        void RemoveBulletFromUI(PictureBox bullet);


        void AddAmmoToUI(PictureBox ammo);
        void RemoveAmmoFromUI(PictureBox ammo);
    }
}
