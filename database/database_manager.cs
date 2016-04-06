using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    class database_manager : i_database_manager
    {
        Dictionary<uint, i_database> _dict_db;

        public database_manager()
        {
            _dict_db = new Dictionary<uint, i_database>();
        }

        public void init()
        {
            
        }

        public i_databae get_database(uint id)
        {
            
        }
    }
}
