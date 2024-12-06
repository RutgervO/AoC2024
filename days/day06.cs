using System.Runtime.InteropServices;
using AOC.util;

namespace AOC.days;

internal class Day06 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "06_t1.txt"), 41);
        AddRun("Part 1", () => RunPart(1, "06.txt"));
        AddRun("Test 2", () => RunPart(2, "06_t1.txt"), 6);
        AddRun("Part 2", () => RunPart(2, "06.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var board = new Board2D<char>(input, x => x[0], false);
        var start = board.Board.Where(x => x.Value == '^').Select(x => x.Key).Single();
        var d = 0;
        Direction[] directions = [new Direction("N"), new Direction("E"), new Direction("S"), new Direction("W")];
        var visited = new HashSet<Coordinate>();
        var c = start;
        while (board.IsOnBoard(c))
        {
            visited.Add(c);
            var nc = c + directions[d];
            if (board[nc] == '#') d = (d + 1) % 4;
            else c = nc;
        }
        if (part == 1) return visited.Count;

        long result = 0;
        foreach (var v in visited)
        {
            board[v] = '#';
            if (IsLoop())
            {
                result++;
            }
            board[v] = '.';
        }
        return result;

        bool IsLoop()
        {
            var visitedDirections = new DefaultDictionary<Coordinate, HashSet<int>>();
            var c = start;
            var d = 0;
            while (board.IsOnBoard(c) && !visitedDirections[c].Contains(d))
            {
                visitedDirections[c].Add(d);
                var nc = c + directions[d];
                if (board[nc] == '#') d = (d + 1) % 4;
                else c = nc;
            }

            return board.IsOnBoard(c);
        }
    }
}