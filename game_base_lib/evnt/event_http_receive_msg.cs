using System;
using gowinder.http_service_lib.evnt;


namespace gowinder.game_base_lib.evnt
{
    public class event_http_receive_msg : event_receive_msg
    {
        public override void process()
        {
            logic_service ser = to_service as logic_service;
            if(ser == null)
                throw new Exception("event_http_receive_msg.process to service is not logic_serivce");

            receive_msg_info info = this.data as receive_msg_info;
            ser.receive_msg(this);
        }
    }

}
