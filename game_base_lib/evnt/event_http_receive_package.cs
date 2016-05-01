using System;
using gowinder.http_service_lib.evnt;


namespace gowinder.game_base_lib.evnt
{
    [Serializable]
    public class event_http_receive_package : event_receive_package
    {
        public const string type = "event_http_receive_package";

        public override void process()
        {
            logic_service ser = to_service as logic_service;
            if(ser == null)
                throw new Exception("event_http_receive_package.process to service is not logic_serivce");

            receive_package_info info = this.data as receive_package_info;
            ser.receive_package(this);
        }
    }

}
