using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.Archive
{

    /// <summary>
    /// 审批详细Dto
    /// </summary>
    public record ArchiveApplyDetailDto
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 是否可驳回指定节点
        /// </summary>
        public bool? IsRejectToNode { get; set; }

        /// <summary>
        /// 流程节点集合
        /// </summary>
        public List<DropSelectDto<string>> NodeList { get; set; } = new();

        /// <summary>
        /// 事件方向分支
        /// </summary>
        public List<DropSelectDto<int>> eventDirectionTypes { get; set; } = new();
    }
}
