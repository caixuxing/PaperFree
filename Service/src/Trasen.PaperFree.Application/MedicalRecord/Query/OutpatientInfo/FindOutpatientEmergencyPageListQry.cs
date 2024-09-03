using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo
{
    public record FindOutpatientEmergencyPageListQry : IRequest<PageData<List<OutpatientEmergencyDto>?>>
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string? HospRecordId { get; set; }
        /// <summary>
        /// 开始时间（接诊时间）
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间(接诊时间)
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// 接诊科室
        /// </summary>
        public string SeeDeptCode {  get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string IcdName {  get; set; }
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
        /// <returns></returns>
        public FindOutpatientEmergencyPageListQry SetPageParm(int pageIndex, int pagesize)
        {
            PageIndex = pageIndex;
            PageSize = pagesize;
            return this;
        }

    }
}
