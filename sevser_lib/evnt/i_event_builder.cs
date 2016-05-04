// gowinder@hotmail.com
// gowinder.base_lib
// i_event_builder.cs
// 2016-05-04-9:34

#region



#endregion

namespace gowinder.base_lib.evnt
{
    public interface i_event_builder
    {
        event_base build_event(string event_type);
    }
}