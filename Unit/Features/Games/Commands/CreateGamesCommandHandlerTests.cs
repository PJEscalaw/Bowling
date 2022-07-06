using Business.DTOs.Games.Inputs;
using Business.Features.Games.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Games.Commands
{
    public class CreateGamesCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly CreateGamesCommandHandler _sut;
        public CreateGamesCommandHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldCreateGames()
        {
            //arrange
            var command = new CreateGamesCommand
            {
                Dto = new CreateGamesInputDto
                {
                    Name = "test"
                }
            };
            _mockUnitOfWork.Setup(x => x.Games.GetGamesByNameAsync(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(await Task.FromResult<Domain.Entities.Games>(null!));
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut.Handle(command, new CancellationToken());

            //assert
            result.Should().NotBeNull();
            result.Name.Should().Be("test");
        }
    }
}
