using Business.DTOs.Scores.Outputs;
using Business.Features.Scores.Queries;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;

namespace Unit.Features.Scores.Queries
{
    public class GetScoresQueriesHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly GetScoresByIdQueryHandler _sut1;
        private readonly GetScoresByGameIdQueryHandler _sut2;
        private readonly GetScoresQueryHandler _sut3;

        public GetScoresQueriesHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut1 = new(_mockUnitOfWork.Object);
            _sut2 = new(_mockUnitOfWork.Object);
            _sut3 = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldReturnScoresById()
        {
            var query = new GetScoresByIdQuery
            {
                Id = Guid.NewGuid()
            };
            //arrange
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

            //actual
            var result = await _sut1.Handle(query, new CancellationToken());

            result.Should().NotBeNull();
            result.Should().BeOfType<GetScoresByIdOutputDto>();
        }

        [Test]
        public async Task ShouldReturnScoresList()
        {
            var entities = new List<Domain.Entities.Scores>
            {
                new Domain.Entities.Scores {
                    Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10 },
                new Domain.Entities.Scores {  Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10},
                new Domain.Entities.Scores { Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10},
            };
            _mockUnitOfWork.Setup(x => x.Scores.GetAllAsync()).ReturnsAsync(entities);

            //actual
            var result = await _sut3.Handle(new GetScoresQuery(), new CancellationToken());

            result.Should().NotBeNull();
            result.ToList().ForEach(g =>
            {
                g.Frame.Should().Be(1);
                g.IsSpare.Should().BeFalse();
            });
        }

        [Test]
        public async Task ShouldReturnScoresGameById()
        {
            //arrange
            var query = new GetScoresByGameIdQuery
            {
                GameId = Guid.NewGuid()
            };
            var entities = new List<Domain.Entities.Scores>
            {
                new Domain.Entities.Scores {
                    Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10 },
                new Domain.Entities.Scores {  Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10},
                new Domain.Entities.Scores { Frame = 1,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsSpare = false,
                IsStrike = true,
                PinsKnockedDown = 10,
                RollIndex = 1,
                Score = 10},
            };
            _mockUnitOfWork.Setup(x => x.Scores.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(entities);

            //actual
            var result = await _sut2.Handle(query, new CancellationToken());

            result.Should().NotBeNull();
            result.ToList().ForEach(s =>
            {
                s.Frame.Should().Be(1);
                s.Score.Should().Be(10);
                s.RollIndex.Should().Be(1);
            });
        }
    }
}
