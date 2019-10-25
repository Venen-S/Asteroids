using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class MedKit : BaseObject
    {
        Image medkit;
        public MedKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            medkit = Image.FromFile("C:/Users/Николай/source/repos/Asteroids/Asteroids/Resources/medkit.png");
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(medkit, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
        public void CollEventMEd()
        {
            Pos.X = 0;
        }
    }
}
