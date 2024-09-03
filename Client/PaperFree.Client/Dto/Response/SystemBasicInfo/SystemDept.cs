using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Response.SystemBasicInfo
{
    public class SystemDept
    {
        public string id { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string orG_CODE { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string hosP_CODE { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string depT_ID { get; set; }
        public string keywords { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string depT_NAME { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string wB_CODE { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string pY_CODE { get; set; }
        public string enabled { get; set; }
    }
}
