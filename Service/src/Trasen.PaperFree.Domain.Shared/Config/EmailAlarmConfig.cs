using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.Shared.Config
{
    /// <summary>
    /// 邮件告警配置
    /// </summary>
    public class EmailAlarmConfig
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public List<string> To { get; set; }
    }
}
