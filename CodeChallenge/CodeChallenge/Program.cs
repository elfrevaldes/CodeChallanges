using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;



/// <summary>
/// Each edge can have an n number of connection where n < possible edges
/// </summary>
public struct Connection
{
    public static readonly char UnInitialized = '*';
    public char Value { get; private set; }
    public int Weight;
    public Connection(char letter, int weight)
    {
        if (IsValidConnection(letter))
        {
            Value = letter;
            Weight = weight;
        }
        else
        {
            Value = UnInitialized;
            Weight = int.MaxValue;
        }
    }
    private bool IsValidConnection(char letter) => letter >= 'A' && letter <= 'Z';
}

/// <summary>
/// We have edges represented as letters A - Z
/// </summary>
public class Edge
{
    public static readonly char UnInitialized = '*';
    public char Value { get; private set; }

    public List<Connection> connections;

    public Edge(char letter = '*')
    {
        if (IsValidEdge(letter))
        {
            Value = letter;
        }
        else
        {
            Value = UnInitialized;
        }
        connections = new List<Connection>();
    }

    public Edge(int edge) : this((char)edge) { }
    private bool IsValidEdge(char vertex) => vertex >= 'A' && vertex <= 'Z';
}

/*
 *                3
 *          ------> F
 *         /        ^ 7
 *        /   2    /
 *  D    B ---> C 
 * 3^   3^     /|
 *  |   /     / | 
 *  |  /     /  |
 *  | /     /   |
 *  A      /    | 
 *    \   V 2   V 20   
 *   20 > E --> G
 *            2
 */

/// <summary>
///  Choose the Edge with the smalles weight to get to destination
///  If no path to destination no weigth will be display
/// </summary>
public class Dijkstra
{
    private int _maxNumberEdges;
    public int CurrentNumberEdges { get; private set; }

    private Edge[] _edges;
    public Dijkstra(int numberVertex, bool isBidirectional = false)
    {
        // we need space to store backwards directions
        _maxNumberEdges = isBidirectional ? numberVertex * 2 : numberVertex;

        CurrentNumberEdges = 0;
        // No adjacent node to begin with
        _edges = new Edge[_maxNumberEdges];
        // create vertexis
        for (int i = 0; i < _maxNumberEdges; i++)
        {
            _edges[i] = new Edge((char)(i + 'A'));
            CurrentNumberEdges++;
        }
    }

    /// <summary>
    /// Internal use to keep track of paths
    /// </summary>
    private struct ShortestPath
    {
        public List<Stack<Connection>> path;
        public int pathsFound;
        public int bestPathIndex;
        public int bestMax;
        public void MakeCopy(int indexOfCopy)
        {
            Stack<Connection> stack = new Stack<Connection>(path[indexOfCopy]);
            // fix the order
            path.Add(new Stack<Connection>(stack));
        }
        public ShortestPath()
        {
            path = new List<Stack<Connection>>()
            {
                new Stack<Connection>()
            };
            bestPathIndex = 0;
            bestMax = int.MaxValue;
        }
    }

    private bool IsValidEdge(char vertex) => vertex >= 'A' && vertex <= 'Z';

    public void AddConnection(char edge, Connection connection)
    {
        if (IsValidEdge(edge))
        {
            if ((connection.Value - 'A') < _maxNumberEdges)
            {
                if (connection.Value != Connection.UnInitialized)
                {
                    AddConnection(edge - 'A', connection);
                }
                else
                {
                    Console.WriteLine($"{connection.Value} is not a initialized as connection");
                }
            }
            else
                Console.WriteLine($"{connection.Value} is not a valid connection");
        }
        else
        {
            Console.WriteLine($"{edge} is not a valid edge to add a connection");
        }

    }
    public void AddConnection(int edge, Connection connection)
    {
        if (edge >= 0 && edge < _maxNumberEdges)
        {
            // Do we have that connection?
            if (!_edges[edge].connections.Contains(connection))
                _edges[edge].connections.Add(connection);
            else
                Console.WriteLine($"Edge: {(char)(edge + 'A')} already has a Connection to: {connection.Value}");
        }
        else
        {
            Console.WriteLine($"{edge} is not a valid edge to add a connection");
        }
    }

    public void Display(char edgeIndex)
    {
        if (IsValidEdge(edgeIndex) && _edges[edgeIndex - 'A'].Value != Edge.UnInitialized)
        {
            Console.WriteLine($"Edge: {edgeIndex} is connected to:");
            foreach (Connection con in _edges[edgeIndex - 'A'].connections)
            {
                Console.WriteLine($"{con.Value} with a weight of: {con.Weight}");
            }
            if (_edges[edgeIndex - 'A'].connections.Count == 0)
            {
                Console.WriteLine($"Nothing, yet");
            }
        }
        else if (IsValidEdge(edgeIndex) && _edges[edgeIndex - 'A'].Value == Edge.UnInitialized)
        {
            Console.WriteLine($"Edge: {edgeIndex} has not been initialized");
        }
        else
        {
            Console.WriteLine($"Edge: {edgeIndex} is invalid");
        }
        Console.WriteLine();
    }

    public void DisplayAllEdges()
    {
        for (int i = 0; i < _edges.Length; i++)
        {
            Display((char)('A' + i));
        }
    }

    public Edge GetEdgeByIndex(int index)
    {
        if (index >= CurrentNumberEdges)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            return _edges[index];
        }
    }

    public Edge GetEdgeByIndex(char index)
    {
        if (IsValidEdge(index) && index - 'A' <= CurrentNumberEdges)
        {
            return _edges[index - 'A'];
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }

    private bool RecursiveVisitation(Dictionary<char, int>[] edgesVisited, ref ShortestPath sPath, char currentEdge, char dest, int currentWeight)
    {
        Edge cEdge = GetEdgeByIndex(currentEdge);
        if (currentEdge == dest)
        {
            return true;
        }

        foreach (Connection con in cEdge.connections)
        {
            // I have not visited this edge visit
            if (!edgesVisited[currentEdge - 'A'].ContainsKey(con.Value))
            {
                sPath.path[sPath.pathsFound].Push(con);
                if (RecursiveVisitation(edgesVisited, ref sPath, con.Value, dest, currentWeight + con.Weight))
                {
                    // copy path for next possible path and move to next path
                    sPath.MakeCopy(sPath.pathsFound);

                    sPath.pathsFound++;
                    // check if this is best path
                    if (con.Weight + currentWeight < sPath.bestMax)
                    {
                        sPath.bestMax = con.Weight + currentWeight;
                        sPath.bestPathIndex = sPath.pathsFound - 1;
                        sPath.path[sPath.pathsFound].Pop();
                    }
                }
                else
                {
                    if (sPath.path[sPath.pathsFound].Peek().Value != dest)
                        sPath.path[sPath.pathsFound].Pop();
                }
                edgesVisited[currentEdge - 'A'].Add(con.Value, con.Weight + currentWeight);
            }
        }

        return false;
    }

    public void FindShortestPath(char origin, char destination)
    {
        if (!IsValidEdge(origin) || !IsValidEdge(destination))
        {
            Console.WriteLine($"{origin} or {destination} is not a valid");
            return;
        }

        // keep track of things to not visit them twice
        Dictionary<char, int>[] edgesVisited = new Dictionary<char, int>[CurrentNumberEdges];
        for (int i = 0; i < CurrentNumberEdges; i++)
        {
            edgesVisited[i] = new Dictionary<char, int>();
        }

        char currentEdge = origin;
        int currentWeight = 0;

        ShortestPath shortestPath = new ShortestPath();
        RecursiveVisitation(edgesVisited, ref shortestPath, origin, destination, currentWeight);

        if (shortestPath.pathsFound > 0)
        {
            Console.WriteLine($"From {origin} to {destination}");
            Console.Write($"{origin} -> ");
            // reverse them because of stack order
            Stack<Connection> connections = new Stack<Connection>(shortestPath.path[shortestPath.bestPathIndex]);
            while (connections.Count > 0)
            {
                var con = connections.Pop();
                Console.WriteLine($"{con.Value} weight: {con.Weight}");


                if (connections.Count > 0)
                {
                    Console.Write(con.Value);
                    Console.Write(" -> ");
                }
            }
            Console.WriteLine($"With a total path weight: {shortestPath.bestMax}");
        }
        else
        {
            Console.WriteLine($"there is no connection from {origin} to {destination}:");
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Create a graph
        Dijkstra graph = new Dijkstra(7);

        // Add edges and weights
        graph.AddConnection('A', new Connection('D', 3));
        graph.AddConnection('A', new Connection('B', 3));
        graph.AddConnection('A', new Connection('E', 20));

        graph.AddConnection('B', new Connection('C', 2));
        graph.AddConnection('B', new Connection('F', 3));

        graph.AddConnection('C', new Connection('F', 7));
        graph.AddConnection('C', new Connection('G', 20));
        graph.AddConnection('C', new Connection('E', 2));

        graph.AddConnection('E', new Connection('G', 2));

        // graph.DisplayAllEdges();
        graph.FindShortestPath('A', 'F');
    }
}