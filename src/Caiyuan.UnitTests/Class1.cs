using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Caiyuan.Controllers;
using Caiyuan.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caiyuan.UnitTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [TestClass]
    public class Class1 :UnitTestBase
    {
        [TestMethod]
        public void test()
        { 
            var con = this.Resolve<HomeController>();
            var c = con.Index();
            Assert.IsNotNull(c);
            Assert.IsNotNull(con.MailSender);
        }

        protected override void RegisterTypes (ContainerBuilder builder)
        {  
            builder.RegisterType<AuthMessageSender>().As<IEmailSender>();
            builder.RegisterType<HomeController>();
        } 
    }
}
