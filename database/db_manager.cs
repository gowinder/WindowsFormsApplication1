// gowinder@hotmail.com
// gowinder.database
// db_manager.cs
// 2016-05-10-14:11

#region

using System.Collections.Generic;

#endregion

namespace gowinder.database
{
    internal class db_manager : i_db_manager
    {
        private readonly Dictionary<uint, i_db> _dict_db;

        public db_manager()
        {
            _dict_db = new Dictionary<uint, i_db>();
        }

        public i_db get_database(uint id)
        {
            return _dict_db[id];
        }

        public void init()
        {
            var db = new mysql_database();
            db.open("localhost", "pd", "root", "asdf", 3306);
            _dict_db.Add(1, db);
        }
    }
}