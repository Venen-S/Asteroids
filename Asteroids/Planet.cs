using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Planet : BaseObject
    {
        /// <summary>
        /// Изображение на заднем фоне
        /// </summary>
        Image Son;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Son = Image.FromFile("C:/Users/Николай/source/repos/Asteroids/Asteroids/Resources/Son.png");
        }
        /// <summary>
        /// Метод задающий местоположение изображений для заднего фона
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Son, new Rectangle(Game.Width - 400, 100, 155, 100));
            //Game.Buffer.Graphics.FillEllipse(Brushes.Cyan, new Rectangle(650, 70, 100, 100));
            //Game.Buffer.Graphics.FillEllipse(Brushes.Red, new Rectangle(850, 300, 20, 20));

            //Game.Buffer.Graphics.FillEllipse(Brushes.Blue, new Rectangle(800, 150, 50, 50));
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
