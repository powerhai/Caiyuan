using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caiyuan.Simulators.WeChat
{
    public class MicrosoftIEHelper
    {
        public enum IEVersions : long
        {
            IE7 = 7000,
            IE8 = 8000,
            IE9 = 9000,
            IE11 = 10000
        }
        public static void RegisterHighLevelWebBrowser(IEVersions version = IEVersions.IE11)
        {
            try
            {
                var appName = AppDomain.CurrentDomain.FriendlyName;
                var regPath = "SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
                //var regPath2 = "SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
                if(!IsRegistryExist(Registry.LocalMachine, regPath, appName))
                {
                    SetRegistryData(Registry.LocalMachine, regPath, appName, Convert.ToDouble(version));
                }                             
            }
            catch (Exception e)
            {

                MessageBox.Show("需要以管理员身份运行一次" );
                
            }
           
        }

        private static void SetRegistryData(RegistryKey root, string subkey, string name, object value)
        {
            RegistryKey aimdir = root.OpenSubKey(subkey,true);
            aimdir.SetValue(name, value, RegistryValueKind.DWord);
        }

        private static bool IsRegistryExist(RegistryKey root, string subkey, string name)
        {
            bool _exit = false;
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, false );
            var val = myKey.GetValue(name);
            return val != null; 
        }
    }
}
