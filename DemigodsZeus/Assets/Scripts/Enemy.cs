using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Animation Variable
    public Animator animator;

    // Collider Variable 
    public BoxCollider2D collider2D;

    // Declaring Health for enemies
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        // Set
        currentHealth = maxHealth;

        // Instanstiate 
        collider2D = transform.GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int damage)
    {
        // If damage take away from current health 
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        // Disable Enemy when dead 
        collider2D.enabled = false;
        this.enabled = false;
    }
}
