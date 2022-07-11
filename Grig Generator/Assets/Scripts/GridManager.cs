using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public TextAsset jsonfile;
    [SerializeField] public int width;
    [SerializeField] public int height;

    [SerializeField] Tile tilePrefab;

    SnapToGrid snapToGrid;
    ColorItemGenerator colorItemGenerator;
    CameraZoomControl cameraZoomControl;

    public class GridSize
    {
        public int width;
        public int height;
    }

    private void Start()
    {
        GridSize gridSize = JsonUtility.FromJson<GridSize>(jsonfile.text);
        Debug.Log(gridSize.width);
        width = gridSize.width;
        height = gridSize.height;

        snapToGrid = FindObjectOfType<SnapToGrid>();
        colorItemGenerator = FindObjectOfType<ColorItemGenerator>();
        cameraZoomControl = FindObjectOfType<CameraZoomControl>();
        cameraZoomControl.SetMinMaxZoomValues();
        colorItemGenerator.SetValues(width, height);
        GridGenerator();
    }
    void GridGenerator()
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnTile(x,y);
            }
        }

        snapToGrid.GetSnapPoints();
        colorItemGenerator.GetPlaceableList();
        snapToGrid.OnEndOfDrag(new Vector3(0f, 0f, -0.1f));
    }

    void SpawnTile(float positionX, float positionY)
    {
        float correctPositionX = (width % 2 > 0) ? positionX - width / 2 : positionX - width / 2 + 0.5f;
        float correctPositionY = (height % 2 > 0) ? positionY - height / 2 : positionY - height / 2 + 0.5f;

        var spawnTile = Instantiate(tilePrefab, new Vector3(correctPositionX, correctPositionY), Quaternion.identity);
        spawnTile.name = $"Tile {correctPositionX} {correctPositionY}";

        var isOffset = (positionX % 2 == 0 && positionY % 2 != 0) || (positionX % 2 != 0 && positionY % 2 == 0);
        spawnTile.Init(isOffset);
    }
}
