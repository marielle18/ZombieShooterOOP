using System;
using System.Windows.Forms;
using ZombieShooterOOP.ZombieShooterOOP;


namespace ZombieShooterOOP
{
    public class GamePresenter
    {
        private IGameView view;
        private GameModel model;
        private int speed = 5;

        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        private string playerDirection = "down";
        internal bool gameOver;

        public GamePresenter(IGameView view)
        {
            this.view = view;
            model = GameModel.Instance;
        }

        public void StartGame()
        {
            model.Reset();
            UpdateUI();
        }

        public void MovePlayer()
        {
            if (GoLeft && view.Player.Left > 0)
            {
                view.Player.Left -= speed;
                playerDirection = "left";
            }
            if (GoRight && view.Player.Left + view.Player.Width < view.GetClientSize().Width)
            {
                view.Player.Left += speed;
                playerDirection = "right";
            }
            if (GoUp && view.Player.Top > 40)
            {
                view.Player.Top -= speed;
                playerDirection = "up";
            }
            if (GoDown && view.Player.Top + view.Player.Height < view.GetClientSize().Height)
            {
                view.Player.Top += speed;
                playerDirection = "down";
            }

            view.UpdatePlayerImage(playerDirection);
        }

        public void Shoot()
        {
            if (model.Ammo > 0)
            {
                model.Ammo--;
                view.UpdateAmmo(model.Ammo);
            }
        }


        public void AddScore(int points)
        {
            model.Score += points;
            view.UpdateScore(model.Score);
        }

        public void DamagePlayer(int damage)
        {
            model.PlayerHealth -= damage;
            if (model.PlayerHealth <= 0)
            {
                model.PlayerHealth = 0;
                model.IsGameOver = true;
                view.DisplayGameOver();
            }
            view.UpdateHealthBar(model.PlayerHealth);
        }

        private void UpdateUI()
        {
            view.UpdateHealthBar(model.PlayerHealth);
            view.UpdateAmmo(model.Ammo);
            view.UpdateScore(model.Score);
        }

        public string GetPlayerDirection()
        {
            return playerDirection;
        }
    }
}
