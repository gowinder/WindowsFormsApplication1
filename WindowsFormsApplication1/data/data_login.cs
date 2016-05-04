// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// data_login.cs
// 2016-05-04-9:34

#region

using ProtoBuf;

#endregion

namespace WindowsFormsApplication1.data
{
    [ProtoContract]
    internal class data_login : http_request_data
    {
        [ProtoMember(3)]
        public string user_name { get; set; }

        [ProtoMember(4)]
        public string user_pwd { get; set; }
    }
}