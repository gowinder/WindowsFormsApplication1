using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.net_base
{
    public class net_package
    {
        public net_package()
        {
            is_parsed = false;
        }

        public uint type { get; set; }
        public uint sub_type { get; set; }
        public uint size { get; set; }
        public uint index { get; set; }
        public string token { get; set; }
        public byte[] data { get; set; }

        
        /// <summary>
        /// 消息是否解析过
        /// </summary>
        public bool is_parsed { get; set; }
    }
}
