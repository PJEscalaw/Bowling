using Business.Commons.Exceptions;
using Business.DTOs.Games.Inputs;
using Business.DTOs.Games.Outputs;
using Mapster;
using MediatR;
using Persistence.Repositories.Interfaces;

namespace Business.Features.Games.Commands
{
    public class UpdateGamesCommand : IRequest<UpdateGamesOutputDto>, IRegister
    {
        public UpdateGamesInputDto Dto { get; set; }
        public void Register(TypeAdapterConfig config) => config.ForType<UpdateGamesInputDto, Domain.Entities.Games>();
    }

    public class UpdateGamesCommandHandler : IRequestHandler<UpdateGamesCommand, UpdateGamesOutputDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGamesCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<UpdateGamesOutputDto> Handle(UpdateGamesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Games.GetByIdAsync(request.Dto.Id);
                if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Games), nameof(request.Dto.Id), request.Dto.Id);

                request.Dto.Adapt(entity);
                await _unitOfWork.CommitAsync();

                return request.Dto.Adapt<UpdateGamesOutputDto>();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
