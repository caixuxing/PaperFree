using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.Home.Dto;

namespace Trasen.PaperFree.Application.Home.Query
{
    public record FindCaseloadQry:IRequest<CaseloadDto>
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
