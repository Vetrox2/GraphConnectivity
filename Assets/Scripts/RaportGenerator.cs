using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RaportGenerator : ScriptableObject
{
    public string Path;

    private string Raport;

    public RaportGenerator(string path)
    {
        Path = path;
    }

    public void InitializeRaport(int iterations, float EdgePossibility)
    {
        Raport = $"EdgePossibility = {EdgePossibility}\nIterations = {iterations}\n";
    }

    public void SaveRaport()
    {
        File.WriteAllText(Path, Raport);
        Debug.Log(Raport);
    }

    public void AddBasicInformationsToRaport(int[,] A, List<Edge> edges)
    {
        Raport += ReadMatrix(A);
        Raport += ReadDegrees(A);
        Raport += ReadEdges(edges);
    }

    public void AddConnectedVerticesToRaport(int id1, int id2)
    {
        Raport += $"\nConnected vertices: {id1 + 1} with {id2 + 1}\n";
    }

    private string ReadEdges(List<Edge> edges)
    {
        var edgesStr = "E = {";
        edges.ForEach(E => edgesStr += $"({E.V1 + 1},{E.V2 + 1}), ");
        edgesStr = edgesStr.Remove(edgesStr.Length - 2);
        edgesStr += "}\n";
        return edgesStr;
    }

    private string ReadDegrees(int[,] A)
    {
        var Degrees = new List<int>();
        for (int i = 0; i < A.GetLength(0); i++)
        {
            var degree = 0;
            for (int j = 0; j < A.GetLength(1); j++)
                if (A[i, j] == 1)
                    degree++;
            Degrees.Add(degree);
        }
        Degrees.Sort();
        var degreesStr = "Ci¹g stopni = (";
        Degrees.ForEach(degree => degreesStr += $"{degree}, ");
        degreesStr = degreesStr.Remove(degreesStr.Length - 2);
        degreesStr += ")\n";
        return degreesStr;
    }

    private string ReadMatrix(int[,] A)
    {
        var matrix = "\nAdjacency matrix:\n";
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
                if (i != j)
                    matrix += A[i, j] + " ";
                else
                    matrix += "X ";

            matrix += "\n";
        }
        matrix += "\n";
        return matrix;
    }
}
