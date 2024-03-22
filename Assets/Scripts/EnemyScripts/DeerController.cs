using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : EnemyController
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    player.ToggleRide();
                    Destroy(gameObject);
                }
            }
        }
    }
}
