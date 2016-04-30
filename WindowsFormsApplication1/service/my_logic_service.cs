using gowinder.base_lib;
using gowinder.game_base_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.data;
using gowinder.game_base_lib.data;
using gowinder.http_service_lib.evnt;
using gowinder.net_base;

namespace WindowsFormsApplication1.service
{
    public class my_logic_service : logic_service
    {
        protected override void build_user_manager()
        {
            account_manager = new my_account_manager(this);
        }

        public override void receive_package(event_receive_package package)
        {
            receive_package_info info = package.data as receive_package_info;
            if(info == null)
                throw new Exception("my_logic_service.receive_package package.data is not receive_package_info");
            if(!info.package.is_parsed)
                throw new Exception("my_logic_service.receive_package package is not parsed");

            switch (info.package.type)
            {
                case 1:
                {
                    process_action_package(info.package);
                }
                    break;
            }
        }

        private void process_action_package(net_package package)
        {
            account_check_login_result result = account_manager.check_login(package);
            if (result == account_check_login_result.need_remote_check)
            {
                
            }
            else if(result == account_check_login_result.login_ok)
            {
                
            }
            else
            {
                
            }
        }
    }
}
