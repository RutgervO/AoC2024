namespace AOC.days;

internal class Day11 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "11_t1.txt"), 55312);
        AddRun("Part 1", () => RunPart(1, "11.txt"));
        AddRun("Part 2", () => RunPart(2, "11.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var stones = GetListOfLines(inputName).Single().Split(' ').Select(long.Parse).ToList();

        var cache = new Dictionary<(long value, int count), long>();
        return stones.Select(s => GetStoneCount(s, part == 1 ? 25 : 75)).Sum();

        long GetStoneCount(long value, int count)
        {
            if (cache.ContainsKey((value, count))) return cache[(value, count)];
            long result = 1;
            if (count != 0)
            {
                if (value == 0)
                {
                    result = GetStoneCount(1, count - 1);
                }
                else
                {
                    string strValue = value.ToString();
                    var valueLength = strValue.Length;
                    if (valueLength % 2 == 0)
                    {
                        valueLength /= 2;
                        result = GetStoneCount(long.Parse(strValue[..valueLength]), count - 1)
                               + GetStoneCount(long.Parse(strValue[valueLength..]), count - 1);
                    }
                    else
                    {
                        result = GetStoneCount(value * 2024, count - 1);
                    }
                }
            }

            cache[(value, count)] = result;
            return result;
        }
    }
}