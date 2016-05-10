// gowinder@hotmail.com
// gowinder.base_lib
// utility.cs
// 2016-05-10-14:11

#region

using System;

#endregion

namespace gowinder.server_lib
{
    public class utility
    {
        public static long get_tick()
        {
            return DateTime.Now.Ticks/10000000 - 8*60*60;
        }
    }
}