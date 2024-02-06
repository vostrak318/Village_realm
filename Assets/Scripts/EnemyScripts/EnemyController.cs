using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(float playerDmg)
    {
        enemy.hp -= playerDmg;
    }
}
