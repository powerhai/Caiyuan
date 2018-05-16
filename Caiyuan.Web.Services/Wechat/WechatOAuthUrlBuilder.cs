using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Common;

namespace Caiyuan.Web.Services.Wechat
{
    public static class WechatOAuthUrlBuilder
    {
        private const string WECHAT_OAUTH_URL =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";

        public static string GetUri (string appId, string hostName,string controller, string action,string clinicID)
        {
            var uri = LocalUriBuilder.CreateHttpUri(hostName,controller,action, clinicID);
            var ccc = uri.ToString();
            var url = string.Format(WECHAT_OAUTH_URL, appId,  Uri.EscapeDataString( uri.ToString() ));
            return url;
        }
    }
}
