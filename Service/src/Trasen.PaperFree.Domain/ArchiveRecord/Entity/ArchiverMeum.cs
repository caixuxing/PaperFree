using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Entity
{
    /// <summary>
    /// 归档菜单目录
    /// </summary>
    public record ArchiverMeum : FullRoot
    {
        private ArchiverMeum() { }
        /// <summary>
        /// 新增
        /// </summary>
        public ArchiverMeum(string id, string menuname, string parentid, string permission,
        string meumtype, int orderby, string secretlevel, WhetherType isallor, WhetherType ishighshots, WhetherType issignature, string orgcode, string hospcode)
        {
            Id = id;
            MenuName = menuname;
            ParentId = parentid;
            Permission = permission;
            MeumType = meumtype;
            Orderby = orderby;
            SecretLevel = secretlevel;
            IsAllorg = isallor;
            IsHighShots = ishighshots;
            IsSignature = issignature;
            OrgCode = orgcode;
            HospCode = hospcode;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public ArchiverMeum ChangeArchiverMeum(string menuname, string parentid, string permission,
        string meumtype, int orderby, string secretlevel, WhetherType isallor, WhetherType ishighshots, WhetherType issignature)
        {
            this.MenuName = menuname;
            this.ParentId = parentid;
            this.Permission = permission;
            this.MeumType = meumtype;
            this.Orderby = orderby;
            this.SecretLevel = secretlevel;
            this.IsHighShots = ishighshots;
            this.IsSignature = issignature;
            this.IsAllorg = isallor;
            return this;
        }
        public List<ArchiverMeum> ChangeArchiverMeumList(List<ArchiverMeum> list)
        {
            List<ArchiverMeum> list1 = new();
            foreach (var item in list)
            {
                this.MenuName = item.MenuName;
                this.ParentId = item.ParentId;
                this.Permission = item.Permission;
                this.MeumType = item.MeumType;
                this.Orderby = item.Orderby;
                this.SecretLevel = item.SecretLevel;
                this.IsHighShots = item.IsHighShots;
                this.IsSignature = item.IsSignature;
                this.IsAllorg = item.IsAllorg;
                list1.Add(this);
            }

            return list1;
        }
        /// <summary>
        /// 目录名称
        /// </summary>
        public string MenuName { get; private set; }
        /// <summary>
        /// 上级目录ID
        /// </summary>
        public string? ParentId { get; private set; }
        /// <summary>
        /// 权限等级 根据权限设置医护所能操作的目录
        /// </summary>
        public string Permission { get; private set; }
        /// <summary>
        /// 目录类型 目录类型（1、目录，2、节点）
        /// </summary>
        public string? MeumType { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Orderby { get; private set; }
        /// <summary>
        /// 保密等级
        /// </summary>
        public string? SecretLevel { get; private set; }
        /// <summary>
        /// 是否高拍
        /// </summary>
        public WhetherType IsHighShots { get; private set; }

        /// <summary>
        /// 是否签名
        /// </summary>
        public WhetherType IsSignature { get; private set; }
        /// <summary>
        /// 是否适用所有机构 是否适用所有机构（0、否；1、是）
        /// </summary>
        public WhetherType IsAllorg { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; private set; }
    }
}