using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.Shared.Const
{
    /// <summary>
    /// 病案系统Api地址
    /// </summary>
    public class MedicalApiConst
    {
        /// <summary>
        /// 保存归档数据到病案示踪
        /// </summary>
        public const string syncData = "/interfaceApi/syncData";

        /// <summary>
        ///  同步日志
        /// </summary>
        public const string syncLog = "/interfaceApi/syncLog";

        /// <summary>
        ///   同步状态
        /// </summary>
        public const string syncStatus = " /interfaceApi/syncStatus";
    }
}
