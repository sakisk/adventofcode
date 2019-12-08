using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.DayEight;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayEightTests
{
    public class SpaceImageFormatDecoderTests
    {
        [Fact]
        public void PartOne()
        {
            var image = File.ReadAllText("input");
            var layers = new List<string>();
            var width = 25;
            var height = 6;
            var layerSize = width * height;

            for (var layer = 0; layer < image.Length; layer += layerSize)
            {
                layers.Add(image.Substring(layer, layerSize));
            }

            var layersWithMinZeros = layers.GroupBy(g => g.Count(x => x == '0'), x => x).Where(x => x.Key > 0).OrderBy(x => x.Key).First();
            var onesCount = layersWithMinZeros.Single().Count(x => x == '1');
            var twosCount = layersWithMinZeros.Single().Count(x => x == '2');

            (onesCount * twosCount).Should().Be(1572);
        }

        [Theory]
        [InlineData("0222112222120000", 2, 2, "0110")]
        public void TestPartTwo(string image, int width, int height, string result)
            => new ImageDecoder(image, width, height).Decode().Should().Be(result);

        [Fact]
        public void PartTwoSolution()
        {
            var encrypted = File.ReadAllText("input");
            var decoder = new ImageDecoder(encrypted, 25, 6);

            decoder.WriteMessage();
            //KYHFE
        }

    }
}
