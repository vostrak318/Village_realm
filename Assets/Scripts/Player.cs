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
    public float currentHP { get { return hp; } private set { hp = hp; } }
    [SerializeField]
    private float dmg = 10f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float age = 20f;
    public float currentAge { get { return age; } private set { age = age; } }
    [SerializeField]
    private float ageCooldown = 1f;
    private float currentCooldown;

    [SerializeField]
    private Image image;


    public float Speed { get { return speed; }}

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


    //loading player
    public void setHP()
    {
        hp = PlayerPrefs.GetFloat("HP");
    }
    public void setAge()
    {
        age = PlayerPrefs.GetFloat("Age");
    }
    public void setPosition()
    {
        gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
    }

    public float ReturnHP()
    {
        return hp;
    }
}
