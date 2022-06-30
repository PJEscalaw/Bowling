using Business.Commons.Exceptions;
using Business.DTOs.Games.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Queries
{
    public class GetGamesByIdQuery : IRequest<GetGamesByIdDto>, IRegister
    {
        public Guid Id { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Entities.Games, GetGamesByIdDto>();
        }
    }

    public class GetGamesByIdQueryHandler : IRequestHandler<GetGamesByIdQuery, GetGamesByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGamesByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetGamesByIdDto> Handle(GetGamesByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Games.GetByIdAsync(request.Id);
            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Games), nameof(request.Id), request.Id);

            return entity.Adapt<GetGamesByIdDto>();
        }
    }
}
