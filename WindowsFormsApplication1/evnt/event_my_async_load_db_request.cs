// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// event_my_async_load_db_request.cs
// 2016-05-04-9:34

#region

using System;
using System.Collections.Generic;
using WindowsFormsApplication1.data;
using gowinder.database;
using gowinder.database.evnt;

#endregion

namespace WindowsFormsApplication1.evnt
{
    [Serializable]
    public class event_my_async_load_db_request : event_async_load_db_request
    {
        protected override Dictionary<string, object> load_need_data(i_db db, uint account_id)
        {
            var dict = new Dictionary<string, object>();
            var str_sql = string.Format("select * from game_item where account_id={0}", account_id);
            var reader = db.create_reader(str_sql);
            var list_item = new List<data_item>();
            while (reader.Read())
            {
                var item = new data_item();
                item.read_from_dataset(reader);
                list_item.Add(item);
            }
            dict.Add("list_item", list_item);

            return dict;
        }
    }
}