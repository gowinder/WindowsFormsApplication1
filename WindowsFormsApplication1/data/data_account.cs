// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// data_account.cs
// 2016-05-10-14:11

#region

using System.Collections.Generic;
using gowinder.game_base_lib.data;

#endregion

namespace WindowsFormsApplication1.data
{
    internal class data_account : data_default_account
    {
        public List<data_item> list_item { get; set; }
        public List<data_fort> list_fort { get; set; }

        public override void clear_full_load()
        {
            base.clear_full_load();
            list_item = null;
            list_fort = null;
        }
    }
}