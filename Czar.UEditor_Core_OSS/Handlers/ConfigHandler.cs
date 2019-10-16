using Newtonsoft.Json.Linq;

namespace Czar.UEditor_Core_OSS.Handlers
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler
    {
        public JObject Process()
        {
            var JsonObject= Config.Items;
            //移除OSS配置的敏感信息(2019-10-16)
            JsonObject.Remove("UseOss");
            JsonObject.Remove("OssAccessKeyId");
            JsonObject.Remove("OssAccessKeySecret");
            JsonObject.Remove("OssEndpoint");
            JsonObject.Remove("OssBucketName");
            JsonObject.Remove("OssViewBaseUrl");
            return JsonObject;
        }
    }
}