using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caiyuan.Simulators.WeChat.Common;

namespace Caiyuan.Simulators.WeChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);
             MicrosoftIEHelper.RegisterHighLevelWebBrowser();
            var doc = ChatMessageBuilder.BuildTextMessage("hai", "xinri", "fuck you").ToString();
        }
    }
}
