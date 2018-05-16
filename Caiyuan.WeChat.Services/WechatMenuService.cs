
using Microsoft.Extensions.Options;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caiyuan.Web.Services
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
            var menu = WeChatMenuBuilder.GetPatientMenu();
            var res = CommonApi.CreateMenu(accessToken, menu);              
        } 
    }
}
