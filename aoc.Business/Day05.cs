namespace aoc.Business
{
    public class Day05 : ISolver
    {
        private readonly string[] _input = [];
        private readonly bool _getTotalAmountOfFreshIngredients;

        public Day05(bool liveMode = false, bool getTotalAmountOfFreshIngredients = false)
        {
            if (liveMode)
            {
                _input = File.ReadAllLines("inputs/day-05.txt");
            }

            _getTotalAmountOfFreshIngredients = getTotalAmountOfFreshIngredients;
        }

        #region Part 1

        public static long GetAmoutOfAvailableFreshIngredients(string[] input)
        {
            var idRanges = GetIdRanges(input, out var index);
            long[] requestedIds = [.. GetRequestedIds(input[(index + 1)..])];

            return requestedIds.Count(item => idRanges.Any(range => IsInRange(range, item)));
        }

        public static long[] GetRequestedIds(string[] input)
        {
            return [.. input.Select(long.Parse)];
        }

        public static bool IsInRange(string range, long compare)
        {
            var (Start, End) = GetIdRangeTuple(range);
            return compare >= Start && compare <= End;
        }


        #endregion Part 1

        #region Shared

        private static List<string> GetIdRanges(string[] input, out int index)
        {
            var idRanges = new List<string>();
            index = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                idRanges.Add(line);
                index++;
            }
            return idRanges;
        }

        #endregion Shared

        #region Part 2

        public static long GetAmoutOfAllFreshIngredients(string[] input)
        {
            var idRanges = GetIdRanges(input, out _);
            var tuples = idRanges
                .Select(GetIdRangeTuple)
                .ToArray();

            var mergedRanges = MergeIdRanges(tuples);

            return mergedRanges
                .Select(GetAmountOfIdsInRange)
                .Sum();
        }

        private static (long Start, long End) GetIdRangeTuple(string line)
        {
            var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
            return (Start: long.Parse(parts[0]), End: long.Parse(parts[1]));
        }

        public static (long Start, long End)[] MergeIdRanges((long Start, long End)[] ranges)
        {
            var ordered = ranges
                .OrderBy(r => r.Start)
                .ToArray();

            var previous = ordered[0];
            List<(long Start, long End)> result = [previous];

            for (var i = 1; i < ordered.Length; i++)
            {
                var current = ordered[i];
                if (current.Start <= previous.End)
                {
                    current.Start = previous.End + 1;
                }
                if (current.Start <= current.End)
                {
                    result.Add((current.Start, current.End));
                    previous = current;
                }
            }

            return [.. result];
        }

        public static long GetAmountOfIdsInRange((long Start, long End) range)
        {
            if (range.Start > range.End)
            {
                return 0;
            }
            return range.End - range.Start + 1;
        }

        #endregion Part 2

        public object Solve()
        {
            return _getTotalAmountOfFreshIngredients ?
                GetAmoutOfAllFreshIngredients(_input) :
                GetAmoutOfAvailableFreshIngredients(_input);
        }
    }
}
