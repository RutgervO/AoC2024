using AOC.util;

namespace AOC.days;

internal class Day05 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "05_t1.txt"), 143);
        AddRun("Part 1", () => RunPart(1, "05.txt"));
        AddRun("Test 2", () => RunPart(2, "05_t1.txt"), 123);
        AddRun("Part 2", () => RunPart(2, "05.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var beforeMe = new DefaultDictionary<int, HashSet<int>>();
        var afterMe = new DefaultDictionary<int, HashSet<int>>();
        long result = 0;
        foreach (var line in input)
        {
            if (line.Contains('|'))
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                beforeMe[parts[1]].Add(parts[0]);
                afterMe[parts[0]].Add(parts[1]);
            }
            else if (line.Contains(','))
            {
                var pages = line.Split(',').Select(int.Parse).ToArray();
                if (IsValidPageSequence(pages))
                {
                    if (part == 1) result += pages[pages.Length / 2];
                }
                else
                {
                    if (part == 2)
                    {
                        var perm = GetValidOrder([], pages.ToList());
                        result += perm[perm.Count / 2];
                    }
                }
            }
        }
        return result;

        bool IsValidPageSequence(int[] pages)
        {
            var before = new HashSet<int>();
            foreach (var page in pages)
            {
                if (before.Intersect(afterMe[page]).Any()) return false;
                before.Add(page);
            }
            return true;
        }
        
        List<int> GetValidOrder(List<int> solution, List<int> pages)
        {
            if (pages.Count == 0) return solution;
            foreach (var page in pages.Where(page => pages.All(p => p == page | !beforeMe[p].Contains(page))))
            {
                return GetValidOrder(solution.Append(page).ToList(), pages.Where(p => p != page).ToList());
            }
            throw new InvalidOperationException();
        }
    }
}