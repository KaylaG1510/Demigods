using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class playertest : MonoBehaviour
{
    public int maxHealth = 150;
    public int currentHealth;
    public int Damage = 30;
    public HealthBar healthBar;
    private Animator m_animator;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;





    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(25);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.SendMessage("takeDamage", 30, SendMessageOptions.RequireReceiver);
    }

    void Attack()
    {
        //GetComponent<Animation>()["attack 1"].speed = 1;
        m_animator.SetTrigger("Attack1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        /*foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<boxHealth>().takeDamage(Damage);
        }*/
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        } 

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
