using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;
using Business.Features.Scores.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Scores.Commands
{
    public class CreateScoresCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly CreateScoresCommandHandler _sut;

        public CreateScoresCommandHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldCreateScores()
        {
            //arrange
            var command = new CreateScoresCommand
            {
                Dto = new CreateScoresInputDto
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

            _mockUnitOfWork.Setup(x => x.Scores.AddAsync(It.IsAny<Domain.Entities.Scores>()));
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut.Handle(command, new CancellationToken());

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreateScoresOutputDto>();
            result.IsSpare.Should().BeFalse();
            result.IsStrike.Should().BeTrue();
            result.RollIndex.Should().Be(1);
            result.Score.Should().Be(10);
        }
    }
}
