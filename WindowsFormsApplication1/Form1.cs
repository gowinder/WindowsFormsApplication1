// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// Form1.cs
// 2016-05-13-12:17

#region

using System;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApplication1.data;
using WindowsFormsApplication1.service;
using gowinder.base_lib.service;
using gowinder.database;
using gowinder.http_service;
using gowinder.http_service_lib;
using gowinder.socket_service_lib;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NLog;

#endregion

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private http_server_service service { get; set; }
        private async_save_db_service async_save_db_service { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            var log = LogManager.GetCurrentClassLogger();
            log.Trace("fuck");
            log.Debug("debug fuck");
            log.Info("info");

            var h = new http_requst_service();
            //          h.test();

            h.test_login();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dl = new data_login {user_name = "test1", user_pwd = "1234", action_type = 1};

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(dl);
            Console.WriteLine(json);

            var str_json =
                @"{""user_name"":""test1"",""user_pwd"":""1234"",""type"":1,,""sub_type"":1,""_i"":1,""ret"":0, ""fuck"":""asdf""}";
            var jo = (JObject) JsonConvert.DeserializeObject(str_json);
            var zone = jo["fuck"].ToString();


            var p1 = serializer.Deserialize<data_login>(str_json);
            Console.WriteLine(p1.ToString());

            /*

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
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var schemaJson = @"{
                  'description': 'A person',
                  'type': 'object',
                  'properties': {
                    'name': {'type':'string'},
                    'hobbies': {
                      'type': 'array',
                      'items': {'type':'string'}
                    }
                  }
                }";
            var schema = JsonSchema.Parse(schemaJson);
            JObject jobj = (JObject) JsonConvert.DeserializeObject(schemaJson);
            Console.WriteLine(schema.Type);
            // Object

            JToken token = jobj.SelectToken("data");
            var json_data = (JObject)jobj["data"];

            foreach (var property in schema.Properties)
            {
                Console.WriteLine(property.Key + " - " + property.Value.Type);
            }

            foreach (var property in jobj.Properties())
            {
                Console.WriteLine(property.Name + " - " + property.Value);
            }
            return;
            var conn = new MySqlConnection("server=localhost;User Id=root;password=asdf;Database=pd");
            conn.Open();
            var command = new MySqlCommand("select * from game_role where id = 1", conn);
            var rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                for (var i = 0; i < rdr.FieldCount; i++)
                {
                    var sb = new StringBuilder();
                    sb.Append(rdr.GetName(i));
                    sb.Append(":");
                    sb.Append(rdr[i]);
                    Console.WriteLine(sb.ToString());
                }
            }
            rdr.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var http_ser = new http_server_service {start_own_thread = true};
            var logic_ser = new my_logic_service {start_own_thread = true};
            var http_listerner_ser = new http_listerner_service("http://127.0.0.1:9981/test_request/")
            {
                start_own_thread = true,
                context_manager = http_ser,
                receive_package_service = logic_ser,
                http_ser = http_ser
            };

            http_listerner_ser.net_package_parser = new http_string_net_package_parser(http_listerner_ser);

            var async_load_db_ser = new my_async_load_db_service();

            var socket_ser = new socket_service(socket_service.service_type.server);
            var socket_listerner_ser = new socket_listerner_service(socket_ser)
            {
                receive_package_service = logic_ser,
                send_package_service = socket_ser
            };
            var socket_net_package_parser = new socket_string_net_package_parser(socket_ser, logic_ser);
            socket_listerner_ser.net_package_parser = socket_net_package_parser;
            socket_ser.net_package_parser = socket_net_package_parser;


            service_manager.instance().add_service(http_ser);
            service_manager.instance().add_service(http_listerner_ser);
            service_manager.instance().add_service(logic_ser);
            service_manager.instance().add_service(async_load_db_ser);
            service_manager.instance().add_service(socket_ser);
            service_manager.instance().add_service(socket_listerner_ser);

            service_manager.instance().start_all();
        }
    }
}