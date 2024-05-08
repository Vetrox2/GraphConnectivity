using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraphVisualObject : MonoBehaviour
{
    [SerializeField]
    private GameObject ConnectionPrefab;
    [SerializeField]
    private GameObject ParticlesPrefab;
    private List<GraphVisualConnection> Connections = new();
    private bool FollowMouse = false;
    private new Collider2D collider;
    private new Rigidbody2D rigidbody;

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

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckForMouseClick();
        FollowMouseMovement();
        UpdateConnectionsVisuals();
    }

    private void UpdateConnectionsVisuals()
    {
        foreach (var conneciton in Connections)
        {
            conneciton.lineRenderer.SetPosition(0, transform.position);
            conneciton.lineRenderer.SetPosition(1, conneciton.gameObject.transform.position);
        }
    }

    private void FollowMouseMovement()
    {
        if (FollowMouse)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 lastPosition = transform.position;
            rigidbody.MovePosition(mousePos);
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                FollowMouse = false;
                var newVelocity = (mousePos - lastPosition) / Time.deltaTime;
                var speed = newVelocity.magnitude;
                newVelocity = newVelocity.normalized * Mathf.Sqrt(speed);
                rigidbody.velocity = newVelocity;
                if (Input.GetMouseButtonUp(1)) rigidbody.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    private void CheckForMouseClick()
    {
        if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (!LightFollowMouse.Instance.Blocks.Contains(gameObject)) LightFollowMouse.Instance.Blocks.Add(gameObject);
            if (Input.GetMouseButtonDown(0))
            {
                FollowMouse = true;
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            if (Input.GetMouseButtonDown(1))
            {
                FollowMouse = true;
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        else if (!FollowMouse) LightFollowMouse.Instance.Blocks.Remove(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var particles = Instantiate(ParticlesPrefab, collision.contacts[0].point, transform.rotation);
        particles.transform.parent = transform;
        Destroy(particles.gameObject, 2);
    }
}
