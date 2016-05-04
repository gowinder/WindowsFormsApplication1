// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// utility.cs
// 2016-05-04-9:34

#region

using System;

#endregion

namespace WindowsFormsApplication1
{
    internal class utility
    {
        public static long get_tick()
        {
            return DateTime.Now.Ticks/10000000 - 8*60*60;
        }
    }
}