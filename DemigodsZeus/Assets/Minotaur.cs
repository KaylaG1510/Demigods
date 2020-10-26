using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minotaur : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    public bool Attacking;
    //public Transform groundDetection;
    //private Sensor_Bandit m_groundSensor;
    public HealthBar healthBar;
    public double maxHealth;
    double currentHealth;
    public bool IsAlive;
    public bool Stunned;
    public bool Dying;

    private Rigidbody2D m_body2d;
    public Animator m_animator;

    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    public GameObject playerTarget;
    public float AttackVelocityX;

    //charge stuff
    public float ChargeSpeed;
    public float ChargeDuration;
    private bool ChargeStarted;
    private float ChargeTimer;

    // Start is called before the first frame update
    void Start()
    {
        //initialise all variables from raycast player, controller and
        //bandit ai code
        speed = -1;
        movingRight = true;
        Attacking = false;
        maxHealth = 500;
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        attackDelay = 2.5f;
        damage = 30;
        attackRange = 150;
        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        IsAlive = true;
        Stunned = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetBool("Alive", IsAlive);
        m_animator.SetBool("Walking", true);

        //move minotaur
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (Input.GetKeyDown("1"))
        {
            //test attacks
        }


    }

    private AttackType chooseAttack()
    {
        AttackType attack;
        int rand_num = Random.Range(0, 4);

        switch(rand_num)
        {
            case 0:
                attack = AttackType.Stomp;
                break;
            case 1:
                attack = AttackType.Bash;
                break;
            case 2:
                attack = AttackType.Charge;
                break;
            case 3:
            default:
                attack = AttackType.Swing;
                break;
        }

        return attack;
    }

    public enum AttackType
    {
        Swing,
        Bash,
        Stomp,
        Charge
    }


    public double attackMultiplier(AttackType attackType)
    {
        //changes damage multiplier depending on attack
        //also deals additional multiplier if on level 3



        //stub
        return 2.0f;
    }

    public void Die()
    {
        //raycast player
        IsAlive = false;
        Debug.Log("Minotaur down");
        m_animator.SetBool("Alive", IsAlive);
        m_animator.SetBool("Dying", Dying);
        //level 2 over
        //level 3 over
    }

    public void Stun()
    {
        //raycast player
        Stunned = true;
        m_animator.Play("Flinch");
        m_animator.SetBool("Stunned", Stunned);
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Walking", false);
    }

    public void ToggleStun()
    {
        if(Stunned)
        {
            FinishedStun();
        }
        else
        {
            Stun();
        }
    }

    public virtual void FinishedStun()
    {
        Stunned = false;
        m_animator.SetBool("Stunned", Stunned);
    }

    public void Dazed()
    {
        //raycast player
    }

    private void HandleChargeMovement()
    {
        //mino controller
        if(ChargeStarted)
        {
            if(ChargeTimer < ChargeDuration)
            {
                ChargeTimer += Time.deltaTime;
            }
            else
            {
                ChargeTimer = 0;
                ChargeStarted = false;
                StopAttacks();
            }
        }
    }

    private void PerformCharge()
    {
        //mino controller
        Attacking = true;
        ChargeStarted = true;
        SetAttackVelocityX(ChargeSpeed);
    }

    private void PerformStomp()
    {
        //mino controller
    }

    private void PerformBash()
    {
        //mino controller
    }

    private void PerformSwing()
    {

    }

    //FixedTick and update animator base methods??

    public void StopAttacks()
    {
        Attacking = false;
        m_animator.SetBool("SwingAttackStart", false);
        m_animator.SetBool("BashAttackStart", false);
        m_animator.SetBool("ChargeAttackStart", false);
        m_animator.SetBool("StompAttackStart", false);
        //SetAttackVelocityX(0);
    }

    public void SetAttackVelocityX(float vel)
    {
        if (movingRight)
        {
            AttackVelocityX = vel;
        }
        else
        {
            AttackVelocityX = -vel;
        }
    }

    public void TakeDamage(double damage)
    {
        currentHealth -= damage;
        //m_animator.SetTrigger
        //set up Dazed trigger and animation methods or use:
        m_animator.Play("Flinch");
        healthBar.SetHealth((int)currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    public void ChangeDirection()
    {
        if(!movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBound"))
        {
            Debug.Log("Hit Bounds");
            ChangeDirection();
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("EndCredit");
    }
}
