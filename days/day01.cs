using AOC.util;

namespace AOC.days;

internal class Day01 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "01_t1.txt"), 11);
        AddRun("Part 1", () => RunPart(1, "01.txt"));
        AddRun("Test 2", () => RunPart(2, "01_t1.txt"), 31);
        AddRun("Part 2", () => RunPart(2, "01.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var left = new List<long>();
        var right = new List<long>();
        var countRight = new DefaultDictionary<long, long>();
        foreach (var line in input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            left.Add(long.Parse(numbers[0]));
            var r = long.Parse(numbers[1]);
            right.Add(r);
            countRight[r]++;
        }
        left.Sort();
        right.Sort();
        long result = 0;

        if (part == 1)
        {
            result = left.Zip(right, (l, r) => Math.Abs(r - l)).Sum();
        }
        else
        {
            result = left.Select(l => l * countRight[l]).Sum();
        }

        return result;
    }
}