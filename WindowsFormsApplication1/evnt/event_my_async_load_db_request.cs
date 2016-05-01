using gowinder.database.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.database;
using System.Collections;
using MySql.Data.MySqlClient;
using WindowsFormsApplication1.data;

namespace WindowsFormsApplication1.evnt
{
    [Serializable]
    public class event_my_async_load_db_request : event_async_load_db_request
    {
        protected override Dictionary<string, object> load_need_data(i_db db, uint account_id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string str_sql = string.Format("select * from game_item where account_id={0}", account_id);
            MySqlDataReader reader = db.create_reader(str_sql);
            List<data_item> list_item = new List<data_item>();
            while(reader.Read())
            {
                data_item item = new data_item();
                item.read_from_dataset(reader);
                list_item.Add(item);
            }
            dict.Add("list_item", list_item);

            return dict;
        }
    }
}
