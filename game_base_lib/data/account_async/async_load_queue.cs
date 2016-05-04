// gowinder@hotmail.com
// gowinder.game_base_lib
// async_load_queue.cs
// 2016-05-04-9:34

#region

using System;
using System.Collections.Generic;

#endregion

namespace gowinder.game_base_lib.data.account_async
{
    public class async_load_queue
    {
        public async_load_queue(account_manager acc_mana)
        {
            acc_manager = acc_mana;

            dict_wait_holder = new Dictionary<uint, async_load_account_wait_holder>();
        }

        protected account_manager acc_manager { get; set; }

        public Dictionary<uint, async_load_account_wait_holder> dict_wait_holder { get; protected set; }


        public void resume_process_wait_holder(uint account_id)
        {
            var wait_holder = dict_wait_holder[account_id];
            if (wait_holder == null)
                throw new Exception(
                    "async_load_queue.resume_process_wait_holder wait_holder not found in async_load_queue");

            foreach (var wait_action in wait_holder.list_wait_action)
            {
                try
                {
                    wait_action.resume_process();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}