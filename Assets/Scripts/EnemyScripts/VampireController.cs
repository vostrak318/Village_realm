using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VampireController : EnemyController
{
  

    void Start()
    {

    }


    void Update()
    {
        LowerCooldown();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (IdleAnimator != null && AttackAnimator != null && player != null && currentCooldown <= 0f)
            {
                AttackAnimator.SetBool("Attacking", true);
                player.DecreaseHP(enemy.dmg);
                currentCooldown = maxCooldown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AttackAnimator.SetBool("Attacking", false);
    }

    public void LowerCooldown()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
