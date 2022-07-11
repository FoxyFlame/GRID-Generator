using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragControler : MonoBehaviour
{
    SnapToGrid snapToGrid;
    ColorItemGenerator colorItemGenerator;

    bool isDragged = false;

    Vector3 mouseDragStartPosition;
    Vector3 playerDragStartPosition;

    void Start()
    {
        snapToGrid = GetComponent<SnapToGrid>();
        colorItemGenerator = FindObjectOfType<ColorItemGenerator>();
    }

    private void OnMouseDown()
    {
        isDragged = true;
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerDragStartPosition = transform.localPosition;
        snapToGrid.GetSnapPoints();
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = playerDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
        }
    }

    private void OnMouseUp()
    {
        ReleaseMouseButton();
    }

    public void ReleaseMouseButton()
    {
        isDragged = false;
        snapToGrid.OnEndOfDrag(playerDragStartPosition);
        colorItemGenerator.needReset = true;
    }
}
