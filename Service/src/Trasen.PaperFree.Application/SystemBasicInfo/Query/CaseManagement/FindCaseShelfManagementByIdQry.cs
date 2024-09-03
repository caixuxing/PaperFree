using Trasen.PaperFree.Application.SystemBasicInfo.Dto.CaseManagement;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.CaseManagement;
public record FindCaseShelfManagementByIdQry(string Id) : IRequest<CaseShelfManagementDto>;