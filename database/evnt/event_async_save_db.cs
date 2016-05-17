// gowinder@hotmail.com
// gowinder.database
// event_async_save_db.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.base_lib.service;

#endregion

namespace gowinder.database.evnt
{
    public class async_save_db_info
    {
        public uint db_index { get; set; }
        public string sql { get; set; }
    }
    
    public class event_async_save_db : event_base
    {
        public const string type = "event_async_save_db";

        public event_async_save_db() : base(type)
        {
        }

        public void set(service_base from, service_base to, async_save_db_info info)
        {
            set(from, to, type, info, null);
        }

        public override void process()
        {
            var ser = to_service as async_save_db_service;
            if (ser == null)
                throw new Exception("event_async_save_db to service is not async_save_db_service");

            var info = data as async_save_db_info;
            if (info == null)
                throw new Exception("event_async_save_db data is not async_save_db_info");

            var db = ser.get_database(info.db_index);
            if (db == null)
                throw new Exception($"event_async_save_db db_id({info.db_index} not found");

            if (db.execute_no_query(info.sql) < 1)
                throw new Exception($"event_async_save_db db_id{info.db_index} failed, sql:{info.sql}");
        }
    }
}