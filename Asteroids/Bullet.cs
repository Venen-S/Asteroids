using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        /// <summary>
        /// Конструктор Пули
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        /// <summary>
        /// Описание пули и ее положения
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width,
            Size.Height);
        }
        /// <summary>
        /// Скорость пули и поведение
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 20;
            //if (Pos.X > Game.Width) Pos.X = 0;

        }
        /// <summary>
        /// Реген пули
        /// </summary>
        //public void CollEvent()
        //{
        //    //Pos.X = -Game.Width;
        //    //Pos.X = Game.Width;
        //    Pos.X = 0;
        //}
    }
}
