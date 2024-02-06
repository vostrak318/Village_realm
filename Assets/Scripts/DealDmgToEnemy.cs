using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgToEnemy : MonoBehaviour
{
    [SerializeField] Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<EnemyController>().TakeDmg(player.DealDamage());
        }
    }
}
