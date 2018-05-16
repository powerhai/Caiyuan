using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Senparc.Weixin.MP;
using Microsoft.Extensions.Options;
using Caiyuan.Web.Config;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.Containers;
using Caiyuan.Web.Services;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Caiyuan.DB.Access;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Caiyuan.Web.Services.Wechat;

namespace Caiyuan.Web.Controllers
{
    public class WeChatController : Controller
    {
        private WeChatSettings mWeChatSettings;
        private readonly WechatMenuService mWechatMenuService;
        private readonly AppDbContext mAppDbContext;
        private readonly ILogger mLogger;
        public WeChatController(IOptions<WeChatSettings> weChatSettings, WechatMenuService wechatMenuService, ILoggerFactory loggerFactory, AppDbContext dbContext)
        {
            mWeChatSettings = weChatSettings.Value;
            mWechatMenuService = wechatMenuService;
            mAppDbContext = dbContext;
            mLogger = loggerFactory.CreateLogger("info");
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("API")]
        public async Task<IActionResult> API( PostModel postModel )
        {
            mLogger.LogDebug("WECHAT API, Signature: {0} ,Timestamp: {1},Nonce:  {2},Token: {3}  ", postModel.Signature, postModel.Timestamp, postModel.Nonce, mWeChatSettings.Token );
             
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, mWeChatSettings.Token))
            {
                return new ContentResult() { Content = "Error" };
            }
            
            try
            {
                mLogger.LogInformation("new WechatMessageHandler");
                mLogger.LogInformation(mAppDbContext == null ? "NULL mAppDbContext" : "");

                if (Request.ContentLength.HasValue)
                {
                    mLogger.LogDebug("WECHAT CONTENT : {0}", Request.ContentLength);
                    
                    //var buf = new byte[Request.ContentLength.Value];
                    //Request.Body.Read(buf, 0, buf.Length);
                    
                    //var str = System.Text.Encoding.UTF8.GetString(buf);
                    //mLogger.LogDebug(str);
                    var doc = XDocument.Load(Request.Body);

                    mLogger.LogInformation("WECHAT body {0} ", doc );

                    var messageHandler = new WechatMessageHandler(doc, postModel, mAppDbContext, mLogger);
                    mLogger.LogInformation("messageHandler.Execute();");
                    messageHandler.Execute();
                    mLogger.LogInformation("new ContentResult()");
                    return new ContentResult() { Content = messageHandler.FinalResponseDocument.ToString() };
                } 
            }
            catch (Exception e)
            {
                mLogger.LogError("API ERROR, {0}", e.Message);
            }
            return new ContentResult() { Content = "" };
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("API")]
        public IActionResult ReplayTokenValidation(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, mWeChatSettings.Token))
            {
                return new ContentResult() { Content = echostr };
            }
            else
            {
                return Content("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, mWeChatSettings.Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdateManu()
        {
            mWechatMenuService.UpdateMenu();
            return new ContentResult() { Content = "OK" };
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult SendText()
        {

            var accessToken = AccessTokenContainer.TryGetAccessToken(mWeChatSettings.AppID, mWeChatSettings.AppSecret);
            const string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

            var data = new
            {
                touser = "odJ3Rv8X-heP3jd0wQDlO8xNgIdE",
                msgtype = "text",
                text = new
                {
                    content = "welcome to yongkang有人的"
                }
            };
            var rv = CommonJsonSend.Send(accessToken, URL_FORMAT, data);
            return new ContentResult() { Content = rv.errmsg };
        }


    }
    [DataContract(Namespace = "", Name ="")]
    public class MessageModel
    {
        public MessageModel()
        {

        }
        [DataMember]
        public string FromUserName { get; set; }
        [DataMember]
        public string ToUserName { get; set; }
        [DataMember]
        public string CreateTime { get; set; }
        [DataMember]
        public string MsgType { get; set; }
        [DataMember]
        public string Event { get; set; }
    }
}
