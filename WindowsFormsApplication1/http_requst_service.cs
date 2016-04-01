using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class http_requst_service
    {
        public async static Task<string> get_respons(string req)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}, task:{1}, thread{2}, ispoos{3}", req, Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            var response = client.GetAsync(req);
            string str_respons = await response.Result.Content.ReadAsStringAsync();

            Console.WriteLine("recive respons({0})", req);

            return str_respons;
        }


        public void test()
        {
            string[] reqs = new string[5]
            {
                //"http://www.baidu.com",
                //"http://www.sina.com.cn",
                //"http://www.sohu.com",
                //"http://www.readfree.me",
                //"http://www.v2ex.com"

                "http://127.0.0.1:9981/test_request?id=1&sleep=2000",
                "http://127.0.0.1:9981/test_request?id=2&sleep=2000",
                "http://127.0.0.1:9981/test_request?id=3&sleep=2000",
                "http://127.0.0.1:9981/test_request?id=4&sleep=2000",
                "http://127.0.0.1:9981/test_request?id=5&sleep=2000"
            };
            foreach (var req in reqs)
            {
                Task t = Task.Run(() => get_respons_async(req));
                //                 Task t = new Task(this.get_respons_async, req, TaskCreationOptions.LongRunning);
                //                 t.Start();

                //var t1 = Task<string>.Factory.StartNew(() => get_respons(req));
                //t1.Start();
                Console.WriteLine("return req:{0}", req);// t1.Result);
            }
        }



        protected async void get_respons_async(string req)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}, task:{1}, thread{2}, ispool:{3}", req, Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            var response = await client.GetAsync(req);
            string str_respons = await response.Content.ReadAsStringAsync();

            Console.WriteLine("recive respons({0}), return:{1}", req, str_respons);
        }
    }
}
