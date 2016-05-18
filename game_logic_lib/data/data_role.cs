using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.game_base_lib.data;

namespace gowinder.game_logic_lib.data
{
    public class data_role : game_data_basic
    {
        public const string tname = "game_role";
        public data_role(service_base owner_serivce):base(owner_serivce)
        {
            table_name = tname;
        }

        protected override void init_fields()
        {
            fields_name.Add("id");
            fields.Add(0);

            fields_name.Add("account_id");
            fields.Add(0);

            fields_name.Add("name");
            fields.Add("");
        }
    }
}
