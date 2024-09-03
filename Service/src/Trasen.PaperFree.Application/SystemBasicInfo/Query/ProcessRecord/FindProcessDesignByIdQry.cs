using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;

public record FindProcessDesignByIdQry(string Id) : IRequest<ProcessDesignDetaiDto>;