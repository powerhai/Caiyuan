using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Caiyuan.Simulators.WeChat.Common
{
    public class ChatMessageBuilder
    {
        private static XDocument BuildCommonMessageBody(string from , string to)
        {
            var doc = new XDocument(
                new XElement("xml",
                    new XElement("ToUserName", new XCData(to)),
                    new XElement("FromUserName", new XCData(from) ),
                    new XElement("CreateTime", DateTime.Now.ToBinary())
                    )
                ); 
            return doc;
        }
        public static XDocument  BuildTextMessage(string from, string to, string content)
        {  
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("text")) );
            doc.Root?.Add(new XElement("Content", new XCData(content)));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            return doc;
        }
        public static XDocument BuildImageMessage(string from, string to, string url, string mediaId)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("image")));
            doc.Root?.Add(new XElement("PicUrl", new XCData(url)));
            doc.Root?.Add(new XElement("MediaId", new XCData(mediaId)));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            return doc;
        }
        public static XDocument BuildVoiceMessage(string from, string to,   string mediaId ,string format)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("voice"))); 
            doc.Root?.Add(new XElement("MediaId", new XCData(mediaId)));
            doc.Root?.Add(new XElement("Format", new XCData(format)));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            return doc;
        }

        public static XDocument BuildVideoMessage(string from, string to, string mediaId, string thumbMediaId)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("video")));
            doc.Root?.Add(new XElement("MediaId", new XCData(mediaId)));
            doc.Root?.Add(new XElement("ThumbMediaId", new XCData(thumbMediaId)));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            return doc;
        }


        public static XDocument BuildShortVideoMessage(string from, string to, string mediaId, string thumbMediaId)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("shortvideo")));
            doc.Root?.Add(new XElement("MediaId", new XCData(mediaId)));
            doc.Root?.Add(new XElement("ThumbMediaId", new XCData(thumbMediaId)));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            return doc;
        }

        public static XDocument BuildLocationMessage(string from, string to, string x, string y,int scale, string label)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("location")));  
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            doc.Root?.Add(new XElement("Location_X",  x ));
            doc.Root?.Add(new XElement("Location_X",  y ));
            doc.Root?.Add(new XElement("Scale", scale));
            doc.Root?.Add(new XElement("Label",  new XCData(label)));
            return doc;
        }

        public static XDocument BuildLikeMessage(string from, string to, string title, string description,  string url)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("like")));
            doc.Root?.Add(new XElement("MsgId", DateTime.Now.ToBinary()));
            doc.Root?.Add(new XElement("Title", title));
            doc.Root?.Add(new XElement("Description", description));
            doc.Root?.Add(new XElement("Url", url)); 
            return doc;
        }

        public static XDocument BuildSubscribeEvent (string from, string to)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("event")));
            doc.Root?.Add(new XElement("Event", new XCData("subscribe"))); 
            return doc;
        }

        public static XDocument BuildUnsubscribeEvent(string from, string to)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("event")));
            doc.Root?.Add(new XElement("Event", new XCData("unsubscribe")));
            return doc;
        }
        public static XDocument BuildClickEvent(string from, string to,string menuKey)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("event")));
            doc.Root?.Add(new XElement("Event", new XCData("CLICK")));
            doc.Root?.Add(new XElement("EventKey", new XCData(menuKey)));
            return doc;
        }

        public static XDocument BuildViewEvent(string from, string to, string url)
        {
            var doc = BuildCommonMessageBody(from, to);
            doc.Root?.Add(new XElement("MsgType", new XCData("event")));
            doc.Root?.Add(new XElement("Event", new XCData("VIEW")));
            doc.Root?.Add(new XElement("EventKey", new XCData(url)));
            return doc;
        }
    }
}
