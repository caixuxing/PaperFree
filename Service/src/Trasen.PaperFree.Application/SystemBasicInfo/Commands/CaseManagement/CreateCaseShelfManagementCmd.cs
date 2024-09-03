using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement
{
    /// <summary>
    /// 创建仓库指令
    /// </summary>
    public record CreateCaseShelfManagementCmd : IRequest<string>
    {
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
        /// 机构编码
        /// </summary>
        public string OrgCode {  get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode {  get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode {  set; get; }
        ///// <summary>
        ///// 状态
        ///// </summary>
        //public string Status { get;  set; }
    }

    public class CreateCaseShelfManagementValidate : AbstractValidator<CreateCaseShelfManagementCmd>
    {
        private ICaseShelfManagementRepo _repo;
        private ICurrentUser _currentUser;

        public CreateCaseShelfManagementValidate(ICaseShelfManagementRepo caseShelfManagementRepo, ICurrentUser currentUser)
        {
            this._repo = caseShelfManagementRepo;
            this._currentUser = currentUser;
            RuleFor(x => x.WarehouseName).NotEmpty().WithMessage("仓库名称不能为空！")
            .MaximumLength(20).WithMessage("仓库名称长度不能超过20个字符").Must(IsShelfNameExist).WithMessage("仓库名称不能重复");
            RuleFor(x => x.WarehouseNumber).NotEmpty().WithMessage("仓库编号不能为空！")
            .MaximumLength(20).WithMessage("仓库编号长度不能超过20个字符");
            RuleFor(x => x.ShelfNo).NotEmpty().WithMessage("病架号不能为空！")
             .MaximumLength(20).WithMessage("病架号长度不能超过10个字符");
            RuleFor(x => x.StorageNumberSegment).NotEmpty().WithMessage("存储号段不能为空！")
            .MaximumLength(20).WithMessage("存储号段长度不能超过20个字符");
            RuleFor(x => x.LineNumber).NotEmpty().WithMessage("存储行不能为空！")
            .MaximumLength(10).WithMessage("存储行长度不能超过10个字符");
            RuleFor(x => x.NumberOlumns).NotEmpty().WithMessage("存储列不能为空！")
            .MaximumLength(10).WithMessage("存储列长度不能超过10个字符");
            RuleFor(x => x.OrgCode).MaximumLength(50).WithMessage("机构编码长度不能超过").NotEmpty().WithMessage("机构编码不能为空");
            RuleFor(x => x.HospCode).MaximumLength(50).WithMessage("院区编码长度不能超过").NotEmpty().WithMessage("院区编码不能为空");
            RuleFor(x => x.InputCode).MaximumLength(50).WithMessage("辖区编码长度不能超过").NotEmpty().WithMessage("辖区编码不能为空");
        }

        public bool IsShelfNameExist(CreateCaseShelfManagementCmd createCaseShelf, string name)
        {
            return !_repo.QueryAll().AsNoTracking().Any(x =>
                x.WarehouseName == name &&
                x.OrgCode == _currentUser.OrgCode &&
                x.HospCode == _currentUser.HospCode &&
                x.InputCode == _currentUser.InputCode &&
                x.WarehouseNumber == createCaseShelf.WarehouseNumber &&
                x.StorageNumberSegment == createCaseShelf.StorageNumberSegment);
        }
    }
}