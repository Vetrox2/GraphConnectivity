using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuCollider : MonoBehaviour
{
    [SerializeField]
    private RectTransform MenuRect;
    private new BoxCollider2D collider;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CameraControler.CameraControlerInstance.OnCameraSizeChange += (_) => UpdateCollider();
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        collider.size = new Vector2(MenuRect.rect.width, MenuRect.rect.height);
        collider.offset = transform.InverseTransformPoint(MenuRect.position);
    }
}
