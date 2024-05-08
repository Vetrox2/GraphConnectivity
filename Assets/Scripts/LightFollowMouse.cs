using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFollowMouse : MonoBehaviour
{
    public static LightFollowMouse Instance;
    public List<GameObject> Blocks = new();
    private Light2D Light;

    private void Start()
    {
        Instance = this;
        Light = GetComponent<Light2D>();
    }

    void Update()
    {
        var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;
        if (Blocks.Count > 0) Light.enabled = false;
        else Light.enabled = true;
    }
}
