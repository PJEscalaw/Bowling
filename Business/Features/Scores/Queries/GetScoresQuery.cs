using Business.DTOs.Scores.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Queries
{
    public class GetScoresQuery : IRequest<IEnumerable<GetScoresOutputDto>>, IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Entities.Scores, GetScoresOutputDto>();
        }
    }

    public class GetScoresQueryHandler : IRequestHandler<GetScoresQuery, IEnumerable<GetScoresOutputDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetScoresQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<IEnumerable<GetScoresOutputDto>> Handle(GetScoresQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Scores.GetAllAsync();
            return entity.Adapt<IEnumerable<GetScoresOutputDto>>();
        }
    }
}
