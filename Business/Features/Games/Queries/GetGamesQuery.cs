using Business.DTOs.Games.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Queries
{
    public class GetGamesQuery : IRequest<IEnumerable<GetGamesOutputDto>>, IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<Domain.Entities.Games>, IEnumerable<GetGamesOutputDto>>();
        }
    }

    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GetGamesOutputDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGamesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<GetGamesOutputDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Games.GetAllAsync();
            return entities.Adapt<IEnumerable<GetGamesOutputDto>>();
        }
    }
}
