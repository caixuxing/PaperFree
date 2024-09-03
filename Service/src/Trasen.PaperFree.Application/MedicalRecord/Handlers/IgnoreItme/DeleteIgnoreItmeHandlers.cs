using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.IgnoreItme;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.IgnoreItme
{
    public record DeleteIgnoreItmeHandlers : IRequestHandler<DeletegnoreItmeCmd, bool>
    {
        private readonly IIgnoreItmeRepo _ignoreItmeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIgnoreItmeHandlers(IIgnoreItmeRepo ignoreItmeRepo, IUnitOfWork unitOfWork)
        {
            _ignoreItmeRepo = ignoreItmeRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletegnoreItmeCmd request, CancellationToken cancellationToken)
        {
            var entity = await _ignoreItmeRepo.FindById(request.id);
            if (entity == null)
                throw new BusinessException(MessageType.Error, "删除失败！", "当前删除数据不存在无法执行删除操作！");
            entity.ChangeDelete();
            _ignoreItmeRepo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true; 
        }
    }
}
