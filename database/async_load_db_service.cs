// gowinder@hotmail.com
// gowinder.database
// async_load_db_service.cs
// 2016-05-10-14:11

namespace gowinder.database
{
    public abstract class async_load_db_service : db_service_base
    {
        public static string default_name = "async_load_db_service";

        public async_load_db_service(string service_name = "")
        {
            if (service_name == "")
                name = default_name;
            else
            {
                name = service_name;
            }
        }
    }
}