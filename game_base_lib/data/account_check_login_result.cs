using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.game_base_lib.data
{
    public enum account_check_login_result
    {
        login_ok = 0,
        invalid_pwd = 1,
        token_timeout = 2,
        need_remote_check = 3,
        
    }
}
