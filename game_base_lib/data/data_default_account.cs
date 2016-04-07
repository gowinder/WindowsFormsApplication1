using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.game_base_lib.account
{
    public class data_default_account
    {
        public uint id { get; set; }
        public uint platform_id { get; set; }
        public string platform_user_id { get; set; }
        public string full_name { get; protected set; }

        public void update_full_name()
        {
            full_name = get_full_name(platform_id, platform_user_id);
        }

        public static string get_full_name(uint platform_id, string platform_user_id)
        {
            return string.Format("_@_{0}_@_{1}", platform_id, platform_user_id);
        }
    }
}
