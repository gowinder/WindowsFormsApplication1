// gowinder@hotmail.com
// gowinder.game_base_lib
// async_load_account_wait_holder.cs
// 2016-05-04-9:34

#region

using System.Collections.Generic;
using gowinder.base_lib;

#endregion

namespace gowinder.game_base_lib.data.account_async
{
    public class async_load_account_wait_holder
    {
        public async_load_account_wait_holder(uint acc_id)
        {
            account_id = acc_id;
            list_wait_action = new List<i_async_wait_action>();
        }

        public List<i_async_wait_action> list_wait_action { get; protected set; }
        public uint account_id { get; protected set; }
    }
}