﻿// gowinder@hotmail.com
// gowinder.base_lib
// event_stop_service.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib.service;

#endregion

namespace gowinder.base_lib.evnt
{
    [Serializable]
    public class event_stop_service : event_base
    {
        public const string type = "stop_service";

        public event_stop_service() : base(type)
        {
        }

        public void set(service_base from, service_base to)
        {
            set(from, to, type, null, null);
        }

        public override void process()
        {
            to_service.stop_service();
        }
    }
}