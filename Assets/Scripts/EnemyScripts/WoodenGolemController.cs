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
                IdleAnimator.SetBool("Attacking", true);
                Attack();
            }
            else
            {
                IdleAnimator.SetBool("Attacking", false);
            }
        }
    }
    private void Update()
    {
        GoAfterPlayer();
        LowerCooldown();
    }

    public void GoAfterPlayer()
    {
        if (Vector2.Distance(parentHolder.transform.position, GameManager.instance.player.transform.position) < 7f)
        {
            parentHolder.transform.position = Vector2.MoveTowards(parentHolder.transform.position, GameManager.instance.player.transform.position, speed * Time.deltaTime);

            IdleAnimator.SetBool("Walk", true);
        }
        else
        {
            IdleAnimator.SetBool("Walk", false);
        }
    }
}
