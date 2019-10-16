using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.UEditor_Core_OSS
{
    public class OssUploadResult
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = "操作成功";
    }
}
