using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;
using Business.Features.Scores.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Scores.Commands
{
    public class UpdateScoresCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly UpdateScoresCommandHandler _sut;
        public UpdateScoresCommandHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldUpdateGames()
        {
            //arrange
            var command = new UpdateScoresCommand
            {
                Dto = new UpdateScoresInputDto
                {
                    Frame = 1,
                    GameId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    IsSpare = false,
                    IsStrike = true,
                    PinsKnockedDown = 10,
                    RollIndex = 1,
                    Score = 10
                }
            };
            var entity = new Domain.Entities.Scores
            {
                Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10
            };
            _mockUnitOfWork.Setup(x => x.Scores.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut.Handle(command, new CancellationToken());

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UpdateScoresOutputDto>();
        }
    }
}
