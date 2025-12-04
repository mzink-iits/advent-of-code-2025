using aoc.Business;
using System.Diagnostics;

var watch = new Stopwatch();
var solver = new Day03(true, true);
watch.Start();
var result = solver.Solve();
watch.Stop();

Console.WriteLine(result);
Console.WriteLine($"Duration: {watch.ElapsedMilliseconds} ms");