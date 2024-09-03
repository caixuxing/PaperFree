using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 纸质病例存储管理
    /// </summary>
    public record CaseShelfManagement : FullRoot
    {
        public CaseShelfManagement() { }
        public CaseShelfManagement(string warehouseNumber, string warehouseName, string shelfNo, string storageNumberSegment, string lineNumber,
                                   string numberOlumns, string status, string orgCode, string hospCode, string inputCode)
        {
            WarehouseNumber = warehouseNumber;
            WarehouseName = warehouseName;
            ShelfNo = shelfNo;
            StorageNumberSegment = storageNumberSegment;
            LineNumber = lineNumber;
            NumberOlumns = numberOlumns;
            Status = status;
            OrgCode = orgCode;
            HospCode = hospCode;
            InputCode = inputCode;
        }
        /// <summary>
        /// 修改纸质病例存储管理
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="processCode"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public CaseShelfManagement UpadteCaseShelfManagement(string warehouseNumber, string warehouseName, string shelfNo, string storageNumberSegment, string lineNumber,
                                   string numberOlumns, string status, string orgCode, string hospCode, string inputCode)
        {
            this.WarehouseNumber = warehouseNumber;
            this.WarehouseName = warehouseName;
            this.ShelfNo = shelfNo;
            this.StorageNumberSegment = storageNumberSegment;
            this.LineNumber = lineNumber;
            this.NumberOlumns = numberOlumns;
            this.Status = status;
            this.OrgCode = orgCode;
            this.HospCode = hospCode;
            this.InputCode = inputCode;
            return this;
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WarehouseNumber { get; private set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; private set; }
        /// <summary>
        /// 病架号
        /// </summary>
        public string ShelfNo { get; private set; }
        /// <summary>
        /// 存储号段
        /// </summary>
        public string StorageNumberSegment { get; private set; }
        /// <summary>
        /// 行数
        /// </summary>
        public string LineNumber { get; private set; }
        /// <summary>
        /// 列数
        /// </summary>
        public string NumberOlumns { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 所属院区
        /// </summary>
        public string HospCode { get; private set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; private set; }
    }
}