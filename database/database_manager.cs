﻿using System;
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
            mysql_database db = new mysql_database();
            db.open("localhost", "pd", "root", "asdf", 3306);
            _dict_db.Add(1, db);
        }

        public i_database get_database(uint id)
        {
            return _dict_db[id];
        }
    }
}