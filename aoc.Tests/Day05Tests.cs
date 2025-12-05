using aoc.Business;

namespace aoc.Tests
{
    public class Day05Tests
    {
        [Theory]
        [InlineData("3-5", new long[] { 3, 4, 5 })]
        [InlineData("10-14", new long[] { 10, 11, 12, 13, 14 })]
        [InlineData("16-20", new long[] { 16, 17, 18, 19, 20 })]
        [InlineData("12-18", new long[] { 12, 13, 14, 15, 16, 17, 18 })]
        public void IsInRangeShouldReturnTrueForValueInRange(string range, long[] compare)
        {
            foreach (var value in compare)
            {
                var result = Day05.IsInRange(range, value);
                Assert.True(result);
            }
        }


        [Theory]
        [InlineData("3-5", new long[] { 2, 6 })]
        [InlineData("10-14", new long[] { 9, 15 })]
        [InlineData("16-20", new long[] { 15, 21 })]
        [InlineData("12-18", new long[] { 11, 19 })]
        public void IsInRangeShouldReturnFalseForValuesOutsideRange(string range, long[] compare)
        {
            foreach (var value in compare)
            {
                var result = Day05.IsInRange(range, value);
                Assert.False(result);
            }
        }

        [Fact]
        public void GetRequestedIdsShouldReturnAllAvailableIds()
        {
            string[] input = [
                "1",
                "5",
                "8",
                "11",
                "17",
                "32"
            ];

            var result = Day05.GetRequestedIds(input);
            Assert.Equal([1, 5, 8, 11, 17, 32], result);
        }

        [Fact]
        public void GetAmoutOfFreshIngredientsShouldReturnExpectedResultForTestInput()
        {
            string[] input = [
                "3-5",
                "10-14",
                "16-20",
                "12-18",
                "",
                "1",
                "5",
                "8",
                "11",
                "17",
                "32",
            ];

            var result = Day05.GetAmoutOfAvailableFreshIngredients(input);
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(3, 5, 3)]
        [InlineData(10, 14, 5)]
        [InlineData(16, 20, 5)]
        [InlineData(12, 18, 7)]
        public void GetAmountOfIdsInRangeShouldReturnExpectedCount(long rangeStart, long rangeEnd, long expectedCount)
        {
            var result = Day05.GetAmountOfIdsInRange((rangeStart, rangeEnd));
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public void MergeIdRangesShouldMergeOverlappingRanges()
        {
            (long Start, long End)[] ranges = [
                (10, 16),
                (15, 20),
                (12, 25)
            ];

            (long Start, long End)[] expected = [
                (10, 16),
                (17, 25)
            ];
            var result = Day05.MergeIdRanges(ranges);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void MergeIdRangesShouldRemoveIrrelevantRanges()
        {
            (long Start, long End)[] ranges = [
                (10, 16),
                (15, 20),
                (12, 26),
                (1, 25)
            ];

            (long Start, long End)[] expected = [
                (1, 25),
                (26, 26)
            ];
            var result = Day05.MergeIdRanges(ranges);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAmoutOfAllFreshIngredientsShouldReturnExpectedResultForTestInput()
        {
            string[] input = [
                "3-5",
                "10-14",
                "10-14",
                "16-20",
                "12-18",
                "1-22",
                "30-37"
            ];
            var result = Day05.GetAmoutOfAllFreshIngredients(input);
            Assert.Equal(30, result);
        }
    }
}
