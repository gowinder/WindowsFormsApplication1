using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.game_base_lib.account;

namespace WindowsFormsApplication1.data
{
    class data_account : data_default_account
    {
        public List<data_item> list_item { get; set; }
        public List<data_fort> list_fort { get; set; }
    }
}
