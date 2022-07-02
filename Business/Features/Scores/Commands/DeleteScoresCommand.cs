using Business.Commons.Exceptions;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Commands
{
    public class DeleteScoresCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteScoresCommandHandler : IRequestHandler<DeleteScoresCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteScoresCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteScoresCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Scores.GetByIdAsync(request.Id);
                if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Scores), nameof(request.Id), request.Id);
                _unitOfWork.Scores.Delete(entity);

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
