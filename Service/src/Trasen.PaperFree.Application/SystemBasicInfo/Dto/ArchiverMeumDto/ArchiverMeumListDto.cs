using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.ArchiverMeumDto
{
    public class ArchiverMeumListDto
    {
        public string ID { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 上级目录ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 权限等级 根据权限设置医护所能操作的目录
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// 目录类型 目录类型（1、目录，2、节点）
        /// </summary>
        public string MeumType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Orderby { get; set; }

        /// <summary>
        /// 保密等级
        /// </summary>
        public string SecretLevel { get; set; }

        /// <summary>
        /// 是否适用所有机构 是否适用所有机构（0、否；1、是）
        /// </summary>
        public WhetherType IsAllorg { get; set; }

        /// <summary>
        /// 是否高拍
        /// </summary>
        public WhetherType IsHighShots { get; set; }

        /// <summary>
        /// 是否签名
        /// </summary>
        public WhetherType IsSignature { get; set; }

        public List<ArchiverMeumListDto> Children { get; set; } = new List<ArchiverMeumListDto>();
    }
}