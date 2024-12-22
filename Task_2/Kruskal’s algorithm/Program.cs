using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int Src { get; set; }
    public int Dest { get; set; }
    public int Weight { get; set; }

    public Edge(int src, int dest, int weight)
    {
        Src = src;
        Dest = dest;
        Weight = weight;
    }

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}

class DisjointSet
{
    private int[] parent, rank;

    public DisjointSet(int n)
    {
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++) parent[i] = i;
    }

    public int Find(int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent[x]); // Path compression
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX == rootY) return;

        if (rank[rootX] > rank[rootY])
            parent[rootY] = rootX;
        else if (rank[rootX] < rank[rootY])
            parent[rootX] = rootY;
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }
}

class KruskalMST
{
    public static List<Edge> FindMST(int vertices, List<Edge> edges)
    {
        List<Edge> mst = new List<Edge>();
        DisjointSet ds = new DisjointSet(vertices);

        edges.Sort(); // Step 1: Sort edges by weight

        foreach (var edge in edges)
        {
            int srcRoot = ds.Find(edge.Src);
            int destRoot = ds.Find(edge.Dest);

            if (srcRoot != destRoot)
            {
                mst.Add(edge); // Add edge to MST
                ds.Union(srcRoot, destRoot); // Merge sets
            }

            if (mst.Count == vertices - 1) break; // Stop when MST is complete
        }

        return mst;
    }

    static void Main()
    {
        int vertices = 4;
        List<Edge> edges = new List<Edge>
        {
            new Edge(0, 1, 10),
            new Edge(0, 2, 6),
            new Edge(0, 3, 5),
            new Edge(1, 3, 15),
            new Edge(2, 3, 4)
        };

        List<Edge> mst = FindMST(vertices, edges);

        Console.WriteLine("Edges in the MST:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"({edge.Src}, {edge.Dest}) with weight {edge.Weight}");
        }
    }
}
