using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.data
{
    [ProtoContract]
    class data_login : http_request_data
    {
        [ProtoMember(3)]
        public string user_name { get; set; }
        [ProtoMember(4)]
        public string user_pwd { get; set; }
    }
}
