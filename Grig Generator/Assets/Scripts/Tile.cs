using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color primaryColor;
    [SerializeField] Color offsetColor;
    [SerializeField] Color blockedTileColor;
    [SerializeField] SpriteRenderer renderer;

    [SerializeField] public bool isPlaceable;
    [SerializeField] public bool isBlocked;

    [SerializeField] int percentOfBlocked = 25;
    public void Init(bool isOffset)
    {
        if(Random.Range(0, 100) < percentOfBlocked)
        {
            renderer.color = blockedTileColor;
            isBlocked = true;
            isPlaceable = false;
        }
        else
        {
            renderer.color = isOffset ? offsetColor : primaryColor; // same as if(isOffset) { renderer.color = offsetColor } else { renderer.color = primaryColor }
            isBlocked = false;
            isPlaceable = true;
        }
    }
}
