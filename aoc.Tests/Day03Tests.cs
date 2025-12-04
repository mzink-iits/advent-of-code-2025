using aoc.Business;

namespace aoc.Tests
{
    public class Day03Tests
    {
        [Theory]
        [InlineData("987654321111111", 98)]
        [InlineData("811111111111119", 89)]
        [InlineData("234234234234278", 78)]
        [InlineData("818181911112111", 92)]
        public void FindMaxValueShouldReturnMaxValue(string input, int expectedResult)
        {
            var result = Day03.FindMaxValue(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("818181911112111", 1, 9)]
        [InlineData("818181211112111", 8, 88822111)]
        [InlineData("818181211111111", 8, 88821111)]
        [InlineData("818181912112111", 12, 888912112111)]
        public void TestShouldReturnExpectedValue(string input, int maxLength, long expectedResult)
        {
            var result = Day03.FindMaxValueWithSafetyOff(input, maxLength, -1);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("987654321111111", 987654321111)]
        [InlineData("811111111111119", 811111111119)]
        [InlineData("234234234234278", 434234234278)]
        [InlineData("818181911112111", 888911112111)]
        [InlineData("2729241222329123457422528225142242286297497441472728464675615243626217526319294335298622221564523151", 999999986655)]

        public void FindMaxValueWithSafetyOffShouldReturnMaxValue(string input, long expectedResult)
        {
            var result = Day03.FindMaxValueWithSafetyOff(input, 12, -1);
            Assert.Equal(expectedResult, result);
        }
    }
}
