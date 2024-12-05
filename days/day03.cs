using System.Text.RegularExpressions;

namespace AOC.days;

internal class Day03 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "03_t1.txt"), 161);
        AddRun("Part 1", () => RunPart(1, "03.txt"));
        AddRun("Test 2", () => RunPart(2, "03_t2.txt"), 48);
        AddRun("Part 2", () => RunPart(2, "03.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = String.Join(' ', GetListOfLines(inputName));
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        long result = 0;
        if (part == 2)
        {
            input = input + "do()";
            string removePattern = @"don't\(\).*?do\(\)";
            input = Regex.Replace(input, removePattern, "");
        }

        var matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {
            result += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
        }
        return result;
    }
}