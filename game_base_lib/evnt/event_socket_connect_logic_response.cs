using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.socket_service_lib.evnt;

namespace gowinder.game_base_lib.evnt
{
    public class event_socket_connect_logic_response : event_socket_connect_response
    {
        public override void process()
        {
            logic_service logic_ser = to_service as logic_service;
            if(logic_ser == null)
                throw new NullReferenceException("event_socket_connect_logic_response.process to_service is not logic_service");

            
        }
    }
}
