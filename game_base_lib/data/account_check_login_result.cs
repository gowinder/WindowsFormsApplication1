// gowinder@hotmail.com
// gowinder.game_base_lib
// account_check_login_result.cs
// 2016-05-04-9:34

namespace gowinder.game_base_lib.data
{
    public enum account_check_login_result
    {
        login_ok = 0,
        invalid_pwd = 1,
        token_timeout = 2,
        need_remote_check = 3
    }
}