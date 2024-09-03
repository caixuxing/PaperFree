using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO;
using System.Security.AccessControl;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;

namespace Trasen.PaperFree.Infrastructure.SeedWork
{
    public class MinioFileService : IMinioFileService
    {
        private readonly MiniIoConfig minio = Appsetting.Instance.GetSection("MinIO").Get<MiniIoConfig>();
        private readonly IMinioClient minioClient;

        public MinioFileService(IMinioClient minioClient, ILogger<MinioFileService> logger)
        {
            this.minioClient = minioClient;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="storagePath">存储目录/</param>
        /// <returns></returns>
        public async Task<string> Upload(IFormFile file, string storagePath)
        {
            bool found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(minio.Bucket));
            if (!found)
                await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(minio.Bucket));
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;
            // 设置上传文件的对象名
            var objectName = file.FileName;
            var putObjectArgs = new PutObjectArgs()
               .WithBucket(minio.Bucket)
               .WithObject($"{storagePath}{objectName}")
               .WithStreamData(stream)
               .WithObjectSize(stream.Length)
               .WithContentType(file.ContentType);
            await minioClient.PutObjectAsync(putObjectArgs);
            return objectName;
        }
        public async Task<string> UploadStream(Stream byteStream, string storagePath,string type,string  filename)
        {

            bool found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(minio.Bucket));
            if (!found)
                await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(minio.Bucket));
            byteStream.Position = 0;
            // 使用传入的字节流
            var putObjectArgs = new PutObjectArgs()
            .WithBucket(minio.Bucket)
               .WithObject(Path.Combine(storagePath, filename))
               .WithStreamData(byteStream)
               .WithObjectSize(byteStream.Length)
               .WithContentType(type); // 或根据实际情况设置适当的内容类型
            await minioClient.PutObjectAsync(putObjectArgs);
            return filename;
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="storagePath">存储目录</param>
        /// <returns></returns>
        public async Task<byte[]> DownLoad(string fileName, string storagePath)
        {
            MemoryStream memoryStream = new MemoryStream();
            if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName + ".PDF";
            }

            await minioClient.StatObjectAsync(new StatObjectArgs().WithBucket(minio.Bucket).WithObject(Path.Combine(storagePath, fileName)));


            GetObjectArgs getObjectArgs = new GetObjectArgs()
                                .WithBucket(minio.Bucket)
                                .WithObject(Path.Combine(storagePath, fileName))
                                .WithCallbackStream((stream) =>stream.CopyTo(memoryStream));
            var data = await minioClient.GetObjectAsync(getObjectArgs);
            memoryStream.Position = 0;
           byte[] by= memoryStream.ToArray();
            return by;

        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="storagePath"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string fileName, string storagePath)
        {
            await minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(minio.Bucket).WithObject($"{storagePath}{fileName}"));
            return true;
        }
    }
}