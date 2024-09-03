using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.MedicalRecord.Entity
{
    public record AnNotAtionTable : FullRoot
    {
        private AnNotAtionTable() { }
        public AnNotAtionTable(string archiveid, string fileId, string anNotAtIOn, string deptcode, string lower, DateTime? anNotAtIOnDate, string remark, string orgcode, string hospcode)
        {
            Archiveid = archiveid;
            FileId = fileId;
            AnNotAtIOn = anNotAtIOn;
            Deptcode = deptcode;
            Lower = lower;
            AnNotAtIOnDate = anNotAtIOnDate;
            Remark = remark;
            OrgCode = orgcode;
            HospCode = hospcode;
        }

        public AnNotAtionTable UpadteAnNotAtionTable(string anNotAtIOn, string lower, DateTime? anNotAtIOnDate, string remark)
        {
            AnNotAtIOn = anNotAtIOn;
            Lower = lower;
            AnNotAtIOnDate = anNotAtIOnDate;
            Remark = remark;
            return this;
        }
        /// <summary>
        /// 档案号 档案号
        /// </summary>
        public string Archiveid { get; init; }
        /// <summary>
        /// 文件id 文件id
        /// </summary>
        public string FileId { get; init; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; init; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; init; }
        //批注内容 批注内容
        public string? AnNotAtIOn { get; private set; }

        //批注科室编码 批注科室编码
        public string Deptcode { get; private set; }

        //备注 备注
        public string? Remark { get; private set; }

        /// <summary>
        /// 评分 评分
        /// </summary>
        public string? Lower { get; private set; }
        /// <summary>
        /// 批注日期 批注日期
        /// </summary>
        public DateTime? AnNotAtIOnDate { get; private set; }
    }
}