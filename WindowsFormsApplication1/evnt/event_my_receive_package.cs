// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// event_my_receive_package.cs
// 2016-05-10-14:11

#region

using System;
using WindowsFormsApplication1.service;
using gowinder.net_base.evnt;

#endregion

namespace WindowsFormsApplication1.evnt
{
    internal class event_my_receive_package : event_receive_package
    {
        public override void process()
        {
            var ser = to_service as my_logic_service;
            if (ser == null)
                throw new Exception("event_my_receive_package.process to_service is not my_logic_service");

            ser.receive_package(this);
        }
    }
}