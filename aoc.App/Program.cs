using aoc.App;
using aoc.Business;

Type[] solvers = [typeof(Day01), typeof(Day02), typeof(Day03), typeof(Day04), typeof(Day05)];

foreach(var type in solvers)
{
    GenericRunner.Run(type);
}