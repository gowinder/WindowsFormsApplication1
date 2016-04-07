using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    abstract public class  async_load_db_service : database_service_base
    {
        public async_load_db_service(string service_name)
        {
            this.name = name;
        }
    }
}
