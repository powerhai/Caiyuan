using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Simulators.WeChatWebServer.Core;



namespace Caiyuan.Simulators.WeChatWebServer.Services
{
    /// <summary>
    /// Pushs events to caiyuan server.
    /// </summary>
    public class EventPusher
    {
        public string CaiyuanServerUrl
        {
            get;
            private set;
        }
        public EventPusher ()
        {
            CaiyuanServerUrl = "http://localhost:1978/";
        }
        private void PushMessage(string message)
        {
            var client = new System.Net.Http.HttpClient();
            var content = new System.Net.Http.StringContent(message);
            client.PostAsync("http://localhost:1978/", content);
        }
        public  void   PushSubscribeEvent (string from)
        {
            var doc = ChatMessageBuilder.BuildSubscribeEvent(from, "to");
            PushMessage(doc.ToString());
        }
        public void PushUnsubscribeEvent(string from)
        {
            var doc = ChatMessageBuilder.BuildUnsubscribeEvent(from, "to");
            PushMessage(doc.ToString());
        }
    }
}
