using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    class db_manager : i_db_manager
    {
        Dictionary<uint, i_db> _dict_db;

        public db_manager()
        {
            _dict_db = new Dictionary<uint, i_db>();
        }

        public void init()
        {
            mysql_database db = new mysql_database();
            db.open("localhost", "pd", "root", "asdf", 3306);
            _dict_db.Add(1, db);
        }

        public i_db get_database(uint id)
        {
            return _dict_db[id];
        }
    }
}
