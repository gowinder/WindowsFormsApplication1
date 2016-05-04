// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// http_requst_service.cs
// 2016-05-04-9:34

#region

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApplication1.data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;

#endregion

namespace WindowsFormsApplication1
{
    internal class http_requst_service
    {
        public static async Task<string> get_respons(string req)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}, task:{1}, thread{2}, ispoos{3}", req, Task.CurrentId,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            var response = client.GetAsync(req);
            var str_respons = await response.Result.Content.ReadAsStringAsync();

            Console.WriteLine("recive respons({0})", req);

            return str_respons;
        }


        public void test()
        {
            data_login[] dls =
            {
                new data_login {user_name = "test1", user_pwd = "1234"},
                new data_login {user_name = "test2", user_pwd = "1234"},
                new data_login {user_name = "test3", user_pwd = "1234"},
                new data_login {user_name = "test4", user_pwd = "1234"},
                new data_login {user_name = "test5", user_pwd = "1234"}
            };
            var reqs = new string[5]
            {
                //"http://www.baidu.com",
                //"http://www.sina.com.cn",
                //"http://www.sohu.com",
                //"http://www.readfree.me",
                //"http://www.v2ex.com"

                //"http://127.0.0.1:9981/test_request?id=1&sleep=2000&pwd=1234",
                //"http://127.0.0.1:9981/test_request?id=2&sleep=2000&pwd=1234",
                //"http://127.0.0.1:9981/test_request?id=3&sleep=2000&pwd=1234",
                //"http://127.0.0.1:9981/test_request?id=4&sleep=2000&pwd=1234",
                //"http://127.0.0.1:9981/test_request?id=5&sleep=2000&pwd=1234"

                "http://127.0.0.1:9981/test_request",
                "http://127.0.0.1:9981/test_request",
                "http://127.0.0.1:9981/test_request",
                "http://127.0.0.1:9981/test_request",
                "http://127.0.0.1:9981/test_request"
            };
            for (var i = 0; i < dls.Length; i++)
            {
                var req = reqs[i];
                var dl = dls[i];
                var t = Task.Run(() => get_respons_async(req, dl));
                //                 Task t = new Task(this.get_respons_async, req, TaskCreationOptions.LongRunning);
                //                 t.Start();

                //var t1 = Task<string>.Factory.StartNew(() => get_respons(req));
                //t1.Start();
                Console.WriteLine("return req:{0}", req); // t1.Result);
            }
        }


        protected async void get_respons_async(string req, data_login dl)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}, task:{1}, thread{2}, ispool:{3}", req, Task.CurrentId,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            try
            {
                var ms = new MemoryStream();
                Serializer.Serialize(ms, dl);
                var ht = new StreamContent(ms);
                var response = await client.PostAsync(req, ht);
                var str_respons = await response.Content.ReadAsStringAsync();

                Console.WriteLine("recive respons({0}), return:{1}", req, str_respons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("receive respons crash: " + ex.Message);
            }
        }

        protected async void get_response_async(string req, string reqesut_data)
        {
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            Console.WriteLine("begin request: {0}, task:{1}, thread{2}, ispool:{3}", req, Task.CurrentId,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            try
            {
//                 MemoryStream ms = new MemoryStream();
//                 Serializer.Serialize(ms, reqesut_data);
//                 StreamContent ht = new StreamContent(ms);
//                 StreamReader sr = new StreamReader(ms);
//                 // Note if the JSON is simple enough you could ignore the 5 lines above that do the serialization and construct it yourself
//                 // then pass it as the first argument to the StringContent constructor
//                 StringContent theContent = new StringContent(sr.ReadToEnd(), System.Text.Encoding.UTF8, "application/json");

                var stringContent = new StringContent(reqesut_data); //这里请求不需要参数  所以为“”
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await client.PostAsync(req, stringContent);
                var str_respons = await response.Content.ReadAsStringAsync();

                Console.WriteLine("recive respons({0}), return:{1}", req, str_respons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("receive respons crash: " + ex.Message);
            }
        }

        public void test_login()
        {
            var str_json =
                @"{""type"":2,""sub_type"":1,""_i"":1,""token"":"""",""platform_id"":0,""user_name"":""test1"",""user_pwd"":""1234"",""ret"":0, ""fuck"":""asdf""}";
            var jo = (JObject) JsonConvert.DeserializeObject(str_json);
            var zone = jo["fuck"].ToString();

            var t = Task.Run(() => get_response_async("http://127.0.0.1:9981/test_request", str_json));
        }
    }
}