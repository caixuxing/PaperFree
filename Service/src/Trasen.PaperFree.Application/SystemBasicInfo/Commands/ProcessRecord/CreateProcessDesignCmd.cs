using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;

/// <summary>
/// 创建流程设计指令
/// </summary>
public record CreateProcessDesignCmd : BaseOrgInput, IRequest<string>
{
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
}

/// <summary>
/// 创建流程设计指令校验
/// </summary>
public class CreateMedicalRecorValidate : AbstractValidator<CreateProcessDesignCmd>
{
    private readonly IProcessDesignRepo processDesignRepo;
    private readonly ICurrentUser currentUser;

    /// <summary>
    ///验证规则
    /// </summary>
    public CreateMedicalRecorValidate(IProcessDesignRepo processDesignRepo, ICurrentUser currentUser)
    {
        this.processDesignRepo = processDesignRepo;
        this.currentUser = currentUser;
        RuleFor(x => x.ProcessName).NotEmpty().WithMessage("流程名称不能为空！")
            .MaximumLength(20).WithMessage("流程名称长度不能超过20个字符")
            .Must(IsChinese).WithMessage($"必须是中文");
        RuleFor(x => x.processTempType).IsInEnum().WithMessage("无效的流程模板类型值");
        RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！")
          .MaximumLength(20).WithMessage("科室编码长度不能超过20个字符");
        RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！")
         .MaximumLength(20).WithMessage("机构编码长度不能超过20个字符");
        RuleFor(x => x.HospCode).NotEmpty().WithMessage("院区编码不能为空！")
         .MaximumLength(20).WithMessage("院区编码长度不能超过20个字符");
    }



    /// <summary>
    /// 是否为中文
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public bool IsChinese( string val)
    {
        return new Regex(@"^[\u4e00-\u9fa5]+$").IsMatch(val);
    }

    /// <summary>
    /// 判断流程是否存在
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool IsProcessExist(CreateProcessDesignCmd cmd, string code)
    {
        return !processDesignRepo.QueryAll().AsNoTracking().Any(_ =>
        _.ProcessCode == code &&
        _.OrgCode == cmd.OrgCode &&
        _.HospCode == cmd.HospCode);
    }

    /// <summary>
    /// 同一种类型【机构、院区、流程类型】模板不能同时开启
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool IsProcess(CreateProcessDesignCmd cmd, string code)
    {
        if (!cmd.IsEnable) return true;
        var falg = (processDesignRepo.QueryAll().AsNoTracking().Count(_ =>
        _.ProcessTempType == cmd.processTempType &&
        _.OrgCode == cmd.OrgCode &&
        _.HospCode == cmd.HospCode &&
        _.DeptCode == cmd.DeptCode &&
        _.IsEnable == true) >= 1);
        return !falg;
    }
}