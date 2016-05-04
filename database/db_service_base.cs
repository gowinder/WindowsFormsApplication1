// gowinder@hotmail.com
// gowinder.database
// db_service_base.cs
// 2016-05-04-9:34

#region

using System.Collections.Generic;
using gowinder.base_lib;

#endregion

namespace gowinder.database
{
    public class db_service_base : service_base, i_db_manager
    {
        private readonly Dictionary<uint, i_db> _dict_db = new Dictionary<uint, i_db>();

        public db_service_base()
        {
            _dict_db = new Dictionary<uint, i_db>();
        }

        public i_db get_database(uint id)
        {
            return _dict_db[id];
        }

        protected override void init()
        {
            var db = new mysql_database();
            db.open("localhost", "pd", "root", "asdf", 3306);
            _dict_db.Add(1, db);
        }
    }
}