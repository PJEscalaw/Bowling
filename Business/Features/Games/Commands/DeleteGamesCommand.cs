using Business.Commons.Exceptions;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Commands
{
    public class DeleteGamesCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteGamesCommandHandler : IRequestHandler<DeleteGamesCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGamesCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteGamesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Games.GetByIdAsync(request.Id);
                if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Games), nameof(request.Id), request.Id);
                _unitOfWork.Games.Delete(entity);

               return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
