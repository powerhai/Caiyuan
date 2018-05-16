﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.DB.Models;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Caiyuan.Web.Config;
using Caiyuan.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Caiyuan.Web.Controllers
{
    public class OAuth2Controller : Controller
    {
        private readonly UserManager mUserManager;
        private readonly SignInManager mSignInManager;
        private readonly  WeChatSettings  mWeChatSettings;
        ////下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        //private string appId = ConfigurationManager.AppSettings["TenPayV3_AppId"];
        //private string secret = ConfigurationManager.AppSettings["TenPayV3_AppSecret"];
        public OAuth2Controller (IOptions<WeChatSettings> weChatSettings , UserManager userManager,
             SignInManager signInManager)
        {
            mUserManager = userManager;
            mSignInManager = signInManager;
            mWeChatSettings = weChatSettings.Value;
        }

        public ActionResult Index()
        {
            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] = OAuthApi.GetAuthorizeUrl(mWeChatSettings.AppID, "http://sdk.weixin.senparc.com/oauth2/UserInfoCallback", "JeffreySu", OAuthScope.snsapi_userinfo);
            ViewData["UrlBase"] = OAuthApi.GetAuthorizeUrl(mWeChatSettings.AppID, "http://sdk.weixin.senparc.com/oauth2/BaseCallback", "JeffreySu", OAuthScope.snsapi_base);
            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<ActionResult> UserInfoCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(mWeChatSettings.AppID, mWeChatSettings.AppSecret, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //HttpContext.Session.SetString("OAuthAccessTokenStartTime", DateTime.Now.ToString());
            //HttpContext.Session.SetInt32("OAuthAccessToken", result);

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                {
                    var user = new User { UserName = "User" + DateTime.Now.ToBinary() };
                    var res = await mUserManager.CreateAsync(user);
                    if (res.Succeeded)
                    {
                        var info = await mSignInManager.GetExternalLoginInfoAsync();
                        res = await mUserManager.AddLoginAsync(user, info);
                        if (res.Succeeded)
                        {
                            await mSignInManager.SignInAsync(user, isPersistent: false);
                            
                        }
                    }
                }
                return View(userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var result = OAuthApi.GetAccessToken(mWeChatSettings.AppID, mWeChatSettings.AppSecret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            //Session["OAuthAccessToken"] = result;

            //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            OAuthUserInfo userInfo = null;
            try
            {
                //已关注，可以得到详细信息
                userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                ViewData["ByBase"] = true;
                return View("UserInfoCallback", userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                //未关注，只能授权，无法得到详细信息
                //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                return Content("用户已授权，授权Token：" + result);
            }
        }
    }
}
