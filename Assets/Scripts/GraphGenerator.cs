using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GraphGenerator
{
    private int VertexCount;
    private float EdgePossibility;
    private int MinimalSubgraphCount;
    private int MinimalSubgraphSize;
    private List<int> AvailableVerticles = new();
    private List<List<int>> Graphs = new();
    private List<Edge> Edges = new();
    private int[,] A;
    private RaportGenerator RaportGenerator;

    public GraphGenerator(int vertexCount, float edgePossibility, int minimalSubgraphCount, int minimalSubgraphSize, RaportGenerator raportGenerator = null)
    {
        VertexCount = vertexCount;
        EdgePossibility = edgePossibility;
        MinimalSubgraphCount = minimalSubgraphCount;
        MinimalSubgraphSize = minimalSubgraphSize;
        RaportGenerator = raportGenerator;
    }

#nullable enable
    public int[,]? GenerateGraph()
    {
        int iterations = 0;

        do
        {
            Graphs.Clear();
            Edges.Clear();
            AvailableVerticles.Clear();
            iterations++;
            if (iterations > 1000)
            {
                Debug.LogError("Exceeded generator iterations limit (more than 1000 iterations)");
                A = null;
                return null;
            }

            AvailableVerticles.AddRange(Enumerable.Range(0, VertexCount));
            A = GenerateMatrix();
            BreadthFirstSearch(A);
        }
        while (ValidateGraphRequirements());

        RaportGenerator?.InitializeRaport(iterations, EdgePossibility);
        RaportGenerator?.AddBasicInformationsToRaport(A, Edges);

        return A;
    }
    public Vector2Int? ConnectGraphs()
    {
        if (Graphs.Count < 2) return null;

        var firstGraphVertexID = Graphs[0][UnityEngine.Random.Range(0, Graphs[0].Count)];
        var secondGraphVertexID = Graphs[1][UnityEngine.Random.Range(0, Graphs[1].Count)];

        Edges.Add(new Edge { V1 = firstGraphVertexID, V2 = secondGraphVertexID });
        A[firstGraphVertexID, secondGraphVertexID] = 1;
        A[secondGraphVertexID, firstGraphVertexID] = 1;
        Graphs[0].AddRange(Graphs[1]);
        Graphs.RemoveAt(1);

        RaportGenerator?.AddConnectedVerticesToRaport(firstGraphVertexID, secondGraphVertexID);
        RaportGenerator?.AddBasicInformationsToRaport(A, Edges);

        return new Vector2Int(firstGraphVertexID, secondGraphVertexID);
    }

    private bool ValidateGraphRequirements() => (Graphs.Count < MinimalSubgraphCount) || !Graphs.All(graf => graf.Count >= MinimalSubgraphSize);

    private int[,] GenerateMatrix()
    {
        int[,] A = new int[VertexCount, VertexCount];
        for (int i = 0; i < VertexCount - 1; i++)
        {
            for (int j = i + 1; j < VertexCount; j++)
            {
                float randomValue = UnityEngine.Random.Range(0.0f, 1f);
                if (randomValue < EdgePossibility)
                {
                    A[i, j] = 1;
                    A[j, i] = 1;
                }
            }
        }
        return A;
    }

    private void BreadthFirstSearch(int[,] A)
    {
        do
        {
            var subgraph = new List<int>();
            FindSubgraph(AvailableVerticles[0], VertexCount, A, subgraph);
            subgraph.ForEach(x => AvailableVerticles.Remove(x));
            Graphs.Add(subgraph);
        }
        while (AvailableVerticles.Count > 0);
    }

    private void FindSubgraph(int i, int n, int[,] A, List<int> graf)
    {
        graf.Add(i);
        for (int j = 0; j < n; j++)
            if (A[i, j] == 1 && !graf.Any(w => w == j))
            {
                Edges.Add(new Edge { V1 = i, V2 = j });
                FindSubgraph(j, n, A, graf);
            }
    }
}