// gowinder@hotmail.com
// gowinder.database
// async_save_db_service.cs
// 2016-05-10-14:11

using gowinder.base_lib.evnt;
using gowinder.database.evnt;

namespace gowinder.database
{
    public class async_save_db_service : db_service_base
    {
        public class async_save_db_service_event_builder : base_event_builder
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
                        break;
                }

                return null;
            }
        }

        public static string default_name = "async_save_db_service";
        public async_save_db_service(string service_name = "")
        {
            name = service_name == "" ? default_name : name;
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new async_save_db_service_event_builder();
        }
        protected static async_save_db_service s_instance;

        public static async_save_db_service instance => s_instance ?? (s_instance = new async_save_db_service());
    }
}