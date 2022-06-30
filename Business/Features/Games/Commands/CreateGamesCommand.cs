using Business.DTOs.Games.Inputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Commands
{
    public class CreateGamesCommand : IRequest<Guid>, IRegister
    {
        public CreateGamesDto Dto { get; set; }
        public void Register(TypeAdapterConfig config) => config.ForType<CreateGamesDto, Domain.Entities.Games>();
    }
    public class CreateGamesCommandHandler : IRequestHandler<CreateGamesCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateGamesCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<Guid> Handle(CreateGamesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Dto.Adapt<Domain.Entities.Games>();

                await _unitOfWork.Games.AddAsync(entity);
                await _unitOfWork.CommitAsync();

                return entity.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
