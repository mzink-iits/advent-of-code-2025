


namespace aoc.Business
{
    public class Day01
    {
        private readonly string[] _input = [];

        public Day01(bool liveMode = false, bool useSecurityProtocol = false)
        {
            if (liveMode)
            {
                _input = File.ReadAllLines("inputs/day-01.txt");
            }
            _securityProtocolActive = useSecurityProtocol;
        }

        private readonly bool _securityProtocolActive;

        public int Password { get; private set; }

        public int DialPosition { get; private set; } = 50;

        public void Rotate(string input)
        {
            var direction = input.ToLowerInvariant()[0];
            var turns = int.Parse(input[1..]);
            var positionModifier = turns % 100;
            var crossedZero = direction switch
            {
                'l' => DialLeft(positionModifier, turns),
                'r' => DialRight(positionModifier, turns),
                _ => throw new ArgumentException("Input must start with L or R"),
            };
            if (DialPosition == 0)
            {
                Password += _securityProtocolActive ? crossedZero : 1;
            }
            else if (_securityProtocolActive)
            {
                Password += crossedZero;
            }
        }

        private int DialLeft(int positionModifier, int turns)
        {
            var crossedZero = 0;
            var newPosition = DialPosition - positionModifier;
            if (newPosition < 0)
            {
                newPosition += 100;
                if (DialPosition != 0)
                {
                    crossedZero = 1;
                }
            }
            else if (positionModifier > 0 && DialPosition != 0 && newPosition == 0)
            {
                crossedZero = 1;
            }
            DialPosition = newPosition;
            crossedZero += (turns - positionModifier) / 100;
            return crossedZero;
        }

        private int DialRight(int positionModifier, int turns)
        {
            var crossedZero = 0;
            var newPosition = DialPosition + positionModifier;
            if (newPosition > 99)
            {
                newPosition -= 100;
                if (DialPosition != 0)
                {
                    crossedZero = 1;
                }
            }
            else if (positionModifier > 0 && DialPosition != 0 && newPosition == 0)
            {
                crossedZero = 1;
            }
            DialPosition = newPosition;
            crossedZero += (turns - positionModifier) / 100;
            return crossedZero;
        }

        public int Solve()
        {
            foreach (var input in _input)
            {
                Rotate(input);
            }
            return Password;
        }
    }
}
