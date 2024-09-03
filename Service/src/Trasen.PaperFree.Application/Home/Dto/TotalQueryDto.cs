using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.Home.Dto
{
    public record TotalQueryDto
    {
        /// <summary>
        /// 出院人数
        /// </summary>
        public int OutNumber {  get; set; }
        /// <summary>
        /// 待签收人数
        /// </summary>
        public int TobesignedNumber {  get; set; }
        /// <summary>
        /// 已签收人数
        /// </summary>
        public int SignedNumber {  get; set; }
        /// <summary>
        /// 待归档人数
        /// </summary>
        public int ArchiveNumber {  get; set; }
        /// <summary>
        /// 召回待审核人数
        /// </summary>
        public int RecallReviewedNumber {  get; set; }
        
        /// <summary>
        /// 已归档人数
        /// </summary>
        public int ArchivedNumber {  get; set; }
    }
}
