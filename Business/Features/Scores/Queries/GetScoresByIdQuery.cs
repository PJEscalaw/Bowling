using Business.Commons.Exceptions;
using Business.DTOs.Scores.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Queries
{
    public class GetScoresByIdQuery : IRequest<GetScoresByIdOutputDto>, IRegister
    {
        public Guid Id { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Entities.Scores, GetScoresByIdOutputDto>();
        }
    }

    public class GetScoresByIdQueryHandler : IRequestHandler<GetScoresByIdQuery, GetScoresByIdOutputDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetScoresByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<GetScoresByIdOutputDto> Handle(GetScoresByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Scores.GetByIdAsync(request.Id);
            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Scores), nameof(request.Id), request.Id);

            return entity.Adapt<GetScoresByIdOutputDto>();
        }
    }
}
