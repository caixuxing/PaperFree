using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord
{
    /// <summary>
    /// 流程设计详细
    /// </summary>
    public class ProcessDesignDetaiDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        [Required]
        public string ProcessName { get; set; } = string.Empty;
        /// <summary>
        /// 流程模板类型
        /// </summary>
        [Required]
        public ProcessTempType processTempType { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptCode { get; set; } = string.Empty;
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; } = string.Empty;

        /// <summary>
        /// 院区编码
        /// </summary>
        [Required]
        public string HospCode { get; set; } = string.Empty;

        /// <summary>
        /// 流程节点集合
        /// </summary>
        public List<WorkNode> workNodes { get; set; } = new();
    }
}