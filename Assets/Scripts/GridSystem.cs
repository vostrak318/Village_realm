using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    private int width;
    private int height;
    private float tileSize;
    private Transform parentPosition;
    private int[,] gridArray;

    private List<TileData> tileDataList = new List<TileData>();

    public GridSystem(int width, int height, float tileSize, Transform parentPosition)
    {
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;
        this.parentPosition = parentPosition;
        gridArray = new int[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                GameObject gridSquare = new GameObject("GridSquare");
                gridSquare.transform.parent = parentPosition;
                gridSquare.transform.position = parentPosition.position + new Vector3(i * tileSize, j * tileSize, 0);

                SpriteRenderer spriteRenderer = gridSquare.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("GridSquare");
                tileDataList.Add(new TileData(i, j, false));

                //Debug.DrawLine(parentPosition.position + new Vector3(i * tileSize, j * tileSize, 0), parentPosition.position + new Vector3((i + 1) * tileSize, j * tileSize, 0), Color.white, 100f);
                //Debug.DrawLine(parentPosition.position + new Vector3(i * tileSize, j * tileSize, 0), parentPosition.position + new Vector3(i * tileSize, (j + 1) * tileSize, 0), Color.white, 100f);
                //Debug.DrawLine(parentPosition.position + new Vector3((i + 1) * tileSize, j * tileSize, 0), parentPosition.position + new Vector3((i + 1) * tileSize, (j + 1) * tileSize, 0), Color.white, 100f);
                //Debug.DrawLine(parentPosition.position + new Vector3(i * tileSize, (j + 1) * tileSize, 0), parentPosition.position + new Vector3((i + 1) * tileSize, (j + 1) * tileSize, 0), Color.white, 100f);
            }
        }
    }
    public void HandleClick(Vector3 clickPosition, GameObject buildingPrefab)
    {
        TileData tmp;
        
        Vector3 gridPosition = GetGridPositionFromWorldPosition(clickPosition);
        tmp = tileDataList.Find(tile => tile.x == gridPosition.x && tile.y == gridPosition.y);
        if (gridPosition != null)
        {
            tmp = tileDataList.Find(tile => tile.x == gridPosition.x && tile.y == gridPosition.y);
            if (!tmp.isOccupied)
            {
                Instantiate(buildingPrefab, gridPosition, Quaternion.identity);
                tmp.SetIsOccupied(true);
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BuildingManager.Instance.IsBuildingMode)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 gridPosition = GetGridPositionFromWorldPosition(clickPosition);

            HandleClick(clickPosition, BuildingManager.Instance.Buidling);
        }
    }
    private Vector3 GetGridPositionFromWorldPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - parentPosition.position.x) / tileSize);
        int y = Mathf.FloorToInt((worldPosition.y - parentPosition.position.y) / tileSize);
        return parentPosition.position + new Vector3(x * tileSize, y * tileSize, 0);
    }

}
