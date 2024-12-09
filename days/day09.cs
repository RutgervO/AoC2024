using System.Runtime.InteropServices;

namespace AOC.days;

internal class Day09 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "09_t1.txt"), 1928);
        AddRun("Part 1", () => RunPart(1, "09.txt"));
        AddRun("Test 2", () => RunPart(2, "09_t1.txt"), 2858);
        AddRun("Part 2", () => RunPart(2, "09.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName).Single().ToArray();
        var disk = new List<int>();
        var fileNo = 0;
        var blocks = new List<(int pos, int size, int content)>();
        
        for (var i = 0; i < input.Length; i++)
        {
            var content = i % 2 == 0 ? fileNo++ : -1;
            blocks.Add((disk.Count, input[i] - '0', content));
            for (var j = 0; j < input[i]-'0'; j++) disk.Add(content);
        }

        if (part == 1)
        {
            var defragged = new List<int>();
            var rear = disk.Count - 1;
            for (var i = 0; i <= rear; i++)
            {
                var value = disk[i];
                if (value == -1)
                {
                    value = disk[rear];
                    while (disk[--rear] == -1) ;
                }
                defragged.Add(value);
            }
            return defragged.Select((f, p) => (long)f * p).Sum();
        }

        var p = blocks.Count - 1;
        while (p >= 0)
        {
            var rear = blocks[p];
            if (rear.content != -1)
            {
                for (var i = 0; i < p; i++)
                {
                    var b = blocks[i];
                    if (b.content != -1 | b.size < rear.size) continue;
                        
                    blocks.RemoveAt(p);
                    blocks.RemoveAt(i);
                    rear.pos = b.pos;
                    blocks.Insert(i, rear);
                    if (b.size > rear.size)
                    {
                        blocks.Insert(i + 1, (b.pos + rear.size, b.size - rear.size, -1));
                    }
                    break;
                }
            }
            p--;
        }

        return blocks.Select(Multiplier).Sum();

        long Multiplier((int pos, int size, int content) x)
        {
            long result = 0;
            if (x.content > -1)
            {
                for (var i = 0; i < x.size; i++)
                {
                    result += (x.pos + i) * x.content;
                }
            }
            return result;
        }
    }
}