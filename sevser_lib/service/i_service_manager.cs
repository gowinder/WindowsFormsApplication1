// gowinder@hotmail.com
// gowinder.base_lib
// i_service_manager.cs
// 2016-05-10-14:11

#region

using gowinder.base_lib.evnt;

#endregion

namespace gowinder.base_lib.service
{
    public interface i_service_manager
    {
        void add_service(service_base ser);
        service_base get_serivce(string name);
        void start_all();
        void stop_all();
        void send_all(event_base e);
    }
}