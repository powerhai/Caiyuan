
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.Context;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities.Request;
using System.IO;
using Senparc.Weixin.MP.Entities.Menu;

namespace Caiyuan.Web.Services
{

    public static class WeChatMenuBuilder
    {
        public static ButtonGroup GetPatientMenu()
        {
            var menu = new ButtonGroup()
            {
                button = new List<BaseButton>
                {
                    new SubButton("应育芳牙科")
                    {
                        sub_button= new List<SingleButton> {
                            new SingleViewButton()
                            {
                                name="关于诊所",
                                url="http://www.baidu.com"
                            },
                            new SingleViewButton()
                            {
                                name="联系方式",
                                url="http://www.baidu.com"
                            },
                            new SingleViewButton()
                            {
                                name="前往诊所",
                                url="http://www.baidu.com"
                            },
                        }
                    },
                    new SubButton("就诊")
                    {
                        sub_button = new List<SingleButton> {
                            new SingleViewButton()
                            {
                                name="预约",
                                url="http://www.baidu.com"
                            },
                            new SingleViewButton()
                            {
                                name="治疗历史",
                                url="http://www.baidu.com"
                            },
                            new SingleViewButton()
                            {
                                name="口腔知识",
                                url="http://www.baidu.com"
                            },
                        }
                    },
                     new SubButton("我")
                    {
                        sub_button = new List<SingleButton> {
                            new SingleViewButton()
                            {
                                name="个人资料",
                                url="http://www.baidu.com"
                            },
                            new SingleViewButton()
                            {
                                name="帐户余额",
                                url="http://www.baidu.com"
                            }
                        }
                    },
                }
            };
            return menu;
        }
    }
    public class WechatMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public WechatMessageHandler(Stream inputStream, PostModel postModel)   : base(inputStream, postModel)
        {
        }
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();  
            responseMessage.Content = "null";
            return responseMessage;
        }
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var userid = requestMessage.FromUserName;
            return base.OnEvent_SubscribeRequest(requestMessage);
        }
    }
}
