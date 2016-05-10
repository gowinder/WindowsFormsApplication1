// gowinder@hotmail.com
// gowinder.base_lib
// base_event_builder.cs
// 2016-05-10-14:11

#region

#endregion

namespace gowinder.base_lib.evnt
{
    /// <summary>
    ///     基本事件构造器
    /// </summary>
    public class base_event_builder : i_event_builder
    {
        public virtual event_base build_event(string event_type)
        {
            switch (event_type)
            {
                case event_stop_service.type:
                {
                    return new event_stop_service();
                }
                default:
                    return null;
            }
        }
    }
}