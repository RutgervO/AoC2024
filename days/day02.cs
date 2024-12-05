namespace AOC.days;

internal class Day02 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "02_t1.txt"), 2);
        AddRun("Part 1", () => RunPart(1, "02.txt"));
        AddRun("Test 2", () => RunPart(2, "02_t1.txt"), 4);
        AddRun("Part 2", () => RunPart(2, "02.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var safeCount = 0;
        if (part == 1)
        {
            foreach (var line in input)
            {
                var numbers = line.Split(' ').Select(int.Parse).ToArray();
                var numbers2 = numbers[1..];
                if (numbers.Zip(numbers2, (l, r) => r - l).All(n => n > 0 & n < 4)
                    | numbers.Zip(numbers2, (l, r) => l - r).All(n => n > 0 & n < 4))
                {
                    safeCount++;
                }
            }
            return safeCount;
        }
        
        foreach (var line in input)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            var numbers2 = numbers[1..];
            if (numbers.Zip(numbers2, (l, r) => r - l).All(n => n > 0 & n < 4)
                | numbers.Zip(numbers2, (l, r) => l - r).All(n => n > 0 & n < 4))
            {
                safeCount++;
            }
            else
            {
                for (var i = 0; i < numbers.Length; i++)
                {
                    var n1 = numbers[0..i].Concat(numbers[(i+1)..]).ToArray();
                    var n2 = n1[1..];
                    if (n1.Zip(n2, (l, r) => r - l).All(n => n > 0 & n < 4)
                        | n1.Zip(n2, (l, r) => l - r).All(n => n > 0 & n < 4))
                    {
                        safeCount++;
                        break;
                    }
                }
            }
        }
        return safeCount;

    }
}