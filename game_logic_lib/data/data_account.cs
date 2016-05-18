// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// data_account.cs
// 2016-05-10-14:11

#region

using System.Collections.Generic;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.game_base_lib.data;

#endregion

namespace gowinder.game_logic_lib.data
{
    public class data_account : data_default_account
    {
        public data_account(service_base owner_service) : base(owner_service)
        {
            
        }

        public List<data_item> list_item { get; set; }
        public List<data_fort> list_fort { get; set; }
        public data_role role { get; set; }

        public override void clear_full_load()
        {
            base.clear_full_load();
            list_item = null;
            list_fort = null;
        }
    }
}