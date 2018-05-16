using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.DB.Access;
using Caiyuan.DB.Models;

namespace Caiyuan.Web.Services.Wechat
{
    public class GZHConfigService
    {
        private readonly AppDbContext mDbContext;
        public GZHConfigService (AppDbContext dbContext)
        {
            mDbContext = dbContext;
        }
        public GZHConfig GetGzhConfig (string wechatUserName)
        {
            return new GZHConfig();
        }
    }
}
