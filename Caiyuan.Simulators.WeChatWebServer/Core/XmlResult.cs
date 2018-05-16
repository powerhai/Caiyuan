using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caiyuan.Simulators.WeChatWebServer.Core
{
    public class XmlResult : ActionResult
    {
        // 可被序列化的内容
        object Data { get; set; }

        // Data的类型
        Type DataType { get; set; }

        // 构造器
        public XmlResult(object data, Type type)
        {
            Data = data;
            DataType = type;
        }

        public override void ExecuteResult (ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
             
            response.ContentType = "text/xml";

            if (Data != null)
            {

                XmlSerializer serializer = new XmlSerializer(DataType);
                MemoryStream ms = new MemoryStream();
                serializer.Serialize(ms, Data);
                response.WriteAsync(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
            }

        }
    }
}
