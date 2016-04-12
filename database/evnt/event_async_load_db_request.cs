using gowinder.base_lib;
using gowinder.base_lib.evnt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database.evnt
{
    [Serializable]
    public class async_load_db_request
    {
        public int type { get; set; }
        public uint account_id { get; set; }
        public uint db_index { get; set; }
    }

    [Serializable]
    public abstract class event_async_load_db_request : event_base
    {
        public const String type = "event_async_load_db_request";

        public event_async_load_db_request() : base(type)
        {
            
        }

        public void set(service_base from, service_base to, async_load_db_request request_info)
        {
            set(from, to, type, request_info, null);
        }

        public override void process()
        {
            async_load_db_service ser = to_service as async_load_db_service;
            if (ser == null)
                throw new Exception("event_async_load_db_request to service is not async_load_db_service");

            async_load_db_request info = this.data as async_load_db_request;
            if (info == null)
                throw new Exception("async_load_db_service data is not async_save_db_info");

            i_db db = ser.get_database(info.db_index);
            if (db == null)
                throw new Exception(string.Format("async_load_db_service db_id({0} not found", info.db_index));

            async_load_db_response response = new async_load_db_response();
            response.type = info.type;
            response.account_id = info.account_id;
            response.dict_result = load_need_data(db, info.account_id);
            event_base res = from_service.get_new_event(event_async_load_db_response.type);
            res.set(to_service, from_service, event_async_load_db_response.type, response, null);
            res.send();
        }

        protected abstract Dictionary<string, object> load_need_data(i_db db, uint account_id);
    }
}
