using System.IO;
using System.Linq;
using AdventOfCode2019.DayThirteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayThirteenTests
{
    public class ArcadeGameTestsWhenGameStartsForFree
    {
        private readonly ArcadeGame _arcadeGame;

        public ArcadeGameTestsWhenGameStartsForFree()
        {
            var gameCode = File.ReadAllText("input");
            _arcadeGame = new ArcadeGame(gameCode);
            _arcadeGame.Start(quarters: 2);
        }

        [Fact]
        public void BallShouldBeAtInitialPosition() => _arcadeGame.RenderArtifacts().First(x => x.Id == 4).Should().Be((17, 16, 4));

        [Fact]
        public void PaddleShouldBeAtInitialPosition() => _arcadeGame.RenderArtifacts().First(x => x.Id == 3).Should().Be((19, 19, 3));
        
        [Fact]
        public void ScoreShouldBeZero() => _arcadeGame.RenderArtifacts().First(x => x.X == -1 && x.Y == 0).Id.Should().Be(0);
    }
}