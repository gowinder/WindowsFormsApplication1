﻿// gowinder@hotmail.com
// gowinder.game_base_lib
// login_result.cs
// 2016-05-10-14:11

namespace gowinder.game_base_lib.data
{
    public enum login_result
    {
        login_ok = 0,
        no_account = 1,
        invalid_authorized = 2,
        login_ok_need_load = 3,
        need_remote_check = 4
    }
}