using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {
        private int energy = 100;
        public int Energy => energy;

        public void EnergyLow(int n)
        {
            energy -= n;
        }
        public void EnergyHigh(int n)
        {
            energy += n;
            if (energy > 100)
            {
                energy = 100;
            }
        }

        /// <summary>
        /// Изображение для косомо корабля
        /// </summary>
        Image ship;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            ship = Image.FromFile("C:/Users/Николай/source/repos/Asteroids/Asteroids/Resources/protos.png");
        }
        /// <summary>
        /// Метод задающий местоположение коробля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(ship, Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
            //Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            //Pos.X = Pos.X + Dir.X;
            //if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            //Pos.Y = Pos.Y + Dir.Y;
            //if (Pos.X < 0) Dir.X = -Dir.X;
            //if (Pos.X > Game.Width) Dir.X = -Dir.X;
            //if (Pos.Y < 0) Dir.Y = -Dir.Y;
            //if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y * 3;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y * 3;
        }
        public void Backward()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X * 3;
        }
        public void Ahead()
        {
            if (Pos.X < Game.Width) Pos.X = Pos.X + Dir.X * 3;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }
        public static event Message MessageDie;
    }
}
