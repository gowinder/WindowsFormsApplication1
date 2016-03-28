using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Trace("fuck");
            log.Debug("debug fuck");
            log.Info("info");

            http_service h = new http_service();
            h.test();
        }
    }
}
