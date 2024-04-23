using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private float tileSize;
    [SerializeField]
    private Transform parentPosition;
    private int[,] gridArray;

    [SerializeField] private Vector3 Offset;

    private List<TileData> tileDataList = new List<TileData>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        gridArray = new int[width, height];

        for (int i = 0; i <= gridArray.GetLength(0) / tileSize; i++)
        {
            for (int j = 0; j <= gridArray.GetLength(1) / tileSize; j++)
            {
                GameObject gridSquare = new GameObject();
                gridSquare.transform.parent = parentPosition;
                gridSquare.transform.position = (new Vector3(i * tileSize, j * tileSize, 0) + Offset);
                gridSquare.name = $"GridSquare(X: {gridSquare.transform.position.x} | Y: {gridSquare.transform.position.y} | I: {i} | J: {j})";

                SpriteRenderer spriteRenderer = gridSquare.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("GridSquare");
                gridSquare.AddComponent<TileData>().Init((int)gridSquare.transform.position.x, (int)gridSquare.transform.position.y, false);
                tileDataList.Add(gridSquare.GetComponent<TileData>());

                Debug.DrawLine(gridSquare.transform.position, gridSquare.transform.position + new Vector3(tileSize, 0, 0), Color.white, 100f);
                Debug.DrawLine(gridSquare.transform.position, gridSquare.transform.position + new Vector3(0, tileSize, 0), Color.white, 100f);
                Debug.DrawLine(gridSquare.transform.position + new Vector3(tileSize, 0, 0), gridSquare.transform.position + new Vector3(tileSize, tileSize, 0), Color.white, 100f);
                Debug.DrawLine(gridSquare.transform.position + new Vector3(0, tileSize, 0), gridSquare.transform.position + new Vector3(tileSize, tileSize, 0), Color.white, 100f);
            }
        }
    }
    public void HandleClick(Vector3 gridPosition, GameObject buildingPrefab)
    {
        TileData tmp = tileDataList.Find(tile => tile.x == gridPosition.x && tile.y == gridPosition.y);

        if (tmp == null)
            Debug.Log("Tile is null!");
        else if(tmp.isOccupied)
            Debug.Log("Tile is already occupied!");
        else
        {
            Instantiate(buildingPrefab, gridPosition, Quaternion.identity);
            Debug.Log($"Spawned at(X: {gridPosition.x} | Y: {gridPosition.y})");
            tmp.SetIsOccupied(true);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BuildingManager.Instance.IsBuildingMode)
        {
            float _x = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            float _y = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            Vector3 clickPosition = new Vector2(_x, _y);
            Vector3 gridPosition = GetGridPositionFromWorldPosition(clickPosition);

            this.HandleClick(gridPosition, BuildingManager.Instance.Building);
        }
    }

    private Vector3 GetGridPositionFromWorldPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((parentPosition.position.x - worldPosition.x) / tileSize);
        int y = Mathf.FloorToInt((parentPosition.position.y - worldPosition.y) / tileSize);
        return new Vector3(-x * tileSize, -y * tileSize, 0);
    }
}
