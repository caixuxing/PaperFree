using Trasen.PaperFree.Application.MedicalRecord.Dto.FileTable;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails
{
    public record PatientDetailsDto
    {
        public string ArchiveId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime OutDate { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string Admiss_Id {  get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public WorkFlowState Status { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName {  get; set; }
        public List<PatientBaseWatermarkDto> WatermarkDataDto{ get; set; }=new List<PatientBaseWatermarkDto>();
        public List<DeptMenuTreeFileListDto>  deptMenuTreeFileListDtos { get; set; }=new List<DeptMenuTreeFileListDto>();
    }
}