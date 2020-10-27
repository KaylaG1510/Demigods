using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minotaur : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    public bool Attacking;
    public HealthBar healthBar;
    public double maxHealth;
    double currentHealth;
    public bool IsAlive;
    public bool Stunned;
    public bool Dying;
    public bool isHit;

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

    public GameObject AttackPtL;
    public GameObject AttackPtR;

    string m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //speed = -100; XX **set inside walk animation code**
        movingRight = true;
        Attacking = false;
        //maxHealth = 500;
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        attackDelay = 3f;
        damage = 30;
        attackRange = 150;
        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        IsAlive = true;
        Stunned = false;
        isHit = false;
        healthBar.SetMaxHealth((int)maxHealth);
        playerTransform = playerTarget.GetComponent<Transform>();
        chaseDistance = 1500;

        AttackPtL = GameObject.FindGameObjectWithTag("MinoAttackL").gameObject;
        AttackPtR = GameObject.FindGameObjectWithTag("MinoAttackR").gameObject;

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
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetBool("Alive", IsAlive);
        //m_animator.SetBool("Walking", false);
        //m_animator.SetBool("Idle", true);

        HandleChargeMovement();

        float distToPlayer = Vector3.Distance(transform.position, playerTarget.transform.position);
        //Debug.Log(distToPlayer);
        if (distToPlayer <= chaseDistance && !Attacking)
        {
            //Debug.Log("Start chasing");
            m_animator.SetTrigger("Chase");
            //m_animator.SetBool("Walking", true);
            //m_animator.SetBool("Idle", false);
        }
        if (distToPlayer > chaseDistance)
        {
            //m_animator.SetBool("Walking", false);
            //m_animator.SetBool("Idle", false);
            //m_animator.SetBool("Idle", true);
            m_animator.SetTrigger("StopChase");
        }

        if (isHit)
        {
            Debug.Log(currentHealth);
            currentHealth -= damage;
            Debug.Log(currentHealth);
            
            ToggleStun();
            healthBar.SetHealth((int)currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        if (movingRight)
        {
            AttackPtL.SetActive(false);
            AttackPtR.SetActive(true);
        }
        else
        {
            AttackPtL.SetActive(true);
            AttackPtR.SetActive(false);
        }

        if (distToPlayer < attackRange)
        {
            if (Time.time > lastAttackTime + attackDelay)
            {
                //show attack anims perform...
                //send damage message from somewhere

                int damageDealt = (int)attackMultiplier(chooseAttack());
                //attackDelegate = (int)attackMultiplier(chooseAttack);

                playerTarget.SendMessage("TakeDamage", damageDealt);

                ResetDamage();
                lastAttackTime = Time.time;
            }
        }
    }

    //public delegate AttackDelegate(AttackType a);
    //AttackDelegate attackDelegate;

    private AttackType chooseAttack()
    {
        AttackType attack;
        int rand_num = Random.Range(0, 4);
        m_animator.SetTrigger("StopChase");

        switch (rand_num)
        {
            case 0:
                attack = AttackType.Stomp;
                Debug.Log("num: " + rand_num + "Attack Type: Stomp");
                PerformStomp();
                break;
            case 1:
                attack = AttackType.Bash;
                Debug.Log("num: " + rand_num + "Attack Type: Bash");
                PerformBash();
                break;
            case 2:
                attack = AttackType.Charge;
                Debug.Log("num: " + rand_num + "Attack Type: Charge");
                PerformCharge();
                break;
            case 3:
            default:
                attack = AttackType.Swing;
                Debug.Log("num: " + rand_num + "Attack Type: Swing/Default");
                PerformSwing();
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
        return damage;
    }

    public void Die()
    {
        IsAlive = false;
        Debug.Log("Minotaur down");
        m_animator.SetBool("Alive", IsAlive);
        m_animator.SetBool("Dying", Dying);
        //level 2 over, trigger level 3
        //level 3 over, trigger credits
    }

    public void Stun()
    {
        Stunned = true;
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
        isHit = false;
        m_animator.SetBool("Stunned", Stunned);
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
        Debug.Log("Perform Charge called");
    }

    private void PerformStomp()
    {
        //mino controller
        Attacking = true;
        m_animator.SetBool("StompAttackStart", true);
        Debug.Log("perform stomp called");
    }

    private void PerformBash()
    {
        //mino controller
        Attacking = true;
        m_animator.SetBool("BashAttackStart", true);
        Debug.Log("perform bash called");
    }

    private void PerformSwing()
    {
        //mino controller
        Attacking = true;
        m_animator.SetBool("SwingAttackStart", true);
        Debug.Log("perform swing called");
    }

    //FixedTick and update animator base methods??

    public void StopAttacks()
    {
        Attacking = false;
        m_animator.SetBool("SwingAttackStart", false);
        m_animator.SetBool("BashAttackStart", false);
        m_animator.SetBool("ChargeAttackStart", false);
        m_animator.SetBool("StompAttackStart", false);
        SetAttackVelocityX(0);
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
        isHit = true;
    }

    //public void ChangeDirection()
    //{
    //    if (!movingRight)
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

    //draw the three attack ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPtL.transform.position, attackRange);
        Gizmos.DrawWireSphere(AttackPtR.transform.position, attackRange);
    }

    public void MovingRight()
    {
        //Debug.Log("Moving Right!");
        movingRight = true;
    }

    public void MovingLeft()
    {
        movingRight = false;
    }

    private void ResetDamage()
    {
        damage = 30;
    }
}
