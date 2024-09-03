using FluentValidation;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Domain.Shared.Validate;

/// <summary>
/// 验证
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public class Validate<TRequest>
{
    private readonly IValidator<TRequest> _validators;

    public Validate(IValidator<TRequest> validators) => _validators = validators;

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public virtual async Task ValidateAsync(TRequest request)
    {
        var validateResult = await _validators.ValidateAsync(request);
        if (!validateResult.IsValid)
        {
            var eorrList = validateResult.Errors.Select(_ => new { key = _.PropertyName, msg = _.ErrorMessage }).ToList();
            throw new BusinessException(MessageType.Warn, "参数校验失败", string.Empty, eorrList, ResultCode.PARAM_ERROR,true);
        }
    }
}