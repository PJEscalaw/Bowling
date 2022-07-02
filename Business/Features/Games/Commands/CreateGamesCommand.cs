using Business.Commons;
using Business.DTOs.Games.Inputs;
using Business.DTOs.Games.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Commands
{
    public class CreateGamesCommand : IRequest<CreateGamesOutputDto>, IRegister
    {
        public CreateGamesInputDto Dto { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateGamesInputDto, Domain.Entities.Games>();
            config.ForType<Domain.Entities.Games, CreateGamesOutputDto>();
        }
    }
    public class CreateGamesCommandHandler : IRequestHandler<CreateGamesCommand, CreateGamesOutputDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateGamesCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<CreateGamesOutputDto> Handle(CreateGamesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var game = await _unitOfWork.Games.GetGamesByNameAsync(request.Dto.Name, cancellationToken);
                if (game is not null) throw new ResponseException($"{request.Dto.Name} already exists from the database.");

                var entity = request.Dto.Adapt<Domain.Entities.Games>();

                await _unitOfWork.Games.AddAsync(entity);
                await _unitOfWork.CommitAsync();

                return entity.Adapt<CreateGamesOutputDto>();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
