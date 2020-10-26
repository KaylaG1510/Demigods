using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minotaur : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    //public bool isFlipped;
    public bool Attacking;
    //public Transform groundDetection;
    //private Sensor_Bandit m_groundSensor;
    public HealthBar healthBar;
    public double maxHealth;
    double currentHealth;
    public bool IsAlive;
    public bool Stunned;
    public bool Dying;

    public Rigidbody2D m_body2d;
    public Animator m_animator;
    private Transform playerTransform;
    public float chaseDistance;

    public float attackRange;
    public double damage;
    private float lastAttackTime;
    public float attackDelay;
    public GameObject playerTarget;
    public float AttackVelocityX;

    //charge stuff
    public float ChargeSpeed;
    public float ChargeDuration;
    private bool ChargeStarted;
    private float ChargeTimer;

    string m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //initialise all variables from raycast player, controller and
        //bandit ai code
        //speed = -100;
        movingRight = true;
        Attacking = false;
        //maxHealth = 500;
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
        playerTransform = playerTarget.GetComponent<Transform>();
        chaseDistance = 1500;

        //level 2 slower speed and less health
        //level 3 faster, less attack delay and more damage(double health)
        m_scene = SceneManager.GetActiveScene().name;
        if (m_scene.CompareTo("LevelTwo") == 0)
        {
            maxHealth = 500;
        }
        else if (m_scene.CompareTo("LevelThree") == 0)
        {
            maxHealth = 900;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetBool("Alive", IsAlive);
        //m_animator.SetBool("Walking", true);

        if (Input.GetKeyDown("1"))
        {
            //test attacks
        }

        //everytime player is in range and attack delay is up,
        //call:
        //attackMultiplier(chooseAttack());

        float distToPlayer = Vector3.Distance(transform.position, playerTarget.transform.position);

        //when player is within distance, or bottom cave level,
        //then set trigger to walk, otherwise just idle
        if (distToPlayer <= chaseDistance)
        {
            //Debug.Log("Start chasing");
            m_animator.SetTrigger("Chase");
        }
        else if (distToPlayer > chaseDistance)
        {
            //m_animator.SetBool("Walking", false);
            //m_animator.SetBool("Idle", true);
            //reset trigger??
            m_animator.SetTrigger("StopChase");
        }

    }

    private AttackType chooseAttack()
    {
        AttackType attack;
        int rand_num = Random.Range(0, 4);

        switch (rand_num)
        {
            case 0:
                attack = AttackType.Stomp;
                Debug.Log("num: " + rand_num + "Attack Type: Stomp");
                break;
            case 1:
                attack = AttackType.Bash;
                Debug.Log("num: " + rand_num + "Attack Type: Bash");
                break;
            case 2:
                attack = AttackType.Charge;
                Debug.Log("num: " + rand_num + "Attack Type: Charge");
                break;
            case 3:
            default:
                attack = AttackType.Swing;
                Debug.Log("num: " + rand_num + "Attack Type: Swing/Default");
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

        switch (attackType)
        {
            case AttackType.Bash:
                damage *= 1.5;
                break;
            case AttackType.Stomp:
            case AttackType.Charge:
                damage *= 1;
                break;
            case AttackType.Swing:
                damage *= 1.25;
                break;
        }

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
        if (Stunned)
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
        if (ChargeStarted)
        {
            if (ChargeTimer < ChargeDuration)
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

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    //public void ChangeDirection()
    //{
    //    if(!movingRight)
    //    {
    //        transform.eulerAngles = new Vector3(0, 0, 0);
    //        movingRight = true;
    //    }
    //    else
    //    {
    //        transform.eulerAngles = new Vector3(0, -180, 0);
    //        movingRight = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBound"))
        {
            Debug.Log("Hit Bounds");
            //ChangeDirection();
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("EndCredit");
    }
}
