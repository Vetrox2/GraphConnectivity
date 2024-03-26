using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AplicationControler : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField VertexCountField;
    [SerializeField]
    private TMPro.TMP_InputField MinimalSubgraphField;
    [SerializeField]
    private TMPro.TMP_InputField MinimalSubgraphSizeField;
    [SerializeField]
    private Slider ProbabilitySlider;
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private Button TempGenerateButton;
    [SerializeField]
    private GraphRenderer GraphRendererRef;

    void Start()
    {
        TempGenerateButton.onClick.AddListener(() =>
        {
            GraphRendererRef.RemoveVisuals();
            GraphGenerator graphGenerator = new(20, 0.15f, 2, 2);
            GraphRendererRef.CreateVisuals(graphGenerator.GenerateGraph());
        });
        StartButton.onClick.AddListener(() =>
        {
            GraphRendererRef.RemoveVisuals();
            var vertexCount = Convert.ToInt32(VertexCountField.text);
            var minimalSubgraph = Convert.ToInt32(MinimalSubgraphField.text);
            var minimalSubgraphSize = Convert.ToInt32(MinimalSubgraphSizeField.text);
            GraphGenerator graphGenerator = new(vertexCount, ProbabilitySlider.value, minimalSubgraph, minimalSubgraphSize);
            GraphRendererRef.CreateVisuals(graphGenerator.GenerateGraph());
        });
    }
}
