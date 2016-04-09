using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    interface i_db_manager
    {
        i_db get_database(uint id);
    }
}
