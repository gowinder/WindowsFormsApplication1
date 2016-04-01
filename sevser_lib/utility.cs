using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.server_lib
{
    class utility
    {
        public static long get_tick()
        {
            return (DateTime.Now.Ticks) / 10000000 - 8 * 60 * 60;            
        }
    }
}
