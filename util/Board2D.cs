namespace AOC.util;

public class Board2D<T> where T : IFormattable, IComparable, new()
{
    public DefaultDictionary<Coordinate, T> Board;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool DiagonalNeighbours { get; }
    public (int dx, int dy)[] NeighbourDeltas { get; set; }

    public Board2D(IEnumerable<string> lines, Func<string, T> parser, bool diagonalNeighbours = true)
    {
        var input = lines.ToArray();
        Width = input.First().Length;
        Height = input.Length;
        Board = new DefaultDictionary<Coordinate, T>();
        foreach (var coordinate in AllCoordinates())
            Board[coordinate] = parser(input[coordinate.Y][coordinate.X].ToString());
        DiagonalNeighbours = diagonalNeighbours;
        NeighbourDeltas = DiagonalNeighbours
            ? new (int dx, int dy)[] {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)}
            : new (int dx, int dy)[] {(0, 1), (1, 0), (0, -1), (-1, 0)};
    }

    public void SetDimensions(int width, int height)
    {
        Width = width;
        Height = height;
        // ToDo remove any coordinates that are out of bounds now
    }
    
    public T Get(Coordinate coordinate)
    {
        return Board[coordinate];
    }

    public T this[Coordinate coordinate]
    {
        get => Board[coordinate];
        set => Board[coordinate] = value;
    }
    
    public IEnumerable<Coordinate> AllCoordinates()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                yield return new Coordinate(x, y);
            }
        }
    }

    public void ApplyFunctionToAllCells(Func<T, T> f)
    {
        foreach (var coordinate in AllCoordinates())
            Board[coordinate] = f(Board[coordinate]);
    }

    public IEnumerable<Coordinate> CoordinatesThatMatch(Func<T, bool> f)
    {
        return AllCoordinates().Where(coordinate => f(Board[coordinate]));
    }

    public bool IsOnBoard(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < Width && coordinate.Y >= 0 && coordinate.Y < Height;
    }

    public bool IsOnEdge(Coordinate coordinate)
    {
        return coordinate.X == 0 || coordinate.X + 1 == Width || coordinate.Y == 0 || coordinate.Y + 1 == Height;
    }

    public IEnumerable<Coordinate> Neighbours(Coordinate coordinate, bool checkOnBoard = true)
    {
        foreach (var delta in NeighbourDeltas)
        {
            var neighbour = coordinate.Add(new Coordinate(delta));
            if (!checkOnBoard || IsOnBoard(neighbour))
                yield return neighbour;
        }
    }

    public IEnumerable<Coordinate> Neighbours(IEnumerable<Coordinate> coordinates, bool checkOnBoard = true)
    {
        return coordinates.SelectMany(x => Neighbours(x, checkOnBoard));
    }

    public void ApplyValueToCoordinates(T value, IEnumerable<Coordinate> coordinates)
    {
        foreach (var coordinate in coordinates)
            Board[coordinate] = value;
    }

    public void ApplyFunctionToCoordinates(Func<T, T> f, IEnumerable<Coordinate> coordinates)
    {
        foreach (var coordinate in coordinates)
            Board[coordinate] = f(Board[coordinate]);
    }

    public IEnumerable<T> AllValues()
    {
        return AllCoordinates().Select(coordinate => Board[coordinate]);
    }

    public bool FindLargerOrEqualValue(Coordinate coordinate, Direction direction)
    {
        var value = Get(coordinate);
        var delta = direction.ToCoordinate();
        while (true)
        {
            coordinate = coordinate.Add(delta);
            if (!IsOnBoard(coordinate))
                return false;
            if (Get(coordinate).CompareTo(value) >= 0)
                return true;
        }
    }

    public int GetDistanceToLargerOrEqualValue(Coordinate coordinate, Direction direction)
    {
        var value = Get(coordinate);
        var delta = direction.ToCoordinate();
        var distance = 0;
        while (true)
        {
            coordinate = coordinate.Add(delta);
            if (!IsOnBoard(coordinate))
                return distance;
            distance += 1;
            if (Get(coordinate).CompareTo(value) >= 0)
                return distance;
        }
    }

    public IEnumerable<string> GetStringsOfLength(Coordinate coordinate, int length)
    {
        foreach (var d in NeighbourDeltas)
        {
            var value = "" + Board[coordinate];
            var c = coordinate;
            for (var i = 1; i < length; i++)
            {
                c += d;
                if (!IsOnBoard(c)) break;
                value += Board[c];
            }
            if (value.Length == length) yield return value;
        }
    }
    
    public void Print(string? title = null, Func<T, bool>? highlight = null)
    {
        if (title is not null)
        {
            Console.Out.WriteLine(title);
        }

        var displaySize = AllValues().Max()!.ToString()!.Length;

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var value = Board[new Coordinate(x, y)];
                var highlighted = highlight is not null && highlight(value);
                if (highlighted)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Out.Write(value.ToString()!.PadLeft(displaySize));
                if (highlighted)
                {
                    Console.ResetColor();
                }

                if (displaySize > 1 && x < Width - 1)
                    Console.Out.Write(" ");
            }

            Console.Out.WriteLine("");
        }
    }
}