using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "stone")
        {
            Destroy(collision.gameObject);
            collision.GetComponent<ObjectDrop>().SpawnItem();
        }
    }
}
