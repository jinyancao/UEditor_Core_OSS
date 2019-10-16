using System;
using System.IO;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace Czar.UEditor_Core_OSS
{
    public class OssApiHelper
    {
        private readonly Aliyun.OSS.OssClient _client;
        private readonly string _bucketName;

        public OssApiHelper()
        {
            _client = new OssClient(Config.GetString("OssEndpoint"), Config.GetString("OssAccessKeyId"), Config.GetString("OssAccessKeySecret"));
            _bucketName = Config.GetString("OssBucketName");
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="key">文件唯一值</param>
        /// <param name="stream">文件流</param>
        public OssUploadResult PutObject(string key, Stream stream)
        {
            try
            {
                _client.PutObject(_bucketName, key, stream);
                return new OssUploadResult();
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="key">唯一键</param>
        /// <param name="filename">文件路径</param>
        public OssUploadResult PutObject(string key, string filename)
        {
            try
            {
                _client.PutObject(_bucketName, key, filename);
                return new OssUploadResult();
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        /// <summary>
        /// 上传字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        public OssUploadResult PutObjectByString(string key, string str)
        {
            try
            {
                var binaryData = Encoding.ASCII.GetBytes(str);
                var stream = new MemoryStream(binaryData);
                _client.PutObject(_bucketName, key, stream);

                return new OssUploadResult();
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        public OssUploadResult GetObject(string key)
        {
            try
            {
                var result = _client.GetObject(_bucketName, key);
                return new OssUploadResult
                {
                };
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="key">唯一键</param>
        /// <param name="filename">文件保存路径名称</param>
        public OssUploadResult GetObject(string key, string filename)
        {
            try
            {
                var result = _client.GetObject(_bucketName, key);
                using (var requestStream = result.Content)
                {
                    using (var fs = File.Open(filename, FileMode.OpenOrCreate))
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        do
                        {
                            length = requestStream.Read(buf, 0, length);
                            fs.Write(buf, 0, length);
                        } while (length != 0);
                    }
                }

                return new OssUploadResult();
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public OssUploadResult GetIamgeUri(string key, float width, float height)
        {
            try
            {
                var process = $"image/resize,m_fixed,w_{width},h_{height}";
                var req = new GeneratePresignedUriRequest(_bucketName, key, SignHttpMethod.Get)
                {
                    Expiration = DateTime.Now.AddMinutes(10),
                    Process = process
                };

                // 产生带有签名的URI
                var uri = _client.GeneratePresignedUri(req);

                return new OssUploadResult { };
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };

            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="key">唯一键</param>
        public OssUploadResult DeleteObject(string key)
        {
            try
            {
                _client.DeleteObject(_bucketName, key);
                return new OssUploadResult();
            }
            catch (OssException ex)
            {
                return new OssUploadResult
                {
                    Code = -1,
                    Message = $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}"
                };
            }
            catch (Exception ex)
            {
                return new OssUploadResult
                {
                    Code = -2,
                    Message = $"Failed with error info: {ex.Message}"
                };
            }
        }

    }
}
