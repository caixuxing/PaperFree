using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    public record DeptMeMenuTreeEntity : FullRoot
    {
        private DeptMeMenuTreeEntity() { }
        public DeptMeMenuTreeEntity(string id, string deptid, string archivermeumid, string parentid, WhetherType isrequired, string orgcode, string hospcode, string inptucode)
        {
            Id = id;
            DeptId = deptid;
            ArchiverMeumId = archivermeumid;
            ParentId = parentid;
            IsRequired = isrequired;
            OrgCode = orgcode;
            HospCode = hospcode;
            InputCode = inptucode;
        }

        public DeptMeMenuTreeEntity(string deptid, string archivermeumid, string parentid, WhetherType isrequired, string orgCode, string hospCode, string inputCode)
        {
            DeptId = deptid;
            ArchiverMeumId = archivermeumid;
            ParentId = parentid;
            IsRequired = isrequired;
            OrgCode = orgCode;
            HospCode = hospCode;
            InputCode = inputCode;
        }

        public DeptMeMenuTreeEntity UpadteMeumTree(string archivermeumid, WhetherType isrequired, string parentId)
        {
            this.ArchiverMeumId = archivermeumid;
            this.IsRequired = isrequired;
            this.ParentId = parentId;
            return this;
        }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptId { get; private set; }
        /// <summary>
        /// 归档目录ID
        /// </summary>
        public string ArchiverMeumId { get; private set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string? ParentId { get; private set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public WhetherType IsRequired { get; private set; }
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
    }
}