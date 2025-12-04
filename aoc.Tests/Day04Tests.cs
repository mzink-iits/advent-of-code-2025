using aoc.Business;

namespace aoc.Tests
{
    public class Day04Tests
    {
        [Theory]
        [InlineData("..@@.@@@@.", 0, 0)]
        [InlineData("..@@.@@@@.", 1, 0)]
        public void GetBoundariesShouldFillRightIfLeftIsShorter(string input, int index, int expectedLeft)
        {
            var (Left, _) = Day04.GetBoundaries(input, index);
            Assert.Equal(expectedLeft, Left);
            Assert.NotEmpty(input[Left..].ToString());
        }

        [Theory]
        [InlineData("..@@.@@@@.", 8, 10)]
        [InlineData("..@@.@@@@.", 9, 10)]
        public void GetBoundariesShouldFillLeftIfRightIsShorter(string input, int index, int expectedRight)
        {
            var (_, Right) = Day04.GetBoundaries(input, index);
            Assert.Equal(expectedRight, Right);
            Assert.NotEmpty(input[..Right].ToString());
        }

        [Theory]
        [InlineData(null, "..@@.@@@@.", "@@@.@.@.@@", 0, 0)]
        [InlineData(null, "..@@.@@@@.", "@@@.@.@.@@", 2, 3)]
        [InlineData(null, "..@@.@@@@.", "@@@.@.@.@@", 7, 4)]
        [InlineData(".@@@@@@@@.", "@.@.@@@.@.", null, 1, 0)]
        [InlineData(".@@@@@@@@.", "@.@.@@@.@.", null, 2, 3)]
        public void GetSurroundingRollsShouldReturnNumberOfAdjacentAtCharacters(string? prevLine, string line, string? nextLine, int index, int expectedCount)
        {
            var result = Day04.GetNumberOfSurroundingRolls(prevLine, line, nextLine, index);
            Assert.Equal(expectedCount, result);
        }

        [Theory]
        [InlineData(0, "", "", "")]
        [InlineData(11, "..@@.@@@@.", "@@@.@.@.@@", "@@@@@.@.@@")]
        public void GetValidRollCountShouldReturnSumOfRollsSurroundedByMaxFourOtherRolls(int expected, params string[] lines)
        {
            var solver = new Day04();
            var result = solver.GetValidRollCount(lines, 0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInputShouldReturnExpectedValidRollCount()
        {
            var testInput = new string[]
            {
                "..@@.@@@@.",
                "@@@.@.@.@@",
                "@@@@@.@.@@",
                "@.@@@@..@.",
                "@@.@@@@.@@",
                ".@@@@@@@.@",
                ".@.@.@.@@@",
                "@.@@@.@@@@",
                ".@@@@@@@@.",
                "@.@.@@@.@."
            };

            var solver = new Day04();
            var result = solver.GetValidRollCount(testInput, 0);

            Assert.Equal(13, result);
        }

        [Fact]
        public void TestInputShouldReturnExpectedValidRollCountForRecursiveRun()
        {
            var testInput = new string[]
            {
                "..@@.@@@@.",
                "@@@.@.@.@@",
                "@@@@@.@.@@",
                "@.@@@@..@.",
                "@@.@@@@.@@",
                ".@@@@@@@.@",
                ".@.@.@.@@@",
                "@.@@@.@@@@",
                ".@@@@@@@@.",
                "@.@.@@@.@."
            };

            var solver = new Day04(removeRecursively: true);
            var result = solver.SolveRecursively(testInput, 0);

            Assert.Equal(43, result);
        }
    }
}
