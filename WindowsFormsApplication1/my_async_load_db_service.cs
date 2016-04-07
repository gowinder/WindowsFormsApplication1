using gowinder.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class my_async_load_db_service : async_load_db_service
    {
        public my_async_load_db_service(string service_name):base(service_name)
        {
            
        }
    }
}
