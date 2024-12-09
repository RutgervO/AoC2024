using AOC.util;

namespace AOC.days;

internal class Day07 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "07_t1.txt"), 3749);
        AddRun("Part 1", () => RunPart(1, "07.txt"));
        AddRun("Test 2", () => RunPart(2, "07_t1.txt"), 11387);
        AddRun("Part 2", () => RunPart(2, "07.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        long result = 0;
        foreach (var line in GetListOfLines(inputName))
        {
            var target = line.Split(':').Select(long.Parse).First();
            var numbers = line.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            if (HasResult(target, numbers.First(), numbers.Skip(1).ToList())) result += target;
        }
        return result;

        bool HasResult(long target, long current, List<long> toDo)
        {
            if (current > target) return false;
            if (!toDo.Any()) return (current == target);
            var first = toDo.First();
            var rest = toDo.Skip(1).ToList();
            return HasResult(target, current + first, rest)
                   | HasResult(target, current * first, rest)
                   | (part == 2 & HasResult(target, long.Parse(current.ToString() + first.ToString()), rest));
        }
    }
}