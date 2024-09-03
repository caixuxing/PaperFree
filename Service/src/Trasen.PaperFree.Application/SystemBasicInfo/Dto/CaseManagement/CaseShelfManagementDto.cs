using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.CaseManagement
{
    public class CaseShelfManagementDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WarehouseNumber { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 病架号
        /// </summary>
        public string ShelfNo { get; set; }

        /// <summary>
        /// 存储号段
        /// </summary>
        public string StorageNumberSegment { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public string LineNumber { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public string NumberOlumns { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; set; }

        public virtual ICollection<CaseShelfManagement> CaseManagement { get; set; }
    }
}