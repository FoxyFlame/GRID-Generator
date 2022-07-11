using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorItemGenerator : MonoBehaviour
{
    [SerializeField] Item itemPrefab;

    Player player;
    GridManager gridManager;

    List<Tile> placeableList = new List<Tile>();

    int yMAX = 1;
    int yMIN = -1;
    int xMAX = 1;
    int xMIN = -1;

    int currentX = 0;
    int currentY = 1;

    int checkDirection = 0;

    int width;
    int height;

    public bool needReset = false;

    bool ShouldIncX = true;
    bool ShouldIncY = true;
    bool ShouldDecX = true;
    bool ShouldDecY = true;


    void Start()
    {
        player = FindObjectOfType<Player>();
        gridManager = FindObjectOfType<GridManager>();
    }

    public void SetValues(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void SpawnPressed()
    {
            if (needReset)
            {
                GetPlaceableList();
                ResetSearch();
                needReset = false;
            }
            else
            {
                CreateItem();
            }
    }

    public void GetPlaceableList()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();

        placeableList.Clear();

        foreach (Tile tile in tiles)
        {
            if (tile.isPlaceable)
            {
                placeableList.Add(tile);
            }
        }
    }

    public void ResetSearch()
    {
        yMAX = 1;
        yMIN = -1;
        xMAX = 1;
        xMIN = -1;

        currentX = 0;
        currentY = 1;

        checkDirection = 0;

        ShouldIncX = true;
        ShouldIncY = true;
        ShouldDecX = true;
        ShouldDecY = true;
    }

    void CreateItem()
    {
        Vector3 offset;

        offset = new Vector2(currentX, currentY);

        Vector3 PlayerPositionWithOffset = player.transform.position + offset;

        float halfWidth = (width / 2)+1;
        float halfHeight = (height / 2)+1;

        if(player.transform.position != PlayerPositionWithOffset)
        {
            switch (checkDirection)
            {
                case 0:
                    if (PlayerPositionWithOffset.x < halfWidth && PlayerPositionWithOffset.y < halfHeight)
                    {
                        if (currentX >= xMAX)
                        {
                            SpawnItem(offset);
                            currentY--;
                            checkDirection = 1;
                            ShouldIncX = true;

                        }
                        else
                        {
                            SpawnItem(offset);
                            currentX++;
                        }
                    }
                    else
                    {
                        if(PlayerPositionWithOffset.x > halfWidth)
                        {
                            currentX--;
                            ShouldIncX = false;
                        }

                        currentX = xMAX;
                        checkDirection = 1;
                    }

                    break;

                case 1:
                    if (PlayerPositionWithOffset.y > -halfHeight && PlayerPositionWithOffset.x < halfWidth)
                    {

                        if (currentY <= yMIN)
                        {
                            SpawnItem(offset);
                            currentX--;
                            checkDirection = 2;
                            ShouldDecY = true;
                        }
                        else
                        {
                            SpawnItem(offset);
                            currentY--;
                        }
                    }
                    else
                    {
                        if (PlayerPositionWithOffset.y < -halfHeight)
                        {
                            currentY++;
                            ShouldDecY = false;
                        }
                        currentY = yMIN;
                        checkDirection = 2;
                    }
                    break;

                case 2:
                    if (PlayerPositionWithOffset.x > -halfWidth && PlayerPositionWithOffset.y > -halfHeight)
                    {
                        if (currentX <= xMIN)
                        {
                            SpawnItem(offset);
                            currentY++;
                            checkDirection = 3;
                            ShouldDecX = true;
                        }
                        else
                        {
                            SpawnItem(offset);
                            currentX--;
                        }
                    }
                    else
                    {
                        if(PlayerPositionWithOffset.x < -halfWidth)
                        {
                            currentX++;
                            ShouldDecX = false;
                        }
                        currentX = xMIN;
                        checkDirection = 3;
                    }
                    break;

                case 3:
                    if (PlayerPositionWithOffset.y < halfHeight && PlayerPositionWithOffset.x > -halfWidth)
                    {
                        if (currentY >= yMAX)
                        {
                            SpawnItem(offset);
                            currentX++;
                            checkDirection = 4;
                            ShouldIncY = true;
                        }
                        else
                        {
                            SpawnItem(offset);
                            currentY++;
                        }
                    }
                    else
                    {
                        if(PlayerPositionWithOffset.y > halfHeight)
                        {
                            currentY--;
                            ShouldIncY = false;
                        }
                        currentY = yMAX;
                        checkDirection = 4;
                    }
                    break;

                case 4:
                    if (PlayerPositionWithOffset.y < halfHeight)
                    {
                        SpawnItem(offset);
                        currentX++;

                        if (currentX >= 0)
                        {
                            IncresseSearchBound();
                        }
                    }
                    else
                    {
                        IncresseSearchBound();
                    }
                    break;
            }
        }
    }

    void IncresseSearchBound()
    {
        checkDirection = 0;

        if (ShouldIncY) yMAX++;
        if (ShouldDecY) yMIN--;
        if (ShouldIncX) xMAX++;
        if (ShouldDecX) xMIN--;
        currentX = 0;
        currentY = yMAX;
    }

    void SpawnItem(Vector3 offset)
    {
        if(itemPrefab == null) { return; }

        Vector3 positionFixed = new Vector3(player.transform.position.x, player.transform.position.y, -0.1f) + offset;

        foreach (var tile in placeableList)
        {
            if (tile.transform.position == (positionFixed + new Vector3(0f,0f,0.1f)) && tile.isPlaceable)
            {
                var spawnItem = Instantiate(itemPrefab, positionFixed, Quaternion.identity);

                tile.isPlaceable = false;
                placeableList.Remove(tile);
                return;
            }
        }
    }
}
