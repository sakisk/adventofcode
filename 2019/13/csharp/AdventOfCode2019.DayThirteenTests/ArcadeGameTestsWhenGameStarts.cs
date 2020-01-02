using System.IO;
using System.Linq;
using AdventOfCode2019.DayThirteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayThirteenTests
{
    public class ArcadeGameTestsWhenNewGame
    {
        private readonly ArcadeGame _arcadeGame;

        public ArcadeGameTestsWhenNewGame()
        {
            var gameCode = File.ReadAllText("input");
            _arcadeGame = new ArcadeGame(gameCode);
        }

        [Fact]
        public void ShouldRenderCorrectNumberOfBlocks() => _arcadeGame.RenderArtifacts().Count(x => x.Id == 2).Should().Be(253);
    }
}