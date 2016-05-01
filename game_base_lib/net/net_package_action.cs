using gowinder.net_base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.game_base_lib;
using gowinder.game_base_lib.data;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1.net
{
    public enum net_package_action_sub_type
    {
        login = 1,
        create_role,
        battle,
    }


    public class net_package_action : net_package
    {
        public net_package_action(service_base from_service, service_base process_service = null) : base(from_service)
        {
            this.process_service = process_service;
        }

        public override void process()
        {
            logic_service logic_ser = process_service as logic_service;
            if (logic_ser == null)
                throw new Exception("net_package_action.process process_service is not my_logic_serivce");
            JObject json_root = data as JObject;
            net_package_action_sub_type st = (net_package_action_sub_type)sub_type;
            switch(st)
            {
                case net_package_action_sub_type.login:
                    {
                        string user_name = (string)json_root[net_json_name.user_name];
                        string user_pwd = (string)json_root[net_json_name.user_pwd];
                        uint platform_id = (uint)json_root[net_json_name.platform_id];
                        login_result login_ret = logic_ser.account_manager.do_login(this, platform_id, user_name, user_pwd);

                        if (login_ret == login_result.login_ok_need_load)
                        {
                            break;
                        }
                        else
                        {
                                    
                        }
                    }
                    break;
                case net_package_action_sub_type.create_role:
                    {
                        
                    }
                    break;
                case net_package_action_sub_type.battle:
                    {

                    }
                    break;
            }
        }
    }
}
