using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GraphGenerator
{
    int VertexCount = 12;
    float EdgePossibility = 0.2f;
    int MinimalSubgraphCount = 3;
    int MinimalSubgraphSize = 2;
    List<int> AvailableVerticles = new();
    List<List<int>> Graphs = new();
    //Move commented logic to other functions!
    public GraphGenerator(int vertexCount, float edgePossibility, int minimalSubgraphCount, int minimalSubgraphSize)
    {
        VertexCount = vertexCount;
        EdgePossibility = edgePossibility;
        MinimalSubgraphCount = minimalSubgraphCount;
        MinimalSubgraphSize = minimalSubgraphSize;
    }
    public int[,]? GenerateGraph()
    {
        int[,] B;
        int iterations = 0;

        do
        {
            iterations++;
            if (iterations > 1000)
            {
                Debug.LogError("Exceeded generator iterations limit (more than 1000 iterations)");
                return null;
            }
            Graphs.Clear();
            int[,] A = new int[VertexCount, VertexCount];
            for (int i = 0; i < VertexCount; i++)
                AvailableVerticles.Add(i);
            for (int i = 0; i < VertexCount - 1; i++)
            {
                for (int j = i + 1; j < VertexCount; j++)
                {
                    float x = UnityEngine.Random.Range(0.0f, 1f);
                    if (x < EdgePossibility)
                    {
                        A[i, j] = 1;
                        A[j, i] = 1;
                    }
                }
            }
            B = Copy2DArray(A);
            do
            {
                var graf1 = new List<int>();
                Wszerz(AvailableVerticles[0], VertexCount, A, graf1);
                WypiszGraf(graf1);
                graf1.ForEach(x => AvailableVerticles.Remove(x));
                Graphs.Add(graf1);
            }
            while (AvailableVerticles.Count > 0);
        }
        while ((Graphs.Count < MinimalSubgraphCount) 
        || !Graphs.All(graf => graf.Count >= MinimalSubgraphSize));
        Debug.Log(iterations);
        return B;
    }


    void WypiszMacierz(int[,] A)
    {
        Console.Write("   ");
        for (int i = 0; i < VertexCount; i++)
            Console.Write(i + 1 + " ");

        Console.WriteLine();
        for (int i = 0; i < VertexCount; i++)
        {
            Console.Write(i + 1 + ": ");
            for (int j = 0; j < VertexCount; j++)
                if (i != j)
                    Console.Write(A[i, j] + " ");
                else
                    Console.Write("X ");

            Console.WriteLine();
        }
        Console.WriteLine();
    }

    void WypiszGraf(List<int> graf)
    {
        Console.WriteLine("Graf: ");
        graf.ForEach(x => Console.Write(x + 1 + " "));
        Console.WriteLine();
    }

    void Wszerz(int i, int n, int[,] A, List<int> graf)
    {
        graf.Add(i);
        for (int j = 0; j < n; j++)
        {
            if (A[i, j] == 1 && !graf.Any(w => w == j))
            {
                A[i, j] = 0;
                A[j, i] = 0;
                Wszerz(j, n, A, graf);
            }

        }
    }
    int[,] Copy2DArray(int[,] A)
    {
        var B = new int[A.GetLength(0),A.GetLength(1)];
        for (int i = 0; i < B.GetLength(0); i++)
            for (int j = 0; j < B.GetLength(1); j++) 
                B[i, j] = A[i, j];
        return B;
    }
}