# UEditor_Core_OSS
基于百度的ueditor.core 封装的支持阿里云OSS存储

# 详细的使用方式如下

`Startup.cs`增加如下代码

```c#
services.AddCzarUEditorService();  //相关配置可参考UEditor.Core
```

`ueditor.json` 增加是否支持对oss的配置记录,详细的配置如下

```json
"UseOss": true, //是否启用Oss存储上传文件 ,使用使用false，请参考原始的配置记录
"OssAccessKeyId": "阿里云配置的AccessKeyId",
"OssAccessKeySecret": "阿里云配置的AccessKeySecret",
"OssEndpoint": "阿里云的Endpoint",
"OssBucketName": "阿里云配置的BucketName",
"OssViewBaseUrl": "文件预览的前缀地址"   //预览地址
//同时需要把以下文件配置的前缀去掉(/)。
"imageUrlPrefix": "", /* 图片访问路径前缀 */
"scrawlUrlPrefix": "", /* 图片访问路径前缀 */
"catcherUrlPrefix": "", /* 图片访问路径前缀 */
"videoUrlPrefix": "", /* 视频访问路径前缀 */
"fileUrlPrefix": "", /* 文件访问路径前缀 */

```

增加处理控制器`UEditorController.cs`