using Business.Commons.Exceptions;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Commands
{
    public class DeleteScoresByGameIdCommand : IRequest<bool>
    {
        public Guid GameId { get; set; }
    }

    public class DeleteScoresByGameIdCommandHandler : IRequestHandler<DeleteScoresByGameIdCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteScoresByGameIdCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteScoresByGameIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _unitOfWork.Scores.GetByGameIdAsync(request.GameId);
                if (entities == null) throw new NotFoundException(nameof(Domain.Entities.Scores), nameof(request.GameId), request.GameId);

                _unitOfWork.Scores.DeleteList(entities);
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
