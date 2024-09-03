using Microsoft.Extensions.DependencyInjection;
using Trasen.PaperFree.Domain.ArchiveRecord.DomainService;
using Trasen.PaperFree.Domain.ArchiveRecord.DomainService.Impl;
using Trasen.PaperFree.Domain.RecallRecord.DomainService;
using Trasen.PaperFree.Domain.RecallRecord.DomainService.Impl;
using Trasen.PaperFree.Domain.SeedWork;

namespace Trasen.PaperFree.Infrastructure;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddSingleton<IArchiveRecordService, ArchiveRecordService>();

        services.AddSingleton<IRecallRecordService, RecallRecordService>();
    }
}