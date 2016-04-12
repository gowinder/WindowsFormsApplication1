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
            this.account_manager = new account_manager(this);
        }

        public account_manager account_manager { get; protected set; }



        public virtual void receive_package(event_receive_package package)
        {
            receive_package_info package_info = package.data as receive_package_info;
            
        }
    }
}