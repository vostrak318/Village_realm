using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    private GameObject attackArea = default;

    private bool attacking = false;

    private float timeToAttack = 0.5f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                animator.SetBool("Attack", false);
                attackArea.SetActive(attacking);
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        animator.SetBool("Attack", true);
    }
}
