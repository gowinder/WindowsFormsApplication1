// gowinder@hotmail.com
// gowinder.database
// db_table_configer.cs
// 2016-05-13-15:13

namespace gowinder.database
{
    public class db_table_configer
    {
        protected static db_table_configer s_instance;
        public static db_table_configer instance => s_instance ?? (s_instance = new db_table_configer());

        public void init()
        {
            
        }

        public void test_init()
        {
            
        }

        public uint get_table_db_index(string str_table_name)
        {
            return 1;
        }
    }
}