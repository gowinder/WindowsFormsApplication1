using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.game_base_lib.data;

namespace WindowsFormsApplication1.data
{
    class data_account : data_default_account
    {
        public data_account() : base()
        {
            
        }

        public override void clear_full_load()
        {
            base.clear_full_load();
            list_item = null;
            list_fort = null;
        }

        public List<data_item> list_item { get; set; }
        public List<data_fort> list_fort { get; set; }


    }
}
