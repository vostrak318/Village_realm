using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public int x;
    public int y;
    public bool isOccupied;

    public TileData(int x, int y, bool isOccupied)
    {
        this.x = x;
        this.y = y;
        this.isOccupied = isOccupied;
    }

    public void SetIsOccupied(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }
}
