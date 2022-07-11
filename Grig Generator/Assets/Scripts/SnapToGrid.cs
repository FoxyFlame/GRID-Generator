using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] public float snapRange = 2f;

    Vector3 offset = new Vector3(0f, 0f, -0.1f);

    public List<Transform> snapPoints;

    Tile[] tiles;

    // to do - add start snap grid on the closest middle grid

    public void GetSnapPoints()
    {
        tiles = FindObjectsOfType<Tile>();

        if (snapPoints != null) { snapPoints.Clear(); }

        foreach (Tile tile in tiles)
        {
            if (tile.isPlaceable)
            {
                snapPoints.Add(tile.transform);
            }
        }
    }

    public void OnEndOfDrag(Vector3 startPosition)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(transform.localPosition, snapPoint.localPosition);

            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            transform.localPosition = closestSnapPoint.localPosition + offset;
        }
        else
        {
            transform.localPosition = startPosition;
        }
    }
}
