using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.base_lib.evnt
{

    /// <summary>
    /// 基本事件构造器
    /// </summary>
    public class base_event_builder : i_event_builder
    {
        public event_base build_event(String event_type)
        {
            switch (event_type)
            {
                case event_stop_service.type:
                    {
                        return new event_stop_service();
                    }
                default:
                    return null;
            }
        }
    }
}
