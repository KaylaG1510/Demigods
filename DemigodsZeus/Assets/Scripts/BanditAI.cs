using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAI : MonoBehaviour
{
    public float speed;
    private bool movingRight = false;
    public Transform groundDetection;

    private Animator m_animator;
    private Rigidbody2D m_body2d;   
    private Sensor_Bandit m_groundSensor;   //collider w ground
    private bool m_grounded = false;        //is enemy on ground
    private bool m_combatIdle = false;  
    private bool m_isDead = false;
    public HealthBar healthBar;         //enemy health bar

    //Test
    public int maxHealth = 120;
    int currentHealth;

    //Attack vars
    public float attackRange;   //how far away the enemy can reach the player when attacking
    public int damage;  //damage dealt to player in a single hit
    //enemy can only attack every couple of seconds
    private float lastAttackTime;
    public float attackDelay;
    //Player target to attack
    public GameObject playerTarget;

    private void Start()
    {
        currentHealth = maxHealth;
        //display health bar at full health
        healthBar.SetMaxHealth(maxHealth);

        //find the player enemies are targeting if not assigned in inspector
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        attackDelay = 2f;
        damage = 30;
        attackRange = 150;
        //set animator, rigidbody and ground sensor components
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    private void Update()
    {
        //constantly running
        m_animator.SetInteger("AnimState", 2);

        //check if character just landed on ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //move enemy
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        //get information on whether ground is detected at character edge of platform
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 100f);

        //no ground found/platform end or bounds reached
        if (groundInfo.collider == false)
        {
            changeDirection();
        }

        //Attack AI

        //check distance between self and player, is player close enough to trigger melee attack?
        float distToPlayer = Vector3.Distance(transform.position, playerTarget.transform.position);
        //RaycastHit2D close = Physics2D.CircleCast(transform.position, 100, Vector2.up);

        //player is within attack range
        if (distToPlayer < attackRange)
        {
            //Check enough time passed since last attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                //play attack animation
                m_animator.SetTrigger("Attack");

                //send message to player to take damage
                playerTarget.SendMessage("TakeDamage", damage, SendMessageOptions.RequireReceiver);

                //record time attacked
                lastAttackTime = Time.time;
            }
        }
    }

    //Enemy collides with patrol bounds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make sure collision is with bounds
        if (collision.gameObject.CompareTag("EnemyBound"))
        {
            changeDirection();
        }
    }

    //reached left or right bounds
    private void changeDirection()
    {
        //moving left, flip to right
        if (!movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
        //moving right, flip to left
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
    }

    //receives message from hero to take damage
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        //display hurt animation
        m_animator.SetTrigger("Hurt");

        healthBar.SetHealth(currentHealth);

        //is enemy dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //kills enemy
    void Die()
    {
        Debug.Log("Test: Enemy Deded");
        //play death animation
        m_animator.SetTrigger("Death");
        //remove enemy gameobject after death animation plays
        Destroy(gameObject, 0.8f);
    }
}
