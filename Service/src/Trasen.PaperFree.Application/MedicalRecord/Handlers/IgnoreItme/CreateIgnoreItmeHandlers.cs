using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.IgnoreItme;
using Trasen.PaperFree.Domain.IgnoreItme.Entity;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.IgnoreItme
{
    internal class CreateIgnoreItmeHandlers : IRequestHandler<CreateIgnoreItmeCmd, string>
    {
        private IIgnoreItmeRepo _ignoreItmeRepo;
        private Validate<CreateIgnoreItmeCmd> validate;
        private IGuidGenerator guidGenerator;
        private IUnitOfWork unitOfWork;

        public CreateIgnoreItmeHandlers(IIgnoreItmeRepo ignoreItmeRepo, Validate<CreateIgnoreItmeCmd> validate, IGuidGenerator guidGenerator, IUnitOfWork unitOfWork)
        {
            _ignoreItmeRepo = ignoreItmeRepo;
            this.validate = validate;
            this.guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateIgnoreItmeCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new IgnoreItmeTable(guidGenerator.Create().ToString(),request.ArchiveId,request.MeumTreeId);
            await _ignoreItmeRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
