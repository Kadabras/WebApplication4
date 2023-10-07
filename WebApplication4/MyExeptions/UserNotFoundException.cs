using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.MyExceptions
{
    public class UserNotFoundException : Exception
    {
        public override string Message 
            => "User no found";
    }
}
