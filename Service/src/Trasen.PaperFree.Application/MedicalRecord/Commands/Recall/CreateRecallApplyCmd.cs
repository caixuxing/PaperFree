using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;

/// <summary>
/// 病历召回申请Cmd
/// </summary>
public record CreateRecallApplyCmd(string ArchivalId) : IRequest<bool>;

/// <summary>
/// 验证规则
/// </summary>
public class CreateRecallApplyValidate : AbstractValidator<CreateRecallApplyCmd>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public CreateRecallApplyValidate()
    {
        RuleFor(x => x.ArchivalId).NotEmpty().WithMessage("档案号ID不能为空！");
    }
}