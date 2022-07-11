using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomControl : MonoBehaviour
{
    public Camera camera;
    GridManager gridManager;
    public Slider slider;

    float maxZoom;
    int minZoom = 1;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        ZoomUpdate();
    }

    public void SetMinMaxZoomValues()
    {
        slider.minValue = minZoom;
        maxZoom = gridManager.height / 2f;
        slider.maxValue = maxZoom;
        slider.value = maxZoom;
    }

    void ZoomUpdate()
    {
        camera.orthographicSize = slider.value;
    }
}
