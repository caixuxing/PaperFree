using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo
{
    /// <summary>
    /// 
    /// </summary>
    public record FindInpatientInfoPageListQry : IRequest<PageData<List<InpatientInfoDto>>?>
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string? AdmissId { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputHosCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 入院开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 入院结束时间
        /// </summary>
        public DateTime? EnterDate { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string? EnterDeptCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 设置分页参数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public FindInpatientInfoPageListQry SetPageParm(int pageIndex, int pagesize)
        {
            PageIndex = pageIndex;
            PageSize = pagesize;
            return this;
        }
    }
}