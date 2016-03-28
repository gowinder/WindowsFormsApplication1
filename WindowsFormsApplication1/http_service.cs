using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class http_service
    {
        public http_service()
        {
            _listener = new HttpListener();
        }
        protected HttpListener _listener;
    /*    public Task run()
        {
            return Task.Run(async () =>
            {
                try
                {
                    _listener.Start();
                }
                catch (HttpListenerException hlex)
                {

                    return;
                }

                while (true)
                {
                    _listener.GetContextAsync().ContinueWith(async (t) =>
                    {
                        var ctx = await t;
                        await
                    });
                }
            });
        }
        */

        public void test()
        {
            string[] reqs = new string[5]
            {
                "http://www.baidu.com",
                "http://www.sina.com.cn",
                "http://www.sohu.com",
                "http://www.readfree.me",
                "http://www.v2ex.com"
            };
            foreach(var req in reqs)
            {
               
            }
        }

        protected async void get_respons_async(string req)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}", req);
            var response = await client.GetAsync(req);
            string str_respons = await response.Content.ReadAsStringAsync();

            Console.WriteLine("recive respons({0})", req);
        }
    }
}
