using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingTree : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "tree")
        {
            Destroy(collision.gameObject);
            collision.GetComponent<ObjectDrop>().SpawnItem();
        }
    }
}
