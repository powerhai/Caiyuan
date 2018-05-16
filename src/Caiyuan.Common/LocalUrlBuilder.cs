using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caiyuan.Common
{
    public static class LocalUriBuilder
    {
        public static Uri CreateHttpUri(string hostName, string controller, string action, string clinicID, int port = 80)
        {
            var builder = new UriBuilder("http", hostName, port)
            {
                Path = string.Format("{0}/{1}/{2}",controller,action, clinicID)
            };

            return builder.Uri;
        }
        public static Uri CreateHttpsUrl (string hostName, string controller, string action, string clinicID, int port = 80)
        {
            var builder = new UriBuilder("http", hostName, port)
            {
                Path = string.Format("{0}/{1}/{2}", controller, action, clinicID)
            };

            return builder.Uri;
        }
    }
}
