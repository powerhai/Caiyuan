using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Caiyuan.Web.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using Caiyuan.Web.Services.Wechat;

namespace Caiyuan.Web.Controllers
{
    public class HomeController : Controller
    {
        private WeChatSettings mWeChatSettings;
       
        private readonly  UserManager mUserManager;
        private readonly ILogger mLogger;
        public IEmailSender MailSender
        {
            get;
            set;
        }
        public HomeController( UserManager userManager, IOptions<WeChatSettings> weChatSettings , ILoggerFactory loggerFactory)
        {
            mWeChatSettings = weChatSettings.Value;
            mUserManager = userManager;
            MailSender = null;
            mLogger = loggerFactory.CreateLogger("Home");
           

        }
        public IActionResult Index()
        { 
            var tt = WechatOAuthUrlBuilder.GetUri("123", "www.weyake.com", "home", "index","3232");
            string v = @"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName> 
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";
            var cc = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(v));
            var doc = XDocument.Load(cc);

            mLogger.LogDebug("User Index");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
