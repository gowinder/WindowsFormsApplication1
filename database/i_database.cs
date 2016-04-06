using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public interface i_database
    {
        bool open(string host, string db_name, string db_pwd, int port);


    }
}
