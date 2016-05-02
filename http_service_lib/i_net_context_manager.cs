using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.net_base;

namespace gowinder.http_service_lib
{
    public interface i_net_context_manager
    {
        void add_context(net_context context);

        net_context find_by_id(uint id);

        void remove_by_id(uint id);
    }
}
