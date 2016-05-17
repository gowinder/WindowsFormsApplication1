// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// data_item.cs
// 2016-05-10-14:11

#region

using System.Collections;
using MySql.Data.MySqlClient;

#endregion

namespace gowinder.game_logic_lib.data
{
    public class data_item
    {
        public data_item()
        {
            change_set = new BitArray(4);
        }

        protected BitArray change_set { get; private set; }
        public uint id { get; set; }
        public uint account_id { get; set; }
        public uint item_type { get; set; }
        public uint item_count { get; set; }

        public void read_from_dataset(MySqlDataReader reader)
        {
            id = (uint) reader[0];
            account_id = (uint) reader[1];
            item_type = (uint) reader[2];
            item_count = (uint) reader[3];
        }
    }
}