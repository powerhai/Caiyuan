using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Caiyuan.DB.Models;
using Caiyuan.Web.Config;
using Caiyuan.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Caiyuan.Web.Controllers
{

    [Authorize()]
     public class VipController : Controller
    {
        private readonly UserManager mUserManager;
        private readonly SignInManager mSignInManager;
        private readonly  WeChatSettings  mWeChatSettings;

        public VipController (IOptions<WeChatSettings> weChatSettings, UserManager userManager,
             SignInManager signInManager)
        {
            mUserManager = userManager;
            mSignInManager = signInManager;
        
            mWeChatSettings = weChatSettings.Value;
        }
 
 
 
         

        [AllowAnonymous]
        public async  Task<IActionResult> Haiser (string ReturnUrl)
        {
            User user = mUserManager.Users.FirstOrDefault();
            ClaimsPrincipal principal = HttpContext.User  as ClaimsPrincipal;
            var cls = new List<Claim>();
            cls.Add( new Claim(ClaimTypes.Name, "haiser" ));
            cls.Add(new Claim(ClaimTypes.Role ,"Users"));
            ClaimsIdentity ci = new ClaimsIdentity( cls, "Wechat") { };
            ClaimsPrincipal cp  = new ClaimsPrincipal(ci);

            await HttpContext.Authentication.SignInAsync("Wechat", cp  );//  mSignInManager.SignInAsync(user, isPersistent:true);

         return Redirect(ReturnUrl);
        }

 
    }

   
}
