﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.base_lib.evnt
{
    public interface i_event_builder
    {
        event_base build_event(String event_type);
    }
}
