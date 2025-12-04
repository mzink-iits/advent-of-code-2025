namespace aoc.Business
{
    public class Day03 : ISolver
    {
        private readonly string[] _input = [];
        private readonly bool _safetyOverride;

        public Day03(bool liveMode = false, bool safetyOverride = false)
        {
            if (liveMode)
            {
                _input = File.ReadAllLines("inputs/day-03.txt");
            }

            _safetyOverride = safetyOverride;
        }

        public object Solve()
        {
            return _safetyOverride ?
                _input.Select(i => FindMaxValueWithSafetyOff(i, 12, -1)).Sum() :
                _input.Select(FindMaxValue).Sum();
        }

        public static int FindMaxValue(string input)
        {
            var maxNumber = FindNumber(input.Reverse().Skip(1));

            var index = input.IndexOf(maxNumber.ToString()) + 1;
            var secondNumber = FindNumber(input.Skip(index));

            return maxNumber * 10 + secondNumber;
        }

        private static int FindNumber(IEnumerable<char> input)
        {
            var maxNumber = 0;
            foreach (var @char in input)
            {
                var currentNumber = int.Parse(@char.ToString());
                if (currentNumber > maxNumber)
                {
                    maxNumber = currentNumber;
                }
            }
            return maxNumber;
        }

        public static long FindMaxValueWithSafetyOff(string input, int digits, int lastIndex)
        {
            if (digits == 0)
            {
                return 0;
            }

            var index = input.Index().Select(idx => (Item: idx.Item - 48, idx.Index)).ToList();

            if (index.Count - lastIndex - 1 == digits)
            {
                var total = 0L;
                for (int i = 1; i <= digits; i++)
                {
                    total += index[i + lastIndex].Item * (long)Math.Pow(10, digits - i);
                }
                return total;
            }

            var ordered = index.Where(c => c.Index > lastIndex && c.Index <= index.Count - digits)
                .OrderByDescending(c => c.Item)
                .ThenBy(c => c.Item);

            var item = (long)(ordered.First().Item * Math.Pow(10, digits - 1));

            return item + FindMaxValueWithSafetyOff(input, digits - 1, ordered.First().Index);
        }
    }
}
