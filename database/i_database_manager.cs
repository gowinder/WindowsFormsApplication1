using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    interface i_database_manager
    {
        i_database get_database(uint id);
    }
}
