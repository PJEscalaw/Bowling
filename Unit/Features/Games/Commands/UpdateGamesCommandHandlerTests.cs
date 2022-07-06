using Business.DTOs.Games.Inputs;
using Business.DTOs.Games.Outputs;
using Business.Features.Games.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Games.Commands
{
    public class UpdateGamesCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly UpdateGamesCommandHandler _sut;
        public UpdateGamesCommandHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldUpdateGames()
        {
            //arrange
            var command = new UpdateGamesCommand
            {
                Dto = new UpdateGamesInputDto
                {
                    Id = Guid.NewGuid(),
                    IsEnded = true,
                    Name = "test"
                }
            };
            var entity = new Domain.Entities.Games
            {
                Id = Guid.NewGuid(),
                IsEnded = true,
                Name = "test"
            };
            _mockUnitOfWork.Setup(x => x.Games.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut.Handle(command, new CancellationToken());

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UpdateGamesOutputDto>();
            result.Name.Should().Be("test");
            result.IsEnded.Should().BeTrue();
        }
    }
}
