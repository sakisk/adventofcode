using System.IO;
using AdventOfCode2019.DayThirteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayThirteenTests
{
    public class ArcadeGameTestsWhenGameStarts
    {
        private readonly ArcadeGame _arcadeGame;

        public ArcadeGameTestsWhenGameStarts()
        {
            var gameCode = File.ReadAllText("input");
            _arcadeGame = new ArcadeGame(gameCode);
            _arcadeGame.Start();
        }

        [Fact]
        public void GridShouldNotBeEmpty() => _arcadeGame.Grid.Should().NotBeEmpty();

        [Fact]
        public void GridShouldHaveCorrectNumberOfBlockTiles() => _arcadeGame.Blocks.Should().HaveCount(253);
    }
}