using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    void Start()
    {
        for (int i = 0; i < 239; i++)
        {
            for (int j = 0; j < 109; j++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(i, j, 0), Quaternion.identity);
                newTile.transform.parent = this.transform;
            }
        }
    }
}
