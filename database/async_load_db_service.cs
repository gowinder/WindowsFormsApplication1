using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    abstract public class  async_load_db_service : db_service_base
    {
        public static string default_name = "async_load_db_service";

        public async_load_db_service(string service_name = "")
        {
            if(service_name == "")
                this.name = default_name;
            else
            {
                this.name = service_name;
            }
        }
    }
}
