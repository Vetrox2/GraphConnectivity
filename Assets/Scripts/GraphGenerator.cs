using System;
using System.Collections.Generic;
using System.Linq;

class GraphGenerator
{
    int VerticlesCount = 12;
    float EdgePossibility = 0.2f;
    List<int> AvailableVerticles = new();
    List<List<int>> Graphs = new();

    //Move commented logic to other functions!
    public int[,] GenerateGraph()
    {


        //do
        //{
            Graphs.Clear();
            Console.Clear();
            int[,] A = new int[VerticlesCount, VerticlesCount];
            for (int i = 0; i < VerticlesCount; i++)
                AvailableVerticles.Add(i);
            for (int i = 0; i < VerticlesCount - 1; i++)
            {
                for (int j = i + 1; j < VerticlesCount; j++)
                {
                    Random r = new();
                    double x = r.NextDouble();
                    if (x < EdgePossibility)
                    {
                        A[i, j] = 1;
                        A[j, i] = 1;
                    }
                }

            }
            return A;
            //WypiszMacierz(A);
        //    do
        //    {
        //        var graf1 = new List<int>();
        //        Wszerz(AvailableVerticles[0], VerticlesCount, A, graf1);
        //        WypiszGraf(graf1);
        //        graf1.ForEach(x => AvailableVerticles.Remove(x));
        //        Graphs.Add(graf1);
        //    }
        //    while (AvailableVerticles.Count > 0);
        //}
        //while ((Graphs.Count < 3 || Graphs.Count > 5) || !Graphs.All(graf => graf.Count > 1));
    }


    void WypiszMacierz(int[,] A)
    {
        Console.Write("   ");
        for (int i = 0; i < VerticlesCount; i++)
            Console.Write(i + 1 + " ");

        Console.WriteLine();
        for (int i = 0; i < VerticlesCount; i++)
        {
            Console.Write(i + 1 + ": ");
            for (int j = 0; j < VerticlesCount; j++)
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
}