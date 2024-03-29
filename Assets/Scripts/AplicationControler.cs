using System;
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
    private Button SaveRaportButton;
    [SerializeField]
    private Button ConnectGraphs;
    [SerializeField]
    private GraphRenderer GraphRendererRef;

    private GraphGenerator GraphGenerator;
    private RaportGenerator RaportGenerator;

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
            RaportGenerator = new RaportGenerator(Application.dataPath + @"\raport.txt");
            GraphGenerator = new(vertexCount, ProbabilitySlider.value, minimalSubgraph, minimalSubgraphSize, RaportGenerator);
            GraphRendererRef.CreateVisuals(GraphGenerator.GenerateGraph());
        });
        SaveRaportButton.onClick.AddListener(() =>
        {
            RaportGenerator?.SaveRaport();
        });
        ConnectGraphs.onClick.AddListener(() =>
        {
            int firstGraphVertexID, secondGraphVertexID;
            (firstGraphVertexID, secondGraphVertexID) = GraphGenerator.ConnectGraphs();
        });
    }
}
