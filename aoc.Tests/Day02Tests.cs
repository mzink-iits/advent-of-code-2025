using aoc.Business;

namespace aoc.Tests
{
    public class Day02Tests
    {
        [Fact]
        public void GetComparisonLengthShouldReturnExpectedValue()
        {
            Assert.Equal(3, Day02.GetComparsionLength("12345"));
            Assert.Equal(2, Day02.GetComparsionLength("1234"));
            Assert.Equal(1, Day02.GetComparsionLength("12"));
            Assert.Equal(1, Day02.GetComparsionLength("1"));
        }

        [Theory]
        [InlineData("11-22", new long[] { 11, 22 })]
        [InlineData("95-115", new long[] { 99 })]
        [InlineData("998-1012", new long[] { 1010 })]
        [InlineData("1188511880-1188511890", new long[] { 1188511885 })]
        [InlineData("222220-222224", new long[] { 222222 })]
        [InlineData("1698522-1698528", new long[] { })]
        [InlineData("446443-446449", new long[] { 446446 })]
        [InlineData("38593856-38593862", new long[] { 38593859 })]
        public void IdentifyShouldFindInvalidSingleRepetitionPatterns(string input, long[] expectedOutput)
        {
            var invalidIds = Day02.IdentifySingleRepetition(input);
            Assert.Equal(expectedOutput, invalidIds);
        }

        [Theory]
        [InlineData(new[] { "11-22", "95-115" }, new long[] { 11, 22, 99 })]
        public void IdentifyShouldHandleMultipleRanges(string[] input, long[] expectedOutput)
        {
            var invalidIds = Day02.IdentifySingleRepetition(string.Join(',', input));
            Assert.Equal(expectedOutput, invalidIds);
        }

        [Fact]
        public void TestInput()
        {
            var testInput = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
            var invalidIds = Day02.IdentifySingleRepetition(testInput);
            var expectedOutput = new long[] { 11, 22, 99, 1010, 1188511885, 222222, 446446, 38593859 };
            Assert.Equal(expectedOutput, invalidIds);
        }


        [Theory]
        [InlineData("11-22", new long[] { 11, 22 })]
        [InlineData("95-115", new long[] { 99, 111 })]
        [InlineData("998-1012", new long[] { 999, 1010 })]
        [InlineData("1188511880-1188511890", new long[] { 1188511885 })]
        [InlineData("222220-222224", new long[] { 222222 })]
        [InlineData("1698522-1698528", new long[] { })]
        [InlineData("446443-446449", new long[] { 446446 })]
        [InlineData("38593856-38593862", new long[] { 38593859 })]
        [InlineData("565653-565659", new long[] { 565656 })]
        [InlineData("824824821-824824827", new long[] { 824824824 })]
        [InlineData("2121212118-2121212124", new long[] { 2121212121 })]
        public void IdentifyShouldFindInvalidMultipleRepetitionPatterns(string input, long[] expectedOutput)
        {
            var invalidIds = Day02.IdentifyMultipleRepetitions(input);
            Assert.Equal(expectedOutput, invalidIds);
        }

        [Theory]
        [InlineData(11, 22, 1, new long[] { 11, 22 })]
        [InlineData(11, 22, 2, new long[] { })]
        [InlineData(110, 112, 1, new long[] { 111 })]
        public void GetRepetitionsShouldReturnExpectedValue(long minValue, long maxValue, int comparisonLength, long[] expectedOutput)
        {
            var repetitions = Day02.GetRepetitions(minValue, maxValue, comparisonLength, false);
            Assert.Equal(expectedOutput, repetitions);
        }
    }
}
