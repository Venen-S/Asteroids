using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class GameObjectException : Exception
    {
        public GameObjectException()
        {
            Console.WriteLine(base.Message);
        }

        public GameObjectException(string message) : base(message)
        {
        }
    }
}
