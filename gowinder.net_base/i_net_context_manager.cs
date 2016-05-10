// gowinder@hotmail.com
// gowinder.net_base
// i_net_context_manager.cs
// 2016-05-10-14:11

namespace gowinder.net_base
{
    public interface i_net_context_manager
    {
        void add_context(net_context context);

        net_context find_by_id(uint id);

        void remove_by_id(uint id);

        uint get_new_id();
    }
}