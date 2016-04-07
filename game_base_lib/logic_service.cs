using System;
using gowinder.base_lib;
using gowinder.game_base_lib.data;

namespace gowinder.game_base_lib
{
    public class logic_service : service_base
    {
        public logic_service()
        {
            name = "logic_serivce";

            build_user_manager();
        }

        protected virtual build_user_manager()
        {
            this.user_manager = new user_manager(this);
        }

        public user_manager user_manager { get; protected set; }
    }
}