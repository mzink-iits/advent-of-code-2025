using aoc.Business;
using System.Diagnostics;

namespace aoc.App
{
    internal static class GenericRunner
    {
        public static void Run(Type t)
        {
            static void actionWrapper(ISolver solver, string part)
            {
                var watch = new Stopwatch();
                watch.Start();
                var result = solver.Solve();
                watch.Stop();

                var resultOutput = $"{part} result: {result}";
                var durationOutput = $"Duration: {watch.ElapsedMilliseconds} ms";
                WriteGroupedOutput(solver.GetType().Name, resultOutput, durationOutput);
            }

            RunPartOne(t, actionWrapper);
            RunPartTwo(t, actionWrapper);
        }

        private static void RunPartOne(Type solverType, Action<ISolver, string> execute)
        {
            try
            {
                var solver = (ISolver?)Activator.CreateInstance(solverType, true, false);
                if (Equals(solver, default))
                {
                    return;
                }
                execute(solver, "Part one");
            }
            catch (MissingMethodException)
            {
                WriteGroupedOutput($"{solverType.Name}", "Part one has not yet been implemented.");
            }
        }
        
        private static void RunPartTwo(Type solverType, Action<ISolver, string> execute)
        {
            try
            {
                var solver = (ISolver?)Activator.CreateInstance(solverType, true, true);
                if (Equals(solver, default))
                {
                    return;
                }

                execute(solver, "Part two");
            }
            catch (MissingMethodException)
            {
                WriteGroupedOutput($"{solverType.Name}", "Part two has not yet been implemented.");
            }
        }

        private static void WriteGroupedOutput(string identifier, params string[] lines)
        {
            Console.WriteLine($"-------- {identifier} --------");
            foreach(var line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}
