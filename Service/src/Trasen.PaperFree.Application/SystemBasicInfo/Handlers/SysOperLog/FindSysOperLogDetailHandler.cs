using Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.SysOperLog;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.SysOperLog
{
    internal class FindSysOperLogDetailHandler : IRequestHandler<FindSysOperLogDetailByIdQry, SysOperLogDetailDto?>
    {
        private readonly ISysOperLogRepo _sysOperLogRepo;
        private readonly IMapper _mapper;

        public FindSysOperLogDetailHandler(ISysOperLogRepo sysOperLogRepo, IMapper mapper)
        {
            _sysOperLogRepo = sysOperLogRepo;
            _mapper = mapper;
        }

        public async Task<SysOperLogDetailDto?> Handle(FindSysOperLogDetailByIdQry request, CancellationToken cancellationToken)
        {
            var dta = await _sysOperLogRepo.FindById(request.id);
            return _mapper.Map<SysOperLogDetailDto>(dta);
        }
    }
}