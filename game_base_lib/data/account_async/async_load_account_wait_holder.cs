using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib;

namespace gowinder.game_base_lib.data.account_async
{
    public class async_load_account_wait_holder
    {
        public List<i_async_wait_action> list_wait_action { get; protected set; }
        public uint account_id { get; protected set; }
        public async_load_account_wait_holder(uint acc_id)
        {
            this.account_id = acc_id;
            list_wait_action = new List<i_async_wait_action>();
        }

    }
}
