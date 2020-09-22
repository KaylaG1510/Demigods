using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    void Start()
    {

    }

    void Update()
    {
        // W key to fight enemies with sword
        if(Input.GetKeyDown(KeyCode.W))
        {
            FightWithW();
        }
    }

    void FightWithW()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<Enemy>.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        // Draw sphere to see in editor 
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
