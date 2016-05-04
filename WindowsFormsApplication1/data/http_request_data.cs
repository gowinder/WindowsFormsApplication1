// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// http_request_data.cs
// 2016-05-04-9:34

#region

using ProtoBuf;

#endregion

namespace WindowsFormsApplication1.data
{
    [ProtoContract]
    [ProtoInclude(3, typeof (data_login))]
    public class http_request_data
    {
        [ProtoMember(1)]
        public int action_type { get; set; }

        [ProtoMember(2)]
        public int ret { get; set; }
    }
}