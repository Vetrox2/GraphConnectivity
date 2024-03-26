using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollidersControler : MonoBehaviour
{
    [SerializeField]
    GameObject LeftBorder;
    [SerializeField]
    GameObject RightBorder;
    [SerializeField]
    GameObject TopBorder;
    [SerializeField]
    GameObject BottomBorder;

    void Start()
    {
        CameraControler.CameraControlerInstance.OnCameraSizeChange += (Vector2 _) => SetBorders(CameraControler.CameraControlerInstance.GetWorldspaceCameraRect());
        SetBorders(CameraControler.CameraControlerInstance.GetWorldspaceCameraRect());
    }

    private void SetBorders(Rect ViewSpace)
    {
        LeftBorder.transform.position = new Vector3(ViewSpace.x, 0, 0);
        RightBorder.transform.position = new Vector3(ViewSpace.x + ViewSpace.width, 0, 0);
        TopBorder.transform.position = new Vector3(0, ViewSpace.y + ViewSpace.height, 0);
        BottomBorder.transform.position = new Vector3(0, ViewSpace.y, 0);
    }
}
