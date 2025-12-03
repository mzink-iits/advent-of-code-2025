namespace aoc.Business
{
    public class Day02
    {
        private readonly string _input = string.Empty;
        private readonly bool _solveForMultiple = false;

        public Day02(bool liveMode = false, bool solveForMultiple = false)
        {
            if (liveMode)
            {
                _input = File.ReadAllText("inputs/day-02.txt");
            }
            _solveForMultiple = solveForMultiple;
        }


        public long Solve()
        {
            var invalidIds = _solveForMultiple ? IdentifyMultipleRepetitions(_input) : IdentifySingleRepetition(_input);
            return invalidIds.Sum();
        }

        public static List<long> IdentifySingleRepetition(string input)
        {
            var results = new List<long>();
            foreach (var range in SplitInputRanges(input))
            {
                var split = range.Split('-');

                var comparisonMinLength = GetComparsionLength(split[0]);
                var comparisonMaxLength = GetComparsionLength(split[1]);
                var minValue = long.Parse(split[0]);
                var maxValue = long.Parse(split[1]);

                for (var comparisonLength = comparisonMinLength; comparisonLength <= comparisonMaxLength; comparisonLength++)
                {
                    results.AddRange(GetRepetitions(minValue, maxValue, comparisonLength, true));
                }
            }

            return results;
        }

        public static IEnumerable<long> IdentifyMultipleRepetitions(string input)
        {
            var results = new List<long>();

            foreach (var range in SplitInputRanges(input))
            {
                var split = range.Split('-');

                var comparisonMaxLength = GetComparsionLength(split[1]);
                var minValue = long.Parse(split[0]);
                var maxValue = long.Parse(split[1]);
                for (var comparisonLength = 1; comparisonLength <= comparisonMaxLength; comparisonLength++)
                {
                    results.AddRange(GetRepetitions(minValue, maxValue, comparisonLength, false));
                }
            }

            return results.Distinct();
        }

        public static List<long> GetRepetitions(long minValue, long maxValue, int comparisonLength, bool ignoreOddLengths)
        {
            var results = new List<long>();

            for (var num = minValue; num <= maxValue; num++)
            {
                var numStr = num.ToString();

                if (numStr.Length % comparisonLength != 0 || ignoreOddLengths && numStr.Length % 2 != 0)
                {
#pragma warning disable
                    num = long.Parse("1" + new string('0', numStr.Length));
#pragma warning restore
                    continue;
                }

                var compare = string.Join(string.Empty, numStr[..comparisonLength]);
                var repetitions = comparisonLength == 0 ? 1 : numStr.Length / comparisonLength;
                if (repetitions > 1 && numStr.Equals(string.Concat(Enumerable.Repeat(compare, repetitions))))
                {
                    results.Add(num);
                }
            }

            return results;
        }

        private static string[] SplitInputRanges(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static int GetComparsionLength(string value)
        {
            var length = value.Length;
            var comparisonLength = length / 2;
            if (length % 2 != 0)
            {
                comparisonLength++;
            }
            return comparisonLength;
        }
    }
}
