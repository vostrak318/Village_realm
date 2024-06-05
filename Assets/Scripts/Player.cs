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
    public float speed = 1f;
    [SerializeField]
    private float age = 20f;
    public float currentAge { get { return age; } private set { age = age; } }

    [SerializeField]
    private float maxHunger = 100f;
    [SerializeField]
    private float hunger;
    public float currentHunger { get { return hunger; } private set { hunger = hunger; } }
    [SerializeField]
    private float ageCooldown = 720f; // 4 minuty na den, 3 dny na rok, cca 50 let
    private float currentAgeCooldown;
    [SerializeField]
    private float hungerCooldown = 60f; // 1 minuta na 1% hladu
    private float currentHungerCooldown;

    [SerializeField]
    private float hungerDMGCooldown = 30f; // pul minuta na dmg z hladu
    private float currentHungerDMGCooldown;

    [SerializeField]
    private float HealFromFullHungerCooldown = 30f; // pul minuta na heal z plnyho briska
    private float currentHealFromFullHungerCooldown;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private Image hungerBar;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private GameObject victoryScreen;

    [SerializeField]
    private GameObject statUI;


    void Update()
    {
        AddAge();
        CheckAge();
        DecreaseHungerCooldown();
        CheckHunger();
        IncreaseHealFromFullHunger();
        LoadStats();
    }

    void AddAge()
    {
        currentAgeCooldown += Time.deltaTime;
        if (currentAgeCooldown >= ageCooldown)
        {
            age++;
            currentAgeCooldown = 0;
        }
    }
    void DecreaseHungerCooldown()
    {
        currentHungerCooldown += Time.deltaTime;
        if (currentHungerCooldown >= hungerCooldown)
        {
            hunger--;
            currentHungerCooldown = 0;
        }
    }
    void DecreaseHungerDMGCooldown()
    {
        currentHungerDMGCooldown += Time.deltaTime;
        if (currentHungerDMGCooldown >= hungerDMGCooldown)
        {
            currentHungerDMGCooldown = 0;
            DecreaseHP(20);
        }
    }
    void IncreaseHealFromFullHunger()
    {
        currentHealFromFullHungerCooldown += Time.deltaTime;
        if (currentHealFromFullHungerCooldown >= HealFromFullHungerCooldown && currentHunger >= hunger && hp < maxHp)
        {
            currentHealFromFullHungerCooldown = 0;
            if (hp > 90 && hp < maxHp)
            {
                hp = maxHp;
            }
            else
            {
                hp += 10;
            }
            hpBar.fillAmount = hp / maxHp;
        }
    }
    void CheckAge() 
    { 
        if (age >= rnd.Next(65, 80))
        { 
            hp = 0; 
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            statUI.SetActive(false);
        } 
    }
    void CheckHunger()
    {
        if (hunger <= 0)
        {
            DecreaseHungerDMGCooldown();
        }
        else
        {
            currentHungerDMGCooldown = 0;
        }
    }

    void LoadStats()
    {
        hpBar.fillAmount = hp / maxHp;
        hungerBar.fillAmount = hunger / maxHunger;
        
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
            statUI.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void DecreaseHunger()
    {
        hungerBar.fillAmount = hunger / maxHunger;
    }

    public float DealDamage()
    {
        return dmg;
    }

    //item effects
    public void AddHunger()
    {
        if (hunger < maxHunger)
        {
            hunger += 20;
            if (hunger > maxHunger)
            {
                hunger = maxHunger;
                hungerBar.fillAmount = hunger / maxHunger;
            }
            hungerBar.fillAmount = hunger / maxHunger;
        }
    }
    public void AddHP()
    {
        if (hp < maxHp)
        {
            hp += 20; 
            if (hp > maxHp)
            {
                hp = maxHp; 
                hpBar.fillAmount = hp / maxHp; 
            } 
            hpBar.fillAmount = hp / maxHp; 
        }
    }
    public void StopTheAge()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void AddSpeed()
    {
        speed += 0.5f;
    }
    public void AddDmg()
    {
        dmg += 5;
    }
    public void AddMaxHP()
    {
        maxHp += 10;
    }

    //loading player
    public void setHP()
    {
        hp = PlayerPrefs.GetFloat("HP");
        hpBar.fillAmount = hp / maxHp;
    }
    public void setHunger()
    {
        hunger = PlayerPrefs.GetFloat("Hunger");
        hungerBar.fillAmount = hunger / maxHunger;
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
