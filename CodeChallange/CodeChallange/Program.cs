using System.Xml.Linq;

public class Node<T>
{
    public T Data;
    public List<Edge<T>> Edges;

    public Node(T data)
    {
        Data = data;
        Edges = new List<Edge<T>>();
    }
}

public class Edge<T>
{
    public Node<T> Source;
    public Node<T> Target;

    public Edge(Node<T> source, Node<T> target)
    {
        Source = source;
        Target = target;
    }
}

public class Graph<T>
{
    public List<Node<T>> Nodes;

    public Graph()
    {
        Nodes = new List<Node<T>>();
    }

    public void AddNode(T data)
    {
        Nodes.Add(new Node<T>(data));
    }

    public void AddEdge(T sourceData, T targetData)
    {
        Node<T>? sourceNode = FindNode(sourceData);
        Node<T>? targetNode = FindNode(targetData);

        if (sourceNode != null && targetNode != null)
        {
            Edge<T> newEdge = new Edge<T>(sourceNode, targetNode);
            if (!sourceNode.Edges.Contains(newEdge))
                sourceNode.Edges.Add(newEdge);
            else
                Console.WriteLine($"Node: {sourceNode.Data} already has edge with: {targetNode.Data}");
        }
        else
        {
            if (sourceNode == null && targetNode == null)
                Console.WriteLine($"Source: {sourceData} and targe: {targetData} are not present in the graph");
            else if (sourceNode == null)
                Console.WriteLine($"Source: {sourceData} is not present in the graph");
            else
                Console.WriteLine($"Targe: {targetData} is not present in the graph");
        }
    }

    private Node<T>? FindNode(T data)
    {
        // Nodes is a list of Node. Node is initialized 
        return Nodes.Find(node => node.Data!.Equals(data));
    }

    public void Display()
    {
        foreach (var node in Nodes)
        {
            Console.Write(node.Data + ": ");
            int edgeCount = node.Edges.Count;
            foreach (var edge in node.Edges)
            {
                //if(edge.Target.Data !=)
                Console.Write(edge.Target.Data);
                if (--edgeCount > 0)
                    Console.Write(", ");
            }
            if (node.Edges.Count == 0)
            {
                Console.Write("null");
            }
            Console.WriteLine();
        }
    }
    /// <summary>
    /// This method will run in O(N+E)
    /// </summary>
    /// <param name="data"></param>
    public void DFSIterative(T data)
    {
        Node<T>? sourceNode = FindNode(data);
        if(sourceNode == null || sourceNode.Data == null)
            return;

        HashSet<T> visited = new HashSet<T>();
        Stack<Node<T>> edgesToVisit = new Stack<Node<T>>();
        edgesToVisit.Push(sourceNode);

        do
        {
            Node<T>? node = edgesToVisit.Pop();
            if (node != null && !visited.Contains(node.Data))
            {
                Console.Write(node.Data + " -> ");
                visited.Add(node.Data);
                foreach (var edge in node.Edges)
                {
                    //Console.Write(edge.Target.Data + " ");
                    if (!visited.Contains(edge.Target.Data))
                    {
                        edgesToVisit.Push(edge.Target);
                    }
                }
            }
        } while (edgesToVisit.Count > 0);
        Console.WriteLine("null");
    }

    private void DFSRecursion(Node<T> node, ref HashSet<T> visited)
    {
        if (node == null || node.Data == null || visited.Contains(node.Data))
            return;

        visited.Add(node.Data);
        Console.Write(node.Data + " -> ");
        foreach (var edge in node.Edges)
        {
            DFSRecursion(edge.Target, ref visited);
        }
        
    }

    public void DFSRecursive(T data)
    {
        Node<T>? sourceNode = FindNode(data);
        if (sourceNode == null || sourceNode.Data == null)
            return;

        HashSet<T> visited = new HashSet<T>();
        DFSRecursion(sourceNode, ref visited);
        Console.WriteLine("null");
    }
}

public class Program
{
    public static void Main()
    {     
        Graph<int> graph = new Graph<int>();
        int nodeAmount = 4;
        for (int i = 0; i <= nodeAmount; i++) 
        {
            graph.AddNode(i);
        }

        //// Bad source
        // graph.AddEdge(10, 2);
        //// Bad target
        // graph.AddEdge(0, 12);
        //// both wrong
        // graph.AddEdge(10, 12);


        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(3, 0);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 4);

        // graph.Display();
        // graph.DFSIterative(0);
        graph.DFSRecursive(0);
        Console.WriteLine("Done");
    }
}