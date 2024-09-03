using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.Shared.Enums
{
    /// <summary>
    /// 文件类型枚举类
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 视频文件
        /// </summary>
        VIDEO,
        /// <summary>
        /// 文档文件
        /// </summary>
        DOCUMENT,
        /// <summary>
        /// 图片文件
        /// </summary>
        IMAGE,
        /// <summary>
        /// PDF
        /// </summary>
        PDF,
        /// <summary>
        ///  // dll文件  
        /// </summary>
        DLLFile,
        /// <summary>
        /// 其他类型文件
        /// </summary>
        OTHER
    }
}
