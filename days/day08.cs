using AOC.util;

namespace AOC.days;

internal class Day08 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "08_t1.txt"), 14);
        AddRun("Part 1", () => RunPart(1, "08.txt"));
        AddRun("Test 2", () => RunPart(2, "08_t1.txt"), 34);
        AddRun("Part 2", () => RunPart(2, "08.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var board = new Board2D<char>(GetListOfLines(inputName), x => x[0]);
        var antennas = new DefaultDictionary<char, HashSet<Coordinate>>();
        foreach (var (key, value) in board.Board)
        {
            if (value != '.') antennas[value].Add(key);
        }
        var antinodes = new HashSet<Coordinate>();
        foreach (var key in antennas.Keys)
        {
            var consider = antennas[key].ToList();
            while (consider.Count > 1)
            {
                var a1 = consider.First();
                consider = consider.Skip(1).ToList();
                foreach (var a2 in consider)
                {
                    var d = a2 - a1;
                    if (part == 1)
                    {
                        if (board.IsOnBoard(a2 + d)) antinodes.Add(a2 + d);
                        if (board.IsOnBoard(a1 - d)) antinodes.Add(a1 - d);
                    }
                    else
                    {
                        var a = a2;
                        while (board.IsOnBoard(a))
                        {
                            antinodes.Add(a);
                            a += d;
                        }
                        a = a1;
                        while (board.IsOnBoard(a))
                        {
                            antinodes.Add(a);
                            a -= d;
                        }
                    }
                }
            }
        }
        
        return antinodes.Count;
    }
}