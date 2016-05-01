using System;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.database;
using gowinder.game_base_lib.data;
using gowinder.game_base_lib.evnt;
using gowinder.http_service_lib.evnt;

namespace gowinder.game_base_lib
{
    public class logic_service : db_service_base
    {
        public class logic_service_event_builder : base_event_builder 
        {
            public override event_base build_event(string event_type)
            {
                event_base e = base.build_event(event_type);
                if (e != null)
                    return e;

                switch (event_type)
                {
                    case event_logic_receive_package.type:
                        return new event_logic_receive_package();
                }

                return null;
            }
        }

        public logic_service()
        {
            name = "logic_serivce";

            event_builder = new logic_service_event_builder();

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

        protected override void on_process_start()
        {
            account_manager.init();
        }
    }
}