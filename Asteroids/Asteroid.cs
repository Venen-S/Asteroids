using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid : BaseObject, ICloneable, IComparable
    {
        /// <summary>
        /// Изображение для астероидов
        /// </summary>
        Image Aster;
        private bool flag = true;
        public bool Flag => flag;

        public int Power { get; set; } = 3;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
            Aster = Image.FromFile("C:/Users/Николай/source/repos/Asteroids/Asteroids/Resources/Юпитер.png");
        }
        /// <summary>
        /// Местоположение обьектов
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Aster, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Клонирование астероидов
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            asteroid.Power = Power;
            return asteroid;
        }

        /// <summary>
        /// Реген астероидов
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            //Pos.Y = Pos.Y + Dir.Y;
            //if (Pos.X < 0) Dir.X = -Dir.X;
            //if (Pos.X > Game.Width) Dir.X = -Dir.X;
            //if (Pos.Y < 0) Dir.Y = -Dir.Y;
            //if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        /// <summary>
        /// Реген астероидов
        /// </summary>
        public void CollEvent()
        {
            Pos.X = Game.Width + Size.Width;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Asteroid temp)
            {
                if (Power > temp.Power)
                    return 1;
                if (Power < temp.Power)
                    return -1;
                else return 0;
            }
            throw new ArgumentException("Parametr is not a Asteroid");
        }
    }
}
