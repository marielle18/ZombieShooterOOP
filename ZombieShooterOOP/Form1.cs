using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public partial class Form1 : Form, IGameView
    {
        private GamePresenter _presenter;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            GameModel model = new GameModel();
            _presenter = new GamePresenter(this, model);
        }

        public void UpdateUI(int health, int score, int ammo)
        {
            healthBar.Value = health;
            txtScore.Text = "Kills: " + score;
            txtAmmo.Text = "Ammo: " + ammo;
        }

        public PictureBox GetPlayer() => player;

        public void ShowGameOver()
        {
            MessageBox.Show("Game Over!");
        }

        public void AddZombieToUI(PictureBox zombie)
        {
            this.Controls.Add(zombie);
            zombie.BringToFront();
        }

        public void RemoveZombieFromUI(PictureBox zombie)
        {
            this.Controls.Remove(zombie);
            zombie.Dispose();
        }

        public void AddBulletToUI(PictureBox bullet)
        {
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        public void RemoveBulletFromUI(PictureBox bullet)
        {
            this.Controls.Remove(bullet);
            bullet.Dispose();
        }

        public void AddAmmoToUI(PictureBox ammo)
        {
            this.Controls.Add(ammo);
            ammo.BringToFront();
        }

        public void RemoveAmmoFromUI(PictureBox ammo)
        {
            this.Controls.Remove(ammo);
            ammo.Dispose();
        }
    }
}
