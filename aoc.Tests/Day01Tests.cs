using aoc.Business;

namespace aoc.Tests
{
    public class Day01Tests
    {
        [Fact]
        public void DialStartsAt50()
        {
            var day01 = new Day01();
            var result = day01.DialPosition;
            Assert.Equal(50, result);
        }

        [Theory]
        [InlineData(2, 48)]
        [InlineData(52, 98)]
        [InlineData(252, 98)]
        public void StartWithLSubstracts(int input, int expectedResult)
        {
            var day01 = new Day01();
            day01.Rotate($"L{input}");
            Assert.Equal(expectedResult, day01.DialPosition);
        }

        [Theory]
        [InlineData(2, 52)]
        [InlineData(60, 10)]
        [InlineData(255, 5)]
        public void StartWithRAdds(int input, int expectedResult)
        {
            var day01 = new Day01();
            day01.Rotate($"R{input}");
            Assert.Equal(expectedResult, day01.DialPosition);
        }

        [Theory]
        [InlineData(false, 3)]
        [InlineData(true, 6)]
        public void TestInput(bool useSecurity, int expectedPassword)
        {
            var day01 = new Day01(useSecurityProtocol: useSecurity);
            var testInput = new string[] { "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82" };
            foreach (var input in testInput)
            {
                day01.Rotate(input);
            }
            Assert.Equal(32, day01.DialPosition);
            Assert.Equal(expectedPassword, day01.Password);
        }

        [Theory]
        [InlineData(1, "R50")]
        [InlineData(1, "L50")]
        [InlineData(2, "R50", "R100")]
        [InlineData(2, "R50", "L100")]
        [InlineData(2, "L50", "R100")]
        [InlineData(2, "L50", "L100")]
        [InlineData(2, "R50", "L50", "L50")]
        [InlineData(2, "L50", "R50", "R50")]
        [InlineData(1, "R350")]
        [InlineData(1, "L350")]
        public void HittingZeroIsRecorded(int expectedResult, params string[] testInput)
        {
            var day01 = new Day01();
            foreach (var input in testInput)
            {
                day01.Rotate(input);
            }
            Assert.Equal(expectedResult, day01.Password);
        }

        [Theory]
        [InlineData("R1000", 10)]
        [InlineData("L1000", 10)]
        [InlineData("L1050", 11)]
        public void LargeInputsWork(string input, int expectedPassword)
        {
            var day01 = new Day01(useSecurityProtocol: true);
            day01.Rotate(input);
            Assert.InRange(day01.DialPosition, 0, 99);
            Assert.Equal(expectedPassword, day01.Password);
        }
    }
}
