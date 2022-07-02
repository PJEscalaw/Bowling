using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Scores.Commands
{
    public class CreateScoresCommand : IRequest<CreateScoresOutputDto>, IRegister
    {
        public CreateScoresInputDto Dto { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateScoresInputDto, Domain.Entities.Scores>();
            config.ForType<Domain.Entities.Scores, CreateScoresOutputDto>();
        }
    }

    public class CreateScoresCommandHandler : IRequestHandler<CreateScoresCommand, CreateScoresOutputDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateScoresCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<CreateScoresOutputDto> Handle(CreateScoresCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Dto.Adapt<Domain.Entities.Scores>();
                await _unitOfWork.Scores.AddAsync(entity);
                await _unitOfWork.CommitAsync();

                return entity.Adapt<CreateScoresOutputDto>();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
