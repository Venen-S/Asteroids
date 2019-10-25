using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    interface ICollision
    {
        /// <summary>
        /// Интерфейс для обработки событий
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
