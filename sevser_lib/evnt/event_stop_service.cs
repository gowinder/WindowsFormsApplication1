﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.sevser_lib.evnt
{
    class event_stop_service : event_base
{
    public const String type = "stop_service";

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
