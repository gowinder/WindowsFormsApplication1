// gowinder@hotmail.com
// gowinder.base_lib
// i_event_pump.cs
// 2016-05-10-14:11

#region

#endregion

namespace gowinder.base_lib.evnt
{
    public interface i_event_pump
    {
        void push(event_base e);
        event_base pop();
        int size();
        bool wait(int mill_second);
        void close();
        void open();
        bool is_open();
        void recycle(event_base e);
        event_base get_new_event(string type);
    }
}