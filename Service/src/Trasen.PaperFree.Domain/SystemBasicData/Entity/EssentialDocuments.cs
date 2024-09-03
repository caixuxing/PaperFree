using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 必传文件配置
    /// </summary>
    public record EssentialDocuments : FullRoot
    {
        public EssentialDocuments() { }
        public EssentialDocuments(string id, string deptCode, string fatherMeumid, string meumType, string status, int orderid, string orgcode, string hospcode, string inptucode)
        {
            Id = id;
            DeptCode = deptCode;
            FatherMeumid = fatherMeumid;
            MeumType = meumType;
            Status = status;
            OrderId = orderid;
            OrgCode = orgcode;
            HospCode = hospcode;
            InputCode = inptucode;
        }

        public EssentialDocuments(string deptCode, string fatherMeumid, string meumType, string status, int? orderId, string orgCode, string hospCode, string inputCode)
        {
            DeptCode = deptCode;
            FatherMeumid = fatherMeumid;
            MeumType = meumType;
            Status = status;
            OrgCode = orgCode;
            HospCode = hospCode;
            InputCode = inputCode;
            OrderId = orderId;
        }

        public EssentialDocuments UpadteEssentialDocuments(string fatherMeumid, string meumType, string status, int? orderid)
        {
            //this.DeptCode = deptCode;
            this.FatherMeumid = fatherMeumid;
            this.MeumType = meumType;
            this.Status = status;
            this.OrderId = orderid;
            return this;
        }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; private set; }
        /// <summary>
        /// 目录编码
        /// </summary>
        public string FatherMeumid { get; private set; }
        /// <summary>
        /// 目录类型（1、目录，2、节点）（当目录里子节点全选时只传父目录信息）
        /// </summary>
        public string MeumType { get; private set; }
        /// <summary>
        /// 状态 状态（0、正常，1、作废）
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; private set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderId { get; private set; }
    }
}