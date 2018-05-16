using System.Collections.ObjectModel;

namespace Caiyuan.Simulators.WeChat.WeChatServer
{
    public class ServerViewModel
    {
        public ObservableCollection<ChatMessage> ChatMessages  { get; } = new ObservableCollection<ChatMessage>();
        public ServerViewModel ()
        {
             
        }

    }
}