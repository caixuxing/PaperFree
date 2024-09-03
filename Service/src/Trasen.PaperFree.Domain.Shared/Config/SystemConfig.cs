using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.Shared.Config
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemConfig
    {
        public bool IsSwagger { get; set; }

        public bool IsSqlLogConsole { get; set; }
        public bool IsMailAlarm { get; set; }
    }
}
