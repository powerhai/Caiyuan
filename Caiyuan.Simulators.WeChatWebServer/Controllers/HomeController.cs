using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Simulators.WeChatWebServer.Core;
using Caiyuan.Simulators.WeChatWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Caiyuan.Simulators.WeChatWebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventPusher mEventPusher;
        public HomeController (EventPusher eventPusher)
        {
            mEventPusher = eventPusher;
        }
        public async Task<IActionResult> Index()
        {
            var rec =  new ContentResult();
            rec.Content = "hai";
            //var client = new RestClient("http://baidu.com");
            //// client.Authenticator = new HttpBasicAuthenticator(username, password);

            //var request = new RestRequest("/", Method.GET);

            //client.Execute(request , a=> {
            //     rec.Content = a.Content;
            //});var cc = new FormUrlEncodedContent(new Dictionary<string, string>());  

            var client =new System.Net.Http.HttpClient();
            var cd = await client.GetAsync("http://www.baidu.com");
            cd.EnsureSuccessStatusCode();
            var s = await cd.Content.ReadAsStringAsync();

            rec.Content = s;
            return rec;
        }

        public async Task<IActionResult> Subscribe(string from)
        { 
            mEventPusher.PushSubscribeEvent(from);
            return new EmptyResult();
        }

        public async Task<IActionResult> Unsubscribe(string from)
        {
            mEventPusher.PushUnsubscribeEvent(from);
            return new EmptyResult();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
