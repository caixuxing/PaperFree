using Trasen.PaperFree.Domain.Shared.Enums;

namespace Trasen.PaperFree.Infrastructure.SeedWork
{
    public class AbpSequentialGuidGeneratorOptions
    {
        //
        // 摘要:
        //     Default value: null (unspecified). Use Volo.Abp.Guids.AbpSequentialGuidGeneratorOptions.GetDefaultSequentialGuidType
        //     method to get the value on use, since it fall backs to a default value.
        public SequentialGuidType? DefaultSequentialGuidType { get; set; }

        //
        // 摘要:
        //     Get the Volo.Abp.Guids.AbpSequentialGuidGeneratorOptions.DefaultSequentialGuidType
        //     value or returns Volo.Abp.Guids.SequentialGuidType.SequentialAtEnd if Volo.Abp.Guids.AbpSequentialGuidGeneratorOptions.DefaultSequentialGuidType
        //     was null.
        public SequentialGuidType GetDefaultSequentialGuidType()
        {
            return DefaultSequentialGuidType ?? SequentialGuidType.SequentialAtEnd;
        }
    }
}