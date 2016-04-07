using gowinder.base_lib;
using gowinder.base_lib.evnt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database.evnt
{
    class async_save_db_info
    {
        public uint db_index { get; set; }
        public string sql { get; set; }
    }

    class event_async_save_db : event_base
    {
        public const String type = "event_async_save_db";

        public void set(service_base from, service_base to, string str_sql, uint db_id)
        {
            async_save_db_info info = new async_save_db_info() { db_index = db_id, sql = str_sql };
            set(from, to, type, info, null);
        }

        public override void process()
        {
            async_save_db_service ser = to_service as async_save_db_service;
            if (ser == null)
                throw new Exception("event_async_save_db to service is not async_save_db_service");

            async_save_db_info info = this.data as async_save_db_info;
            if(info == null)
                throw new Exception("event_async_save_db data is not async_save_db_info");

            i_database db = ser.get_database(info.db_index);
            if (db == null)
                throw new Exception(string.Format("event_async_save_db db_id({0} not found", info.db_index));

            if (db.execute_no_query(info.sql) < 1)
                throw new Exception(string.Format("event_async_save_db db_id{0} failed, sql:{1}", info.db_index, info.sql));

        }
    }
}
