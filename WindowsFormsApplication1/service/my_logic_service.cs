// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// my_logic_service.cs
// 2016-05-10-14:11

#region

using WindowsFormsApplication1.data;
using WindowsFormsApplication1.evnt;
using gowinder.base_lib.evnt;
using gowinder.database.evnt;
using gowinder.game_base_lib;

#endregion

namespace WindowsFormsApplication1.service
{
    public class my_logic_service : logic_service
    {
        protected override void build_user_manager()
        {
            account_manager = new my_account_manager(this);
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new my_logic_service_event_builder();
            ;
        }

        protected class my_logic_service_event_builder : logic_service_event_builder
        {
            public override event_base build_event(string event_type)
            {
                switch (event_type)
                {
                    case event_async_load_db_response.type:
                        return new event_my_async_load_db_response();
                }


                var e = base.build_event(event_type);
                if (e != null)
                    return e;

                return null;
            }
        }

//         {
//         public override void receive_package(event_receive_package package)

// 
//             receive_package_info info = package.data as receive_package_info;
//             if(info == null)
//                 throw new Exception("my_logic_service.receive_package package.data is not receive_package_info");
//             if(!info.package.is_parsed)
//                 throw new Exception("my_logic_service.receive_package package is not parsed");
// 
//             switch ((net_package_type)info.package.type)
//             {
//                 case net_package_type.action:
//                 {
//                     process_action_package(info.package);
//                 }
//                     break;
//             }
//         }
// 
// 
//         private void process_action_package(net_package package)
//         {
//             account_check_login_result result = account_manager.check_login(package);
//             if (result == account_check_login_result.need_remote_check)
//             {
// 
//             }
//             else if (result == account_check_login_result.login_ok)
//             {
// 
//             }
//             else
//             {
// 
//             }
//         }
    }
}