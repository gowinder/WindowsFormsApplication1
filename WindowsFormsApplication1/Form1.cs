using NLog;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.data;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private http_service service { get; set; }

        public Form1()
        {
            InitializeComponent();
            service = new http_service();
            service.run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Trace("fuck");
            log.Debug("debug fuck");
            log.Info("info");

            http_requst_service h = new http_requst_service();
            h.test();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //    data_login dl = new data_login() { user_name = "test1", user_pwd = "1234", action_type = 1 };

            data_login dl = new data_login() { user_name = "test1", user_pwd = "1234", action_type = 1 };

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, dl);

            data_login hrd = Serializer.Deserialize<data_login>(ms);
            if(hrd.action_type == 1)
            {
                data_login ddl = hrd as data_login;
                Console.WriteLine(ddl.ToString());
            }
            Console.WriteLine(hrd.ToString());
        }
    }
}
