using Business.DTOs.Games.Outputs;
using Business.Features.Games.Queries;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Games.Queries
{
    public class GetGamesQueriesHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly GetGamesByIdQueryHandler _sut1;
        private readonly GetGamesQueryHandler _sut2;

        public GetGamesQueriesHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut1 = new(_mockUnitOfWork.Object);
            _sut2 = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldReturnGamesById()
        {
            var query = new GetGamesByIdQuery
            {
                Id = Guid.NewGuid()
            };
            //arrange
            var entity = new Domain.Entities.Games
            {
                Id = Guid.NewGuid(),
                IsEnded = true,
                Name = "test"
            };
            _mockUnitOfWork.Setup(x => x.Games.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(entity);

            //actual
            var result = await _sut1.Handle(query, new CancellationToken());

            result.Should().NotBeNull();
            result.Should().BeOfType<GetGamesByIdDto>();
            result.IsEnded.Should().BeTrue();
            result.Name.Should().Be("test");
        }

        [Test]
        public async Task ShouldReturnGamesList()
        {
            var entities = new List<Domain.Entities.Games>
            {
                new Domain.Entities.Games { Id = Guid.NewGuid(), Name = "test", IsEnded = true },
                new Domain.Entities.Games { Id = Guid.NewGuid(), Name = "test", IsEnded = true },
                new Domain.Entities.Games { Id = Guid.NewGuid(), Name = "test", IsEnded = true },
            };

            _mockUnitOfWork.Setup(x => x.Games.GetAllAsync()).ReturnsAsync(entities);

            //actual
            var result = await _sut2.Handle(new GetGamesQuery(), new CancellationToken());

            result.Should().NotBeNull();
            result.ToList().ForEach(g =>
            {
                g.Name.Should().Be("test");
                g.IsEnded.Should().BeTrue();
            });
        }
    }
}