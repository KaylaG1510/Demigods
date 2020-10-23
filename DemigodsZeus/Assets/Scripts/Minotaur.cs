using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    //Charge attack
    public float ChargeSpeed = 5.0f;
    public float ChargeDuration = 1.0f;
    private bool ChargeStarted = false;
    private float ChargeTimer = 0;

    //movement
    public float speed;
    private bool movingRight = false;
    public Transform groundDetection;

    //Animator stuff??
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    //ground sensor?? see bandit script??

    //attack stuff
    private bool m_isDead = false;
    public HealthBar healthBar;
    public float attackRange;
    public double damage;
    private AttackType attack;
    private float lastAttackTime;
    public float attackDelay;
    public GameObject playerTarget;

    //health
    public int maxHealth = 500;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerTarget = GameObject.FindGameObjectWithTag("Player");
        attackDelay = 2f;
        damage = 30;
        attackRange = 150;

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        //***m_groundSensor???
    }

    void Update()
    {
        //****Set minotaur to always move

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        //minotaur about to walk off platform edge?
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 100f);

        if (!groundInfo.collider)
        {
            changeDirection();
        }
    }

    //multiplies damage done depending on attack type
    private double attackMultiplier()
    {
        switch(attack)
        {
            case AttackType.Swing:
                damage *= 1;
                break;
            case AttackType.Bash:
            case AttackType.Stomp:
                damage *= 1.2;
                break;
            case AttackType.Charge:
                damage *= 2;
                break;
            default:
                break;
        }
        return damage;
    }

    public enum AttackType
    {
        Swing,
        Bash,
        Stomp,
        Charge
    }

    private void changeDirection()
    {
        if(!movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = true;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
    }

    public void takeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        //**set animator trigger to hurt
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Minotaur down!");
        //***play death animation
        Destroy(gameObject, 1.5f);
    }
}
