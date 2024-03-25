using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AplicationControler : MonoBehaviour
{
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private GraphRenderer GraphRendererRef;

    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            GraphRendererRef.RemoveVisuals();
            GraphGenerator graphGenerator = new();
            GraphRendererRef.CreateVisuals(graphGenerator.GenerateGraph());
        });
    }
}
