using System.IO;
using AdventOfCode2019.DayThirteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayThirteenTests
{
    public class GivenAGameStartedForFreeTestWhenPlays
    {
        private readonly int _score;

        public GivenAGameStartedForFreeTestWhenPlays()
        {
            var gameCode = File.ReadAllText("input");
            var arcadeGame = new ArcadeGame(gameCode);
            arcadeGame.Start(quarters: 2);
            
            _score = arcadeGame.Play();
        }

        [Fact]
        public void ShouldHaveCorrectScoreAfterAllBlockDestroyed() => _score.Should().Be(12263);
    }
}