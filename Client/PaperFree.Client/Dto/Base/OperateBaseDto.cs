using PaperFree.Client.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Base
{
    public class OperateBaseDto
    {

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string FormTitle { get; set; }

        /// <summary>
        ///操作类型
        /// </summary>
        public OperateType OperateType { get; set; }
    }
}
