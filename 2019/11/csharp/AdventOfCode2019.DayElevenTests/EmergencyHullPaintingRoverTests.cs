using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2019.DayEleven;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayElevenTests
{
    public class EmergencyHullPaintingRoverTests
    {
        [Fact]
        public void ShouldStartAtInitialState()
        {
            var rover = new EmergencyHullPaintingRover();
            rover.Position.Should().Be((0, 0));
            rover.Direction.Should().Be('^');
            rover.PanelsDrawn.Should().BeEmpty();
        }

        [Fact]
        public void ShouldCameraShowBlackAtStart() => new EmergencyHullPaintingRover().Camera().Should().Be(0L);

        [Fact]
        public void GivenAPanelNotPaintedBefore_WhenCamera_ThenShowsBlack() =>
            new EmergencyHullPaintingRover((1, 0), '<', new List<(char Color, (int X, int Y))>()
            {
                ('.',(0,0))
            }).Camera().Should().Be(0L);

        [Fact]
        public void GivenAPanelPaintedBefore_WhenCamera_ThenShowsColorPaintedLast() =>
            new EmergencyHullPaintingRover((1, 0), '<', new List<(char Color, (int X, int Y))>
            {
                ('#',(1,0)), ('.', (1,0)), ('#',(1,0))

            }).Camera().Should().Be(1L);

        [Theory]
        [InlineData(0, '.')]
        [InlineData(1, '#')]
        public void GivenRoverJustStarted_WhenPaints_ThenRoverInCorrectState(int color, char newColor)
        {
            var rover = new EmergencyHullPaintingRover();

            rover.Paint(color);

            rover.PanelsDrawn.Last().Color.Should().Be(newColor);
        }

        [Theory]
        [InlineData(0, -1, 0, '<')]
        [InlineData(1, 1, 0, '>')]
        public void GivenRoverJustStarted_WhenTurns_TheRoverInCorrectState(int turn, int x, int y, char newDirection)
        {
            var rover = new EmergencyHullPaintingRover();

            rover.Turn(turn);

            rover.Direction.Should().Be(newDirection);
            rover.Position.Should().Be((x, y));
        }

        [Theory]
        [InlineData(0, 0, '^', 0, -1, 0, '<')]
        [InlineData(0, 0, '^', 1, 1, 0, '>')]
        [InlineData(0, 0, '<', 0, 0, -1, 'v')]
        [InlineData(0, 0, '<', 1, 0, 1, '^')]
        [InlineData(0, 0, 'v', 0, 1, 0, '>')]
        [InlineData(0, 0, 'v', 1, -1, 0, '<')]
        [InlineData(0, 0, '>', 0, 0, 1, '^')]
        [InlineData(0, 0, '>', 1, 0, -1, 'v')]
        public void GivenRoverIsInPosition_WhenTurning_ThenNewPositionAndDirectionAreCorrect(int x, int y, char direction, int turn, int newX, int newY, char newDirection)
        {
            var rover = new EmergencyHullPaintingRover((x, y), direction);

            rover.Turn(turn);

            rover.Position.Should().Be((newX, newY));
            rover.Direction.Should().Be(newDirection);
        }

        [Theory]
        [MemberData(nameof(RoverPaintingTestCases))]
        public void GivenRoverInADirectionAndPosition_WhenExecutingBrainOutput_TheRoverHasCorrectState(RoverTestData testCase)
        {
            var rover = new EmergencyHullPaintingRover(testCase.Position, testCase.Direction);

            for (var i = 0; i < testCase.BrainInstructions.Length; i += 2)
            {
                rover.Paint(testCase.BrainInstructions[i]);
                rover.Turn(testCase.BrainInstructions[i + 1]);
            }

            rover.PanelsDrawn.Should().HaveCount(testCase.TotalPanelsDrawn);
            rover.PanelsDrawn.Last().Should().Be(testCase.NewPanelDrawnLast);
            rover.Direction.Should().Be(testCase.NewDirection);
            rover.Position.Should().Be(testCase.NewPosition);
        }

        [Fact]
        public void PartOneSolution()
        {
            const string instructions =
                @"3,8,1005,8,319,1106,0,11,0,0,0,104,1,104,0,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,1001,8,0,28,2,1008,7,10,2,4,17,10,3,8,102,-1,8,10,101,1,10,10,4,10,1008,8,0,10,4,10,1002,8,1,59,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,0,10,4,10,1001,8,0,81,1006,0,24,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,105,2,6,13,10,1006,0,5,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,1002,8,1,134,2,1007,0,10,2,1102,20,10,2,1106,4,10,1,3,1,10,3,8,102,-1,8,10,101,1,10,10,4,10,108,1,8,10,4,10,1002,8,1,172,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,1,8,10,4,10,101,0,8,194,1,103,7,10,1006,0,3,1,4,0,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,228,2,109,0,10,1,101,17,10,1006,0,79,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,1002,8,1,260,2,1008,16,10,1,1105,20,10,1,3,17,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,295,1,1002,16,10,101,1,9,9,1007,9,1081,10,1005,10,15,99,109,641,104,0,104,1,21101,387365733012,0,1,21102,1,336,0,1105,1,440,21102,937263735552,1,1,21101,0,347,0,1106,0,440,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21102,3451034715,1,1,21101,0,394,0,1105,1,440,21102,3224595675,1,1,21101,0,405,0,1106,0,440,3,10,104,0,104,0,3,10,104,0,104,0,21101,0,838337454440,1,21102,428,1,0,1105,1,440,21101,0,825460798308,1,21101,439,0,0,1105,1,440,99,109,2,22101,0,-1,1,21102,1,40,2,21101,0,471,3,21101,461,0,0,1106,0,504,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,466,467,482,4,0,1001,466,1,466,108,4,466,10,1006,10,498,1102,1,0,466,109,-2,2105,1,0,0,109,4,2101,0,-1,503,1207,-3,0,10,1006,10,521,21101,0,0,-3,21202,-3,1,1,22102,1,-2,2,21101,1,0,3,21102,540,1,0,1105,1,545,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,568,2207,-4,-2,10,1006,10,568,22102,1,-4,-4,1106,0,636,22102,1,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,587,1,0,1105,1,545,21201,1,0,-4,21101,0,1,-1,2207,-4,-2,10,1006,10,606,21102,0,1,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,628,22102,1,-1,1,21102,1,628,0,105,1,503,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2106,0,0";
            var rover = new EmergencyHullPaintingRover();

            rover.PaintRegistration(instructions, color: 0);

            rover.PanelsDrawn.GroupBy(x => x.Panel).Should().HaveCount(2293);
        }

        [Fact]
        public void PartTwoSolution()
        {
            const string instructions =
                @"3,8,1005,8,319,1106,0,11,0,0,0,104,1,104,0,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,1001,8,0,28,2,1008,7,10,2,4,17,10,3,8,102,-1,8,10,101,1,10,10,4,10,1008,8,0,10,4,10,1002,8,1,59,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,0,10,4,10,1001,8,0,81,1006,0,24,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,105,2,6,13,10,1006,0,5,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,1002,8,1,134,2,1007,0,10,2,1102,20,10,2,1106,4,10,1,3,1,10,3,8,102,-1,8,10,101,1,10,10,4,10,108,1,8,10,4,10,1002,8,1,172,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,1,8,10,4,10,101,0,8,194,1,103,7,10,1006,0,3,1,4,0,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,228,2,109,0,10,1,101,17,10,1006,0,79,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,1002,8,1,260,2,1008,16,10,1,1105,20,10,1,3,17,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,295,1,1002,16,10,101,1,9,9,1007,9,1081,10,1005,10,15,99,109,641,104,0,104,1,21101,387365733012,0,1,21102,1,336,0,1105,1,440,21102,937263735552,1,1,21101,0,347,0,1106,0,440,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21102,3451034715,1,1,21101,0,394,0,1105,1,440,21102,3224595675,1,1,21101,0,405,0,1106,0,440,3,10,104,0,104,0,3,10,104,0,104,0,21101,0,838337454440,1,21102,428,1,0,1105,1,440,21101,0,825460798308,1,21101,439,0,0,1105,1,440,99,109,2,22101,0,-1,1,21102,1,40,2,21101,0,471,3,21101,461,0,0,1106,0,504,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,466,467,482,4,0,1001,466,1,466,108,4,466,10,1006,10,498,1102,1,0,466,109,-2,2105,1,0,0,109,4,2101,0,-1,503,1207,-3,0,10,1006,10,521,21101,0,0,-3,21202,-3,1,1,22102,1,-2,2,21101,1,0,3,21102,540,1,0,1105,1,545,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,568,2207,-4,-2,10,1006,10,568,22102,1,-4,-4,1106,0,636,22102,1,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,587,1,0,1105,1,545,21201,1,0,-4,21101,0,1,-1,2207,-4,-2,10,1006,10,606,21102,0,1,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,628,22102,1,-1,1,21102,1,628,0,105,1,503,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2106,0,0";
            var rover = new EmergencyHullPaintingRover();

            rover.PaintRegistration(instructions, color: 1);

            var minX = rover.PanelsDrawn.Min(x => x.Panel.X);
            var minY = rover.PanelsDrawn.Min(x => x.Panel.Y);
            var maxX = rover.PanelsDrawn.Max(x => x.Panel.X);
            var maxY = rover.PanelsDrawn.Max(x => x.Panel.Y);

            var result = new StringBuilder();

            var registration = rover.PanelsDrawn.ToDictionary(x => x.Panel, x => x.Color);

            for (var j = maxY - minY; j >= 0; j--)
            {
                for (var i = 0; i < maxX - minX + 1; i++)
                {
                    if (registration.TryGetValue((i + minX, j + minY), out var color))
                        result.Append(color == '.' ? " " : "#");
                    else
                        result.Append(" ");
                }

                result.AppendLine();
            }
            File.WriteAllText("registration", result.ToString());
            //AHLCPRAL
        }
        public static IEnumerable<object[]> RoverPaintingTestCases => new List<RoverTestData[]>
        {
            new []
            {
                new RoverTestData
                {
                    Position = (0,0),
                    Direction = '^',
                    BrainInstructions = new[]{1,0},
                    NewPosition = (-1,0),
                    NewDirection = '<',
                    NewPanelDrawnLast = ('#', (0,0)),
                    TotalPanelsDrawn = 1
                }
            },
            new []
            {
                new RoverTestData
                {
                    Position = (0,0),
                    Direction = '^',
                    BrainInstructions = new[]{1,0,0,0},
                    NewPosition = (-1,-1),
                    NewDirection = 'v',
                    NewPanelDrawnLast = ('.', (-1,0)),
                    TotalPanelsDrawn = 2
                }
            },
            new []
            {
                new RoverTestData
                {
                    Position = (0,0),
                    Direction = '^',
                    BrainInstructions = new[]{1,0,0,0,1,0,1,0},
                    NewPosition = (0,0),
                    NewDirection = '^',
                    NewPanelDrawnLast = ('#', (0,-1)),
                    TotalPanelsDrawn = 4
                }
            },
            new []
            {
                new RoverTestData
                {
                    Position = (0,0),
                    Direction = '^',
                    BrainInstructions = new[]{1,0,0,0,1,0,1,0,0,1,1,0,1,0},
                    NewPosition = (0,1),
                    NewDirection = '<',
                    NewPanelDrawnLast = ('#', (1,1)),
                    TotalPanelsDrawn = 7
                }
            },
        };


        public class RoverTestData
        {
            public (int X, int Y) Position { get; set; }
            public char Direction { get; set; }
            public (char, (int, int)) PanelDrawnLast { get; set; }
            public (int, int) NewPosition { get; set; }
            public char NewDirection { get; set; }
            public (char, (int, int)) NewPanelDrawnLast { get; set; }
            public int TotalPanelsDrawn { get; set; }
            public int[] BrainInstructions { get; set; }
        }
    }
}
