using Business.DTOs.Scores.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Queries
{
    public class GetScoresByGameIdQuery : IRequest<IEnumerable<GetScoresByGameIdOutputDto>>, IRegister
    {
        public Guid GameId { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<Domain.Entities.Scores>, IEnumerable<GetScoresByGameIdOutputDto>>();
        }
    }

    public class GetScoresByGameIdQueryHandler : IRequestHandler<GetScoresByGameIdQuery, IEnumerable<GetScoresByGameIdOutputDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetScoresByGameIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<GetScoresByGameIdOutputDto>> Handle(GetScoresByGameIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Scores.GetByGameIdAsync(request.GameId);
            return entities.Adapt<IEnumerable<GetScoresByGameIdOutputDto>>();
        }
    }
}
