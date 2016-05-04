// gowinder@hotmail.com
// gowinder.database
// event_async_save_db.cs
// 2016-05-04-9:34

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.database.evnt
{
    [Serializable]
    internal class async_save_db_info
    {
        public uint db_index { get; set; }
        public string sql { get; set; }
    }

    [Serializable]
    internal class event_async_save_db : event_base
    {
        public const string type = "event_async_save_db";

        public event_async_save_db() : base(type)
        {
        }

        public void set(service_base from, service_base to, string str_sql, uint db_id)
        {
            var info = new async_save_db_info {db_index = db_id, sql = str_sql};
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
                throw new Exception(string.Format("event_async_save_db db_id({0} not found", info.db_index));

            if (db.execute_no_query(info.sql) < 1)
                throw new Exception(string.Format("event_async_save_db db_id{0} failed, sql:{1}", info.db_index,
                    info.sql));
        }
    }
}