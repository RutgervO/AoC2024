using AOC.util;
namespace AOC.days;

internal class Day04 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "04_t1.txt"), 18);
        AddRun("Part 1", () => RunPart(1, "04.txt"));
        AddRun("Test 2", () => RunPart(2, "04_t1.txt"), 9);
        AddRun("Part 2", () => RunPart(2, "04.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var board = new Board2D<char>(input, x => x[0]);
        
        if (part == 1)
        {
            return board.AllCoordinates().Select(c => board.GetStringsOfLength(c, 4).Count(s => s == "XMAS")).Sum();
        }

        return board.AllCoordinates().Count(IsMasMas);
        
        bool IsMasMas(Coordinate c)
        {
            if (board[c] != 'A') return false;
            var d1 = new HashSet<char> { board[c - (1, 1)], board[c + (1, 1)] };
            var d2 = new HashSet<char> { board[c - (1, -1)], board[c + (1, -1)] };
            var ms = new HashSet<char> { 'M', 'S' };
            return d1.SetEquals(ms) & d2.SetEquals(ms);
        }
    }
}