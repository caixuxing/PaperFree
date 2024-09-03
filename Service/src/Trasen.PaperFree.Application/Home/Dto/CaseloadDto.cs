using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.Home.Dto
{
    /// <summary>
    /// 工作量统计
    /// </summary>
    public record CaseloadDto
    {

        /// <summary>
        /// 待签收人数
        /// </summary>
        public int TobesignedNumber { get; set; }
        /// <summary>
        /// 已签收人数
        /// </summary>
        public int SignedNumber { get; set; }
        /// <summary>
        /// 签收率
        /// </summary>
        public double ReceiptedRate { get; set; }


        /// <summary>
        /// 待归档人数
        /// </summary>
        public int TreatArchiveNumber { get; set; }
        /// <summary>
        /// 已归档人数
        /// </summary>
        public int ArchiveNumber { get; set; }
        /// <summary>
        /// 归档率
        /// </summary>
        public double ArchivedRate { get; set; }
 
        /// <summary>
        /// 借阅待审核人数
        /// </summary>
        public int BorrowingNumber { get; set; }
        /// <summary>
        /// 借阅已审核人数
        /// </summary>
        public int HasBorrowingNumber { get; set; }
        /// <summary>
        /// 审核率
        /// </summary>
        public double AuditBorrowingRate { get; set; }
 
        /// <summary>
        /// 复印待审核人数
        /// </summary>
        public int CopyNumber { get; set; }
        /// <summary>
        /// 复印已审核人数
        /// </summary>
        public int HasCopyNumber { get; set; }
        /// <summary>
        /// 审核率
        /// </summary>
        public double CopyAuditRate { get; set; }
    }

}
