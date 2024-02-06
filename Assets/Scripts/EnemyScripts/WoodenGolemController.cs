using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WoodenGolemController : EnemyController
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (IdleAnimator != null && player != null && currentCooldown <= 0f)
            {
                Attack();
            }
        }
    }
}
