using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.Shared.Config
{
    public class MiniIoConfig
    {
          
        /// <summary>
        /// 服务器
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 桶
        /// </summary>
        public string Bucket { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public string BucketDirectory { get; set; }
    }
}
