using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Global
{
   public class Api
    {
        /// <summary>
        /// 登录
        /// </summary>
        public const string Login = "/auth/login";

        /// <summary>
        /// 菜单
        /// </summary>
        public const string menu = "/systembasicinfo/getusermenu";
        /// <summary>
        /// 机构名称
        /// </summary>
        public const string org = "/systembasicinfo/org";
        /// <summary>
        /// 科室名称
        /// </summary>
        public const string dept = "/systembasicinfo/orgdepartment";
        public const string MedicalRecordControllers = "/medicalrecord/querylist";
    }
}
