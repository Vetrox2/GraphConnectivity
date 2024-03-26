using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphVisualObject : MonoBehaviour
{
    [SerializeField]
    private GameObject ConnectionPrefab;
    private List<GraphVisualConnection> Connections = new();

    private int _Index;
    public int Index
    {
        get => _Index; 
        set 
        {
            GetComponentInChildren<TextMeshPro>().text = value.ToString();
            name = $"GraphVisualObject {value}";
            _Index = value;
        }
    }

    void Update()
    {
        foreach (var conneciton in Connections)
        {
            conneciton.lineRenderer.SetPosition(0, transform.position);
            conneciton.lineRenderer.SetPosition(1, conneciton.gameObject.transform.position);
        }
    }

    public void AddConnection(GameObject connection)
    {
        var connectionObj = Instantiate(ConnectionPrefab, transform);
        int connectedIndex = connection.GetComponent<GraphVisualObject>().Index;
        connectionObj.name = $"Connection {Index}-{connectedIndex}";

        var joint = gameObject.AddComponent<SpringJoint2D>();
        joint.autoConfigureDistance = false;
        joint.distance = 2;
        joint.enableCollision = true;
        joint.frequency = 0.6f;
        joint.connectedBody = connection.GetComponent<Rigidbody2D>();

        Connections.Add(new GraphVisualConnection() { gameObject = connection, lineRenderer = connectionObj.GetComponent<LineRenderer>(), joint = joint });
    }
}
