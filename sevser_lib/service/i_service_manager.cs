using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.base_lib.service
{
    public interface i_service_manager
    {
        void add_service(service_base ser);
        void get_serivce(string name);
    }
}
