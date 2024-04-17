using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    System.Random rnd = new System.Random();
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
    private float ageCooldown = 720f; // 4 minuty na den, 3 dny na rok, cca 50 let
    private float currentCooldown;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private GameObject deathScreen;


    public float Speed { get { return speed; }}

    void Update()
    {
        AddAge();
        CheckAge();
    }

    void AddAge()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= ageCooldown)
        {
            age++;
            currentCooldown = 0;
        }
    }
    void CheckAge() 
    { 
        if (age >= rnd.Next(65, 80)) //nebo pokud nemas potion
        { 
            hp = 0; 
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            //game over
        } 
    }

    public void RestartAndQuitToMenu()
    {
        PlayerPrefs.DeleteAll();
        UIManager.Instance.QuitGame();
    }

    public void DecreaseHP(float enemyDmg)
    {
        hp -= enemyDmg;
        hpBar.fillAmount = hp / maxHp;
        if (hp <= 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public float DealDamage()
    {
        return dmg;
    }


    //loading player
    public void setHP()
    {
        hp = PlayerPrefs.GetFloat("HP");
        hpBar.fillAmount = hp / maxHp;
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
