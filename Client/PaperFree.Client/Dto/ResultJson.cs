using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto
{
   public class ResultJson<T>
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public virtual HttpStatusCode HttpStatus { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public virtual T Data { get; set; }

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public virtual string Message { get; set; }
    }
}
