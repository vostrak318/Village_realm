using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrorBirdController : EnemyController
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (IdleAnimator != null && AttackAnimator != null && player != null && currentCooldown <= 0f)
            {
                AttackAnimator.SetBool("Attacking", true);
                Attack();
            }
            else
            {
                AttackAnimator.SetBool("Attacking", false);
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
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < 7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed * Time.deltaTime);
            IdleAnimator.SetBool("Walk", true);
            if (gameObject.transform.position.x > GameManager.instance.player.transform.position.x)
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            IdleAnimator.SetBool("Walk", false);
        }
    }
}