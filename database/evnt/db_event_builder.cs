// gowinder@hotmail.com
// gowinder.database
// db_event_builder.cs
// 2016-05-10-14:11

#region

using gowinder.base_lib.evnt;

#endregion

namespace gowinder.database.evnt
{
    public class db_event_builder : base_event_builder
    {
        public override event_base build_event(string event_type)
        {
            var e = base.build_event(event_type);
            if (e != null)
                return e;

            switch (event_type)
            {
                case event_async_save_db.type:
                {
                    return new event_async_save_db();
                }
                default:
                    return null;
            }
        }
    }
}