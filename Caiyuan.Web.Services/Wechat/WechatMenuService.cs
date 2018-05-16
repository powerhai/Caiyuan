using Caiyuan.Web.Config;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;

namespace Caiyuan.Web.Services.Wechat
{
    public class WechatMenuService
    {
        private readonly WeChatSettings mWeChatSettings;
        public WechatMenuService(IOptions<WeChatSettings> weChatSettings)
        {
            mWeChatSettings = weChatSettings.Value;
        }
        public void UpdateMenu()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(mWeChatSettings.AppID, mWeChatSettings.AppSecret);
            var menu = WeChatMenuBuilder.GetPatientMenu("appid");
            var res = CommonApi.CreateMenu(accessToken, menu);              
        } 
    }
}
