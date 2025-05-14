using System;
using System.Windows.Forms;

namespace ZombieShooterOOP
{
    public class GamePresenter
    {
        private readonly IGameView _view;
        private readonly GameModel _model;

        public GamePresenter(IGameView view, GameModel model)
        {
            _view = view;
            _model = model;

            _model.GameTimer.Tick += GameTimerEvent;
            _view.GetPlayer().Parent.KeyDown += KeyIsDown;
            _view.GetPlayer().Parent.KeyUp += KeyIsUp;

            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {

            if (_model.Score >= 20 && !_model.GameOver)
            {
                _model.GameOver = true;
                _model.GameTimer.Stop();
                MessageBox.Show("You Win! Congratulations!");


                Timer restartTimer = new Timer();
                restartTimer.Interval = 2000;
                restartTimer.Tick += (s, args) =>
                {
                    restartTimer.Stop();
                    RestartGame();
                };
                restartTimer.Start();
                return;
            }


            if (_model.PlayerHealth <= 0 && !_model.GameOver)
            {
                _model.GameOver = true;
                _model.GameTimer.Stop();


                _view.GetPlayer().Image = Properties.Resources.dead;

                _view.ShowGameOver();

                Timer restartGameTimer = new Timer();
                restartGameTimer.Interval = 2000;
                restartGameTimer.Tick += (s, args) =>
                {
                    restartGameTimer.Stop();
                    RestartGame();
                };
                restartGameTimer.Start();

                return;
            }


            _view.UpdateUI(_model.PlayerHealth, _model.Score, _model.Ammo);

            if (_model.GoLeft) _view.GetPlayer().Left -= 5;
            if (_model.GoRight) _view.GetPlayer().Left += 5;
            if (_model.GoUp) _view.GetPlayer().Top -= 5;
            if (_model.GoDown) _view.GetPlayer().Top += 5;

            foreach (var zombie in _model.Zombies.ToArray())
            {
                _model.UpdateZombieMovement(zombie, _view.GetPlayer());

                if (_view.GetPlayer().Bounds.IntersectsWith(zombie.Bounds))
                {
                    _model.PlayerHealth -= 1;
                }
            }

            foreach (var bullet in _model.Bullets.ToArray())
            {
                foreach (var zombie in _model.Zombies.ToArray())
                {
                    if (bullet.Bounds.IntersectsWith(zombie.Bounds))
                    {
                        _model.Score++;
                        _view.RemoveZombieFromUI(zombie);
                        _model.RemoveZombie(zombie);
                        _view.RemoveBulletFromUI(bullet);
                        _model.Bullets.Remove(bullet);
                        break;
                    }
                }
            }

            if (_model.Zombies.Count == 0)
            {
                SpawnZombies(3);
            }


            foreach (var ammo in _model.AmmoItems.ToArray())
            {
                if (_view.GetPlayer().Bounds.IntersectsWith(ammo.Bounds))
                {
                    _model.Ammo += 5;
                    _view.RemoveAmmoFromUI(ammo);
                    _model.RemoveAmmo(ammo);
                }
            }


            if (_model.Ammo <= 0 && _model.AmmoItems.Count == 0)
            {
                DropAmmo();
            }
        }





        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox
            {
                Image = Properties.Resources.ammo_Image,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Left = _model.RandNum.Next(10, 800),
                Top = _model.RandNum.Next(60, 500),
                Tag = "ammo"
            };

            _model.AddAmmo(ammo);
            _view.AddAmmoToUI(ammo);
        }

        private void SpawnZombies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddZombie();
            }
        }

        public void AddZombie()
        {
            PictureBox zombie = new PictureBox
            {
                Tag = "zombie",
                Image = Properties.Resources.zdown,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Left = _model.RandNum.Next(0, 800),
                Top = _model.RandNum.Next(0, 600)
            };

            _model.AddZombie(zombie);
            _view.AddZombieToUI(zombie);
        }

        public void RestartGame()
        {

            foreach (var zombie in _model.Zombies.ToArray())
            {
                _view.RemoveZombieFromUI(zombie);
                _model.RemoveZombie(zombie);
            }


            _model.RestartGame();


            _view.UpdateUI(_model.PlayerHealth, _model.Score, _model.Ammo);


            SpawnZombies(3);


            _model.GameTimer.Start();
        }



        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (_model.GameOver) return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    _model.GoLeft = true;
                    _model.Facing = "left";
                    _view.GetPlayer().Image = Properties.Resources.left;
                    break;
                case Keys.Right:
                    _model.GoRight = true;
                    _model.Facing = "right";
                    _view.GetPlayer().Image = Properties.Resources.right;
                    break;
                case Keys.Up:
                    _model.GoUp = true;
                    _model.Facing = "up";
                    _view.GetPlayer().Image = Properties.Resources.up;
                    break;
                case Keys.Down:
                    _model.GoDown = true;
                    _model.Facing = "down";
                    _view.GetPlayer().Image = Properties.Resources.down;
                    break;
            }
        }



        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: _model.GoLeft = false; break;
                case Keys.Right: _model.GoRight = false; break;
                case Keys.Up: _model.GoUp = false; break;
                case Keys.Down: _model.GoDown = false; break;
                case Keys.Space:
                    if (_model.Ammo > 0 && !_model.GameOver)
                    {
                        _model.Ammo--;
                        ShootBullet();
                    }
                    break;
            }
        }

        private void ShootBullet()
        {
            int bulletX = _view.GetPlayer().Left + (_view.GetPlayer().Width / 2);
            int bulletY = _view.GetPlayer().Top + (_view.GetPlayer().Height / 2);


            string direction = GetBulletDirection();


            Bullet bullet = new Bullet(bulletX, bulletY, direction);


            _model.Bullets.Add(bullet.BulletPictureBox);


            bullet.CreateBullet(_view.GetPlayer().Parent.FindForm());


            _view.AddBulletToUI(bullet.BulletPictureBox);
        }

        private string GetBulletDirection()
        {
            if (_model.Facing == "left") return "left";
            if (_model.Facing == "right") return "right";
            if (_model.Facing == "up") return "up";
            if (_model.Facing == "down") return "down";
            return "right";
        }

    }
}
