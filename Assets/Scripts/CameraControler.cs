using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    /// <summary>Camera size in pixels</summary>
    public Vector2 CameraSize { get; private set; }
    public event System.Action<Vector2> OnCameraSizeChange;
    public static CameraControler CameraControlerInstance;

    private static Camera GameCamera;

    private void Awake()
    {
        GameCamera = GetComponent<Camera>();
        CameraSize = new Vector2(GameCamera.scaledPixelWidth, GameCamera.scaledPixelHeight);
        if (CameraControlerInstance != null) Debug.LogError("CameraControler can be instanced only once!");
        CameraControlerInstance = this;
    }

    void Update()
    {
        if (CameraSize.x != GameCamera.scaledPixelWidth || CameraSize.y != GameCamera.scaledPixelHeight)
        {
            CameraSize = new Vector2(GameCamera.scaledPixelWidth, GameCamera.scaledPixelHeight);
            OnCameraSizeChange?.Invoke(CameraSize);
        }
    }

    /// <returns>Rect measured from bottom-left</returns>
    public Rect GetWorldspaceCameraRect()
    {
        Vector2 bottomLeftPos = GameCamera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 topRightPos = GameCamera.ViewportToWorldPoint(new Vector3(1, 1));
        return new Rect(bottomLeftPos, topRightPos - bottomLeftPos);
    }

    public Vector2 GetRandomPositionInCameraView()
    {
        var rect = GetWorldspaceCameraRect();
        float randomX = Random.Range(rect.x, rect.x + rect.width);
        float randomY = Random.Range(rect.y, rect.y + rect.height);
        return new Vector2(randomX, randomY);
    }
}
