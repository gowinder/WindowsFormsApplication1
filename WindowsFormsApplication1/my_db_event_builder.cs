// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// my_db_event_builder.cs
// 2016-05-10-14:11

#region

using WindowsFormsApplication1.evnt;
using gowinder.base_lib.evnt;
using gowinder.database.evnt;

#endregion

namespace WindowsFormsApplication1
{
    internal class my_db_event_builder : db_event_builder
    {
        public override event_base build_event(string event_type)
        {
            var e = base.build_event(event_type);
            if (e != null)
                return e;

            switch (event_type)
            {
                case event_async_load_db_request.type:
                {
                    return new event_my_async_load_db_request();
                }
                default:
                    return null;
            }
        }
    }
}