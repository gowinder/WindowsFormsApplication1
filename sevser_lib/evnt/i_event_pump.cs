using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.sevser_lib.evnt
{
    interface i_event_pump
    {
        void push(event_base e);
        event_base pop();
        int size();
        bool wait(int mill_second);
        void close();
        void open();
        bool is_open();
        void recycle(event_base e);
        event_base get_new_event(String type);
    }
}
