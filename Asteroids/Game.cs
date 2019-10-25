using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    class Game
    {
        private static int Score;
        public static Random Rnd = new Random();
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 25 };
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("GAME OVER\n Score: " + Score, new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, Game.Height / 2, Game.Width / 4);
            Buffer.Render();
        }


        /// <summary>
        /// Свойство
        /// </summary>
        private static BufferedGraphicsContext _context;
        /// <summary>
        /// Свойство
        /// </summary>
        public static BufferedGraphics Buffer;
        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public static int Width { get; set; }
        /// <summary>
        /// Высота поля
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Коснтруктор класса Game
        /// </summary>
        static Game()
        {

        }

        /// <summary>
        /// Метод для вывода графики
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            if (Width > 2000 || Width < 0)
            {
                throw new ArgumentOutOfRangeException("Превышена ширина экрана");
            }
            Height = form.ClientSize.Height;
            if (Height > 1500 || Height < 0)
            {
                throw new ArgumentOutOfRangeException("Превышена высота экрана");
            }
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            //Timer timer = new Timer { Interval = 25 };
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) bullet.Add(new Bullet(new Point(ship.Rect.X
                + 15, ship.Rect.Y + 19), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.W) ship.Up();
            if (e.KeyCode == Keys.S) ship.Down();
            if (e.KeyCode == Keys.A) ship.Backward();
            if (e.KeyCode == Keys.D) ship.Ahead();
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Очистка буфера и рендер
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(10, 10, 20, 20));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();
            //Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            foreach (Asteroid obj in asteroids)
            {
                obj?.Draw();
            }
            foreach (Bullet b in bullet) b.Draw();
            ship.Draw();
            medKit.Draw();
            Buffer.Graphics.DrawString("HP: " + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Score: " + Score, SystemFonts.DefaultFont, Brushes.White, 0, 15);
            Buffer.Render();
        }

        /// <summary>
        /// Обработка событий + обработка
        /// </summary>
        public static void Update()
        {
            int x;
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }
            foreach (Bullet b in bullet) b.Update();
            medKit.Update();
            for (int i = 0; i < asteroids.Count; i++)
            {
                x = 0;
                try
                {
                    if (asteroids[i] == null) continue;
                    asteroids[i].Update();

                    for (int j = 0; j < bullet.Count; j++)
                    {
                        if (asteroids[i] != null && bullet[j].Collision(asteroids[i]))
                        {
                            System.Media.SystemSounds.Hand.Play();
                            asteroids[i] = null;
                            bullet.RemoveAt(j);
                            j--;
                            Score += 10;
                            asteroids.RemoveAt(i);
                        }
                        if (ship.Collision(medKit))
                        {
                            medKit.CollEventMEd();
                            ship.EnergyHigh(15);
                            System.Media.SystemSounds.Beep.Play();
                            //Score += 5;
                        }
                        //if (asteroids.Count == 0) Reload(asteroids);
                        foreach (var e in asteroids)
                        {
                            if (asteroids[i] == null) x++;
                        }
                        if (x == asteroids.Count) Reload(asteroids);


                    }
                    if (asteroids[i] == null || !ship.Collision(asteroids[i])) continue;
                    ship.EnergyLow(Rnd.Next(1, 10));
                    System.Media.SystemSounds.Asterisk.Play();
                    if (ship.Energy <= 0) ship.Die();

                }
                catch
                {

                }
            }





            //foreach (Asteroid a in asteroids)
            //{
            //    if (a == null) continue;
            //    a.Update();
            //    if(a.Collision(bullet))//Обработка столкновения с пулей
            //    {
            //        System.Media.SystemSounds.Hand.Play();
            //        bullet.CollEvent();//регеним пулю
            //        a.CollEvent();//регеним астероид
            //        Score += 10;
            //    }
            //    if (bullet != null && bullet.Collision(a))
            //    {
            //        System.Media.SystemSounds.Hand.Play();
            //        asteroids = null;
            //        bullet = null;
            //        continue;
            //    }
            //    if(ship.Collision(medKit))
            //    {
            //        medKit.CollEventMEd();
            //        ship.EnergyHigh(15);
            //        System.Media.SystemSounds.Beep.Play();
            //        Score += 5;
            //    }
            //    if (!ship.Collision(a)) continue;
            //    ship?.EnergyLow(3);
            //    System.Media.SystemSounds.Asterisk.Play();
            //    if (ship.Energy <= 0) ship?.Die();
            //}
        }

        private static void Reload(List<Asteroid> asteroids)
        {
            x++;
            Random rnd = new Random();
            int r;

            for (int i = 0; i < x; i++)
            {
                r = rnd.Next(30, 50);
                asteroids.Add(new Asteroid(new Point(rnd.Next(Game.Height+Game.Height/2,Game.Height*2), rnd.Next(0, Game.Height-75)),
                    new Point(-r / 5, r), new Size(r, r)));
            }

        }
        public static int x = 2;
        public static MedKit medKit;
        private static Ship ship;
        public static BaseObject[] _objs;
        //private static Bullet bullet;
        private static List<Bullet> bullet = new List<Bullet>();
        public static List<Asteroid> asteroids = new List<Asteroid>();
        //public static Asteroid[] asteroids;
        //Чем меньше значение массива тем быстрее мерцание и меньше обьектов
        /// <summary>
        /// Метод инициализации объектов
        /// </summary>
        public static void Load()
        {
            Random rnd = new Random();
            _objs = new BaseObject[50];
            ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            //bullet = new Bullet(new Point(ship.Rect.X + 40, ship.Rect.Y + 20), new Point(4, 0), new Size(4, 1));
            medKit = new MedKit(new Point(rnd.Next(0, Game.Height), rnd.Next(0, Game.Height-75)),
                new Point(-rnd.Next(30, 50) / 5, 30), new Size(25, 25));
            int a = Game.Height;
            int b = 4;
            int c = 0;
            int r;
            for (var i = 0; i < _objs.Length; i++)
            {
                r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(rnd.Next(c, a), rnd.Next(c, a)), new
                Point(-r, r), new Size(b, b));
                if (r > 51) throw new GameObjectException("Слишком большая скорость");
                if (r < -50) throw new GameObjectException("Слишком низкая скорость");
                if (a > Game.Height || a < 0 || b > 4 || b < 0 || c < 0 || c > a)
                    throw new GameObjectException("Один из параметров не верный");
            }

            for (var i = 0; i < 2; i++)
            {
                r = rnd.Next(30, 50);
                //asteroids[i] = new Asteroid(new Point(rnd.Next(c, a), rnd.Next(c, a)),
                //new Point(-r /5, r), new Size(r, r));
                asteroids.Add(new Asteroid(new Point(rnd.Next(c, a), rnd.Next(c, a)),
                new Point(-r / 5, r), new Size(r, r)));
                if (r > 51) throw new GameObjectException();
                if (r < -50) throw new GameObjectException();
                if (a > Game.Height || a < 0 || c < 0 || c > a)
                    throw new GameObjectException();
            }
            //for (int i = 0; i < _objs.Length / 4; i++)
            //    _objs[i] = new BaseObject(new Point(rnd.Next(1, 600), i * rnd.Next(1, 20)), new Point(-i, -i), new
            //    Size(10, 10));
            //for (int i = _objs.Length / 4; i < _objs.Length; i++)
            //    _objs[i] = new Star(new Point(rnd.Next(1, 600), i * rnd.Next(1, 25)), new Point(-i, 0), new Size(5,
            //    5));
            //for (int i = _objs.Length / 4 + _objs.Length / 4; i < _objs.Length; i++)
            //    _objs[i] = new Planet(new Point(600,i*20), new Point(-i, 0), new Size(5,
            //    5));
            //for (int i = _objs.Length / 4 + _objs.Length / 2; i < _objs.Length; i++)
            //    _objs[i] = new Ship(new Point(600, i * 20), new Point(-i, 0), new Size(5,
            //    5));
        }
    }
}
