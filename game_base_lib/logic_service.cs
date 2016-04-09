using System;
using gowinder.base_lib;
using gowinder.database;
using gowinder.game_base_lib.data;
using gowinder.http_service_lib.evnt;

namespace gowinder.game_base_lib
{
    public class logic_service : db_service_base
    {
        public logic_service()
        {
            name = "logic_serivce";

            build_user_manager();
        }

        protected virtual void build_user_manager()
        {
            this.user_manager = new user_manager(this);
        }

        public user_manager user_manager { get; protected set; }



        public void receive_msg(event_receive_msg msg)
        {
            receive_msg_info msg_info = msg.data as receive_msg_info;
            
        }
    }
}