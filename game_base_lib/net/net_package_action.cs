// gowinder@hotmail.com
// gowinder.game_base_lib
// net_package_action.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.game_base_lib;
using gowinder.game_base_lib.data;
using gowinder.net_base;
using Newtonsoft.Json.Linq;

#endregion

namespace WindowsFormsApplication1.net
{
    public enum net_package_action_sub_type
    {
        login = 1,
        create_role,
        battle
    }


    public class net_package_action : net_package
    {
        public net_package_action(service_base from_service, service_base process_service = null) : base(from_service)
        {
            this.process_service = process_service;
        }

        public override void process()
        {
            var this_ret = 0;
            var logic_ser = process_service as logic_service;
            if (logic_ser == null)
                throw new Exception("net_package_action.process process_service is not my_logic_serivce");
            var json_root = data as JObject;
            var st = (net_package_action_sub_type) sub_type;
            switch (st)
            {
                case net_package_action_sub_type.login:
                {
                    var user_name = (string) json_root[net_json_name.user_name];
                    var user_pwd = (string) json_root[net_json_name.user_pwd];
                    var platform_id = (uint) json_root[net_json_name.platform_id];
                    var login_ret = logic_ser.account_manager.do_login(this, platform_id, user_name, user_pwd);
                    this_ret = (int) login_ret;
                    if (login_ret == login_result.login_ok_need_load)
                    {
                        return;
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

            ret = this_ret;
            send_back(process_service);
//             var event_send = from_service.get_new_event(event_send_package.type) as event_send_package;
//             if (event_send == null)
//                 throw new Exception("net_package_action.process get new event_send_package failed");
// 
//             receive_package_info recv_info = this.owner as receive_package_info;
// 
//             send_package_info send_info = new send_package_info() {context = recv_info.context, package = this};
//             this.owner = send_info;
//             event_send.set(process_service, from_service, send_info);
//             event_send.send();
        }
    }
}