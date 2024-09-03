using Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Application.SystemBasicInfo.AutoMapConfig
{
    public class SysOperLogMapConfig : Profile
    {
        public SysOperLogMapConfig()
        {
            CreateMap<SysOperLog, SysOperLogDetailDto>()
                 .ForMember(destination => destination.BusinessTypeName, map => map.MapFrom(source => source.BusinessType.ToDescription()))
                 .ForMember(destination => destination.OperatorTypeName, map => map.MapFrom(source => source.OperatorType.ToDescription()))
                 .ForMember(destination => destination.StatusName, map => map.MapFrom(source => source.Status.ToDescription()))
                .ReverseMap();
        }
    }
}