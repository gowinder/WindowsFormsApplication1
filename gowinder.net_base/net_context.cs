﻿// gowinder@hotmail.com
// gowinder.net_base
// net_context.cs
// 2016-05-10-14:11

namespace gowinder.net_base
{
    public class net_context
    {
        public net_context(uint i)
        {
            id = i;
            done = false;
        }

        public uint id { get; set; }

        public bool done { get; set; }
    }
}