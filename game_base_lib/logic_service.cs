using System;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.database;
using gowinder.game_base_lib.data;
using gowinder.game_base_lib.evnt;
using gowinder.http_service_lib.evnt;
using gowinder.net_base;
using gowinder.net_base.evnt;

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
            
            build_user_manager();
        }
        protected override i_event_builder on_create_event_builder()
        {
            return new logic_service_event_builder() as i_event_builder; ;
        }
        protected virtual void build_user_manager()
        {
            this.account_manager = new account_manager(this);
        }

        public account_manager account_manager { get; protected set; }

        private void process_action_package(net_package package)
        {
            account_check_login_result result = account_manager.check_login(package);
            if (result == account_check_login_result.need_remote_check)
            {

            }
            else if (result == account_check_login_result.login_ok)
            {

            }
            else
            {

            }
        }

        public virtual void receive_package(event_receive_package package)
        {
            receive_package_info info = package.data as receive_package_info;
            if (info == null)
                throw new Exception("my_logic_service.receive_package package.data is not receive_package_info");
            if (!info.package.is_parsed)
                throw new Exception("my_logic_service.receive_package package is not parsed");

            switch ((net_package_type)info.package.type)
            {
                case net_package_type.action:
                    {
                        process_action_package(info.package);
                    }
                    break;
            }
        }

        protected override void on_process_start()
        {
            account_manager.init();
        }
    }
}