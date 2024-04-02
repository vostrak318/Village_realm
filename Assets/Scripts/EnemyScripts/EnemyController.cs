using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    protected bool attacked = false;
    [SerializeField]
    protected Animator IdleAnimator;
    [SerializeField]
    protected Animator AttackAnimator;
    protected float currentCooldown;
    protected float maxCooldown = 3f;

    [SerializeField]
    protected float hp;
    protected float maxHp;
    protected float dmg;
    protected float speed;
    protected GameObject drop;

    [SerializeField]
    protected Image healthBar;
    [SerializeField]
    protected GameObject healthCanvas;

    [SerializeField]
    protected GameObject parentHolder;


    private void Start()
    {
        healthCanvas.SetActive(false);
        hp = enemy.hp;
        maxHp = hp;
        dmg = enemy.dmg;
        speed = enemy.speed;
        currentCooldown = maxCooldown;
        drop = enemy.drop;
    }

    private void Update()
    {
        LowerCooldown();
    }

    public virtual void Attack()
    {
        GameManager.instance.player.DecreaseHP(dmg);
        currentCooldown = maxCooldown;
    }

    public void TakeDmg(float playerDmg)
    {
        healthCanvas.SetActive(true);
        this.hp -= playerDmg;
        healthBar.fillAmount = hp / maxHp;
        if (this.hp <= 0 )
        {
            Destroy(parentHolder);
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);
            Instantiate(drop, spawnPosition, Quaternion.identity);
        }
    }

    public void LowerCooldown()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
