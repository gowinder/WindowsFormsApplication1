using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.data
{
    [ProtoContract]
    [ProtoInclude(3, typeof(data_login))]
    public class http_request_data
    {
        [ProtoMember(1)]
        public int action_type { get; set; }
        [ProtoMember(2)]
        public int ret { get; set; }
    }
}
