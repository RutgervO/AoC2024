using AOC.util;

namespace AOC.days;

internal class Day10 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "10_t1.txt"), 36);
        AddRun("Part 1", () => RunPart(1, "10.txt"));
        AddRun("Test 2", () => RunPart(2, "10_t1.txt"), 81);
        AddRun("Part 2", () => RunPart(2, "10.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var board = new Board2D<int>(GetListOfLines(inputName), int.Parse, false);
        return board.Board
            .Where(kvp => kvp.Value == 0)
            .Select(kvp => part == 1
                ? GetSummits(kvp.Key, 0).Distinct().Count()
                : GetSummits(kvp.Key, 0).Count())
            .Sum();

        IEnumerable<Coordinate> GetSummits(Coordinate start, int value)
        {
            if (value++ == 9) yield return start;
            else
            {
                foreach (var s in board.Neighbours(start)
                             .Where(c => board[c] == value)
                             .SelectMany(c => GetSummits(c, value)))
                {
                    yield return s;
                }
            }
        }
    }
}