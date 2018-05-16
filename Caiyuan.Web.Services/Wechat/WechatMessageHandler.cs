using System.Xml.Linq;
using Caiyuan.DB.Access;
using Caiyuan.DB.Models;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace Caiyuan.Web.Services.Wechat
{
    public class WechatMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        private readonly AppDbContext mDbContext;
        private readonly ILogger mLogger;
        public WechatMessageHandler(XDocument reqDoc,PostModel postModel,  AppDbContext dbContext, ILogger logger,int maxRecordCount = 0) : base(reqDoc,postModel, maxRecordCount)
        {
            mDbContext = dbContext;
            mLogger = logger;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();  
            responseMessage.Content = "null";
            return responseMessage;
        }
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        { 
            mLogger.LogDebug("requestMessage, {0} , {1}, {2}, {3}, {4} " , requestMessage.MsgId , requestMessage.MsgType, requestMessage.FromUserName ,  requestMessage.Encrypt, requestMessage.ToUserName);
            var userid = requestMessage.FromUserName;
            var mis = CreateResponseMessage<ResponseMessageText>();
           
            mis.Content = "hi!";
            mLogger.LogDebug("Add(openUser)");

       
            mLogger.LogDebug("SaveChanges();");
            return mis;
        }

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return base.OnEvent_UnsubscribeRequest(requestMessage);
        }
    }
}
