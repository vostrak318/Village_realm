using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject drop;
    [SerializeField]
    private int prefabId;
    
    public int PrefabId { get => prefabId; set => prefabId = value; }

    private Vector2 spawnOffset = new Vector2(1,1);
    public void SpawnItem()
    {
        Vector2 spawnPosition1 = new Vector2(transform.position.x, transform.position.y);
        Instantiate(drop, spawnPosition1, Quaternion.identity);

        Vector2 spawnPosition2 = new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y);
        Instantiate(drop, spawnPosition2, Quaternion.identity);
    }
}
