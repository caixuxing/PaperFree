using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Response.SystemBasicInfo
{
    public class SystemDictHospital
    {
        public string id { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string orG_CODE { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string orG_NAME { get; set; }
        /// <summary>
        /// 上级机构
        /// </summary>
        public string parenT_CODE { get; set; }
        /// <summary>
        /// 0机构1院区
        /// </summary>
        public string orG_TYPE { get; set; }
        /// <summary>
        /// 机构简称
        /// </summary>
        public string shorT_NAME { get; set; }
        /// <summary>
        /// 机构级别
        /// </summary>
        public string orG_LEVEL { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string pinyincode { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string fivecode { get; set; }
        /// <summary>
        /// 所属省
        /// </summary>
        public string attributionprovince { get; set; }
        /// <summary>
        /// 所属市州
        /// </summary>
        public string attributioncity { get; set; }

    }
}
