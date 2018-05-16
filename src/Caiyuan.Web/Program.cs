using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel;
using System.Security.Cryptography.X509Certificates;

namespace Caiyuan.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel( ).UseUrls( "http://*:80")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
              
                .Build();

            host.Run(); 
        }

        private static Action<KestrelServerOptions> ConfigHttps()
        {
            return x => {
                var pfxFile = Path.Combine(Directory.GetCurrentDirectory(), "*.pfx");
                //password 填写申请的密钥
                var certificate = new X509Certificate2(pfxFile, "password");
                x.UseHttps(certificate);
            };
        }



    }
}
