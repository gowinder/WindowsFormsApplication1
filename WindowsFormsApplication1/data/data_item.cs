using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.data
{
    public class data_item
    {
        protected BitArray change_set { get; private set; }
        public uint id { get; set; }
        public uint account_id { get; set; }
        public uint item_type { get; set; }
        public uint item_count { get; set; }

        public data_item()
        {
            change_set = new BitArray(4);
        }

        public void read_from_dataset(MySqlDataReader reader)
        {
            id = (uint)reader[0];
            account_id = (uint)reader[1];
            item_type = (uint)reader[2];
            item_count = (uint)reader[3];
        }
    }
}
