using Business.Features.Games.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Games.Commands
{

    public class DeleteGamesCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly DeleteGamesCommandHandler _sut;

        public DeleteGamesCommandHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldDeleteGames()
        {
            //arrange
            var command = new DeleteGamesCommand
            {
                Id = Guid.NewGuid()
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

            result.Should().BeTrue();
        }
    }
}
