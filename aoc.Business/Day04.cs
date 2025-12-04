
namespace aoc.Business
{
    public class Day04 : ISolver
    {
        private readonly string[] _input = [];
        private readonly bool _removeRecursively = false;

        public Day04(bool liveMode = false, bool removeRecursively = false)
        {
            if (liveMode)
            {
                _input = File.ReadAllLines("inputs/day-04.txt");
            }

            _removeRecursively = removeRecursively;
        }

        public static (int Left, int Right) GetBoundaries(string input, int index)
        {
            var left = index < 1 ? 0 : index - 1;
            var right = (input.Length - index - 1) < 1 ? input.Length - 1 : index + 1;

            return (left, right + 1);
        }

        public static int GetNumberOfSurroundingRolls(string? previousLine, string line, string? nextLine, int index)
        {
            if (line[index] != '@')
            {
                return 0;
            }

            var previousLineCharacters = previousLine?.ToCharArray() ?? [.. Enumerable.Repeat('.', line.Length)];
            var currentLineCharacters = line.ToCharArray();
            var nextLineCharacters = nextLine?.ToCharArray() ?? [.. Enumerable.Repeat('.', line.Length)];

            var (Left, Right) = GetBoundaries(line, index);

            return previousLineCharacters[Left..Right]
                .Concat(currentLineCharacters[Left..Right])
                .Concat(nextLineCharacters[Left..Right])
                .Count(c => c == '@' || c == 'x') - 1;
        }

        public int GetValidRollCount(string[] input, int currentLineIndex)
        {
            if (input.Length <= currentLineIndex)
            {
                return 0;
            }

            var inputLine = input[currentLineIndex];

            int validRollCount = 0;
            var positions = inputLine.Index()
                .Where(c => c.Item == '@')
                .Select(c => c.Index)
                .ToList();

            var previousLine = currentLineIndex > 0 ? input[currentLineIndex - 1] : null;
            var nextLine = currentLineIndex < input.Length - 1 ? input[currentLineIndex + 1] : null;

            foreach (var position in positions)
            {
                var surroundingRolls = GetNumberOfSurroundingRolls(previousLine, inputLine, nextLine, position);
                if (surroundingRolls < 4)
                {
                    validRollCount++;
                    if (_removeRecursively)
                    {
                        inputLine = inputLine[..position] + 'x' + inputLine[(position + 1)..];
                    }
                }
            }

            input[currentLineIndex] = inputLine;
            return validRollCount + GetValidRollCount(input, currentLineIndex + 1);
        }

        public long SolveRecursively(string[] input, int currentLineIndex)
        {
            var total = 0;
            var currentValidRolls = GetValidRollCount(input, currentLineIndex);
            while (currentValidRolls > 0)
            {
                total += currentValidRolls;
                for (int i = 0; i < input.Length; i++)
                {
                    input[i] = input[i].Replace('x', '.');
                }
                currentValidRolls = GetValidRollCount(input, currentLineIndex);
            }

            return total;
        }

        public object Solve()
        {
            return _removeRecursively ? 
                SolveRecursively(_input, 0) : 
                GetValidRollCount(_input, 0);
        }
    }
}
