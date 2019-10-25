using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class ArgumentOutOfRangeException : Exception
    {
        public ArgumentOutOfRangeException()
        {
            Console.WriteLine(base.Message);
        }

        public ArgumentOutOfRangeException(string message) : base(message)
        {
        }
    }
}
