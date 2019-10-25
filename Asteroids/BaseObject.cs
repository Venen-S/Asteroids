using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    abstract class BaseObject : ICollision
    {
        //Image Aster = Image.FromFile("C:/Users/Николай/source/repos/MyGame/MyGame/Resources/asteroid.png");
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        //{
        //Game.Buffer.Graphics.DrawImage(Aster, Pos.X,Pos.Y, Size.Width, Size.Height);
        //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y,
        //Size.Width, Size.Height);
        //}
        public abstract void Update();
        //{
        //Pos.X = Pos.X + Dir.X;
        //if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        //Pos.Y = Pos.Y + Dir.Y;
        //if (Pos.X < 0) Dir.X = -Dir.X;
        //if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        //}
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

        //public abstract void CollEvent();
        public delegate void Message();
    }
}
