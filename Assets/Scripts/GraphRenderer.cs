using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    [SerializeField]
    private Collider2D MenuCollider;
    [SerializeField]
    private GameObject GraphVisualObjectPrefab;

    private List<GraphVisualObject> GraphVisualObjects = new();

    public void RemoveVisuals()
    {
        foreach (var visualObject in GraphVisualObjects)
        {
            Destroy(visualObject.gameObject);
        }
        GraphVisualObjects = new();
    }

    public void CreateVisuals(int[,] graph)
    {
        if (GraphVisualObjects.Count != 0)
        {
            Debug.LogError("Visuals are already created!");
            return;
        }

        SpawnGraphVerticies(graph);
        SetGraphConnections(graph);
    }

    public void AddConnection(int index1, int index2)
    {
        GraphVisualObjects[index1].AddConnection(GraphVisualObjects[index2].gameObject);
    }

    private void SpawnGraphVerticies(int[,] graph)
    {
        int graphWidth = graph.GetLength(0);

        for (int w = 0; w < graphWidth; w++)
        {
            Vector2 randomPosition = CameraControler.CameraControlerInstance.GetRandomPositionInCameraView();
            while (MenuCollider.OverlapPoint(randomPosition)) randomPosition = CameraControler.CameraControlerInstance.GetRandomPositionInCameraView();

            var graphVisualObject = Instantiate(GraphVisualObjectPrefab, randomPosition, Quaternion.identity, transform).GetComponent<GraphVisualObject>();
            graphVisualObject.Index = w + 1;
            GraphVisualObjects.Add(graphVisualObject);
        }
    }

    private void SetGraphConnections(int[,] graph)
    {
        int graphWidth = graph.GetLength(0);
        int graphHeight = graph.GetLength(1);

        for (int w = 0; w < graphWidth; w++)
        {
            for (int h = 0; h < graphHeight; h++)
            {
                if (graph[w, h] == 1) GraphVisualObjects[w].AddConnection(GraphVisualObjects[h].gameObject);
                if (w == h) break;
            }
        }
    }
}
