using Business.Features.Scores.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit.Features.Scores.Commands
{
    public class DeleteScoresCommandsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly DeleteScoresByGameIdCommandHandler _sut;
        private readonly DeleteScoresCommandHandler _sut2;

        public DeleteScoresCommandsHandlerTests()
        {
            _mockUnitOfWork = new();

            _sut = new(_mockUnitOfWork.Object);
            _sut2 = new(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task ShouldDeleteScoresByGameIdTests()
        {
            //arrange
            var command = new DeleteScoresByGameIdCommand
            {
                GameId = Guid.NewGuid()
            };

            var entities = new List<Domain.Entities.Scores>
            {
               new Domain.Entities.Scores
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

            _mockUnitOfWork.Setup(x => x.Scores.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(entities.AsEnumerable());
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut.Handle(command, new CancellationToken());

            //assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ShouldDeleteScoresTests()
        {
            //arrange
            var command = new DeleteScoresCommand
            {
                Id = Guid.NewGuid()
            };

            var entities = new Domain.Entities.Scores
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

            _mockUnitOfWork.Setup(x => x.Scores.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(entities);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //actual
            var result = await _sut2.Handle(command, new CancellationToken());

            //assert
            result.Should().BeTrue();
        }
    }
}
