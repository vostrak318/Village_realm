using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 100;
    [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private float dmg = 10f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float age = 20f;
    [SerializeField]
    private float ageCooldown = 1f;
    private float currentCooldown;

    [SerializeField]
    private Image image;


    public float Speed { get { return speed; }}

    void Start()
    {
        
    }

    void Update()
    {
        AddAge();
    }

    void AddAge()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
        else if (currentCooldown <= 0f)
        {
            age++;
            currentCooldown = ageCooldown;
        }
    }

    public void DecreaseHP(float enemyDmg)
    {
        hp -= enemyDmg;
        image.fillAmount = hp / maxHp;
    }

    public float DealDamage()
    {
        return dmg;
    }
}
