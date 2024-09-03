using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Enums
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete,

        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit,


    }
}
