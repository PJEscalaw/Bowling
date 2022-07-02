using Business.Commons.Exceptions;
using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Commands
{
    public class UpdateScoresCommand : IRequest<UpdateScoresOutputDto>, IRegister
    {
        public UpdateScoresInputDto Dto { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<UpdateScoresInputDto, Domain.Entities.Scores>();
            config.ForType<Domain.Entities.Scores, UpdateScoresOutputDto>();
        }
    }

    public class UpdateScoresCommandHandler : IRequestHandler<UpdateScoresCommand, UpdateScoresOutputDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateScoresCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<UpdateScoresOutputDto> Handle(UpdateScoresCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Scores.GetByIdAsync(request.Dto.Id);
                if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Scores), nameof(request.Dto.Id), request.Dto.Id);

                request.Dto.Adapt(entity);
                await _unitOfWork.CommitAsync();

                return request.Dto.Adapt<UpdateScoresOutputDto>();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
