using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.Utils
{
    public class OnlineMusicException : Exception
    {
        public OnlineMusicException()
        {
        }

        public OnlineMusicException(string message)
            : base(message)
        {
        }

        public OnlineMusicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
