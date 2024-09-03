namespace Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;

/// <summary>
/// 病历归档申请Cmd
/// </summary>
/// <param name="ArchivalId">档案号ID</param>
public record CreateArchiveApplyCmd(string ArchivalId) : IRequest<bool>;

/// <summary>
/// 验证规则
/// </summary>
public class CreateArchiveApplyValidate : AbstractValidator<CreateArchiveApplyCmd>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public CreateArchiveApplyValidate()
    {
        RuleFor(x => x.ArchivalId).NotEmpty().WithMessage("档案号不能为空！");
    }
}