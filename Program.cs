
using System.Runtime.Intrinsics.Arm;

int n = 8;
float p = 0.2f;
var T = new List<int>();
var grafy = new List<List<int>>();

do
{
    grafy.Clear();
    Console.Clear();
    int[,] A = new int[n, n];
    for (int i = 0; i < n; i++)
        T.Add(i + 1);
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            Random r = new();
            double x = r.NextDouble();
            if (x < p)
            {
                A[i, j] = 1;
                A[j, i] = 1;
            }
        }

    }
    WypiszMacierz(A);
    do
    {
        var graf1 = new List<int>();
        Wszerz(T[0] - 1, n, A, graf1);
        WypiszGraf(graf1);
        WypiszMacierz(A);
        graf1.ForEach(x => T.Remove(x));
        grafy.Add(graf1);
    }
    while (T.Count > 0);
}
while (grafy.Count < 3);
void WypiszMacierz(int[,] A)
{
    Console.Write("   ");
    for (int i = 0; i < n; i++)
        Console.Write(i + 1 + " ");

    Console.WriteLine();
    for (int i = 0; i < n; i++)
    {
        Console.Write(i + 1 + ": ");
        for (int j = 0; j < n; j++)
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
    graf.ForEach(x => Console.Write(x + " "));
    Console.WriteLine();
}
void Wszerz(int i, int n, int[,] A, List<int> graf)
{
    graf.Add(i + 1);
    for (int j = 0; j < n; j++)//wracam do tego samego wierzcholka kilka razys
    {
        if (A[i, j] == 1)
        {
            A[i, j] = 0;
            A[j, i] = 0;
            Wszerz(j, n, A, graf);
        }

    }
}