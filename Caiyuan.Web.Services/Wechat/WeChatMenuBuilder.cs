using System.Collections.Generic;
using Senparc.Weixin.MP.Entities.Menu;

namespace Caiyuan.Web.Services.Wechat
{
    public static class WeChatMenuBuilder
    {
        private const string WECHAT_OAUTH_URL =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={redirectUrl}&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";

        public static ButtonGroup GetPatientMenu(string appId  )
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
                                name="登录测试",
                                url="http://www.weyake.cn/vip/index"
                            },
                            new SingleViewButton()
                            {
                                name="关于诊所",
                                url="http://www.weyake.cn/vip/index"
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
}