using Business.Commons.Exceptions;
using Business.DTOs.Games.Inputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Commands
{
    public class UpdateGamesCommand : IRequest<bool>, IRegister
    {
        public UpdateGamesDto Dto { get; set; }
        public void Register(TypeAdapterConfig config) => config.ForType<UpdateGamesDto, Domain.Entities.Games>();
    }

    public class UpdateGamesCommandHandler : IRequestHandler<UpdateGamesCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGamesCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(UpdateGamesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Games.GetByIdAsync(request.Dto.Id);
                if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Games), nameof(request.Dto.Id), request.Dto.Id);

                request.Dto.Adapt(entity);
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
