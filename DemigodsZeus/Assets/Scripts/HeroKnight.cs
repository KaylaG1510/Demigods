﻿using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    //Testing
    public int maxHealth = 150;
    public int currentHealth;
    public int Damage = 30;
    public HealthBar healthBar;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public GameObject           pauseMenu;
    public GameObject           pauseButton;
    public GameObject           levelCanvas;    //contains pause menu and win/lose panels

    AudioSource                 movementSrc;
    public bool                 isMoving = false;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;

    // Use this for initialization
    void Start ()
    {
        //Test
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        movementSrc = GetComponent<AudioSource>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update ()
    {
        //user hits esc key to bring up pause menu
        if (Input.GetKeyDown("escape"))
        {
            //pause/freeze any animated things
            Time.timeScale = 0f;
            //activate pause menu
            pauseMenu.SetActive(true);
            //hide pause button as already paused
            pauseButton.SetActive(false);
        }

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            ManagingAudio.PlaySound("Landing");
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0) //moving right
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0)    //moving left
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling)
        {
            //translate rigidbody
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //rigidbody is moving
            if (m_body2d.velocity.x != 0)
            {
                isMoving = true;
            }
            //not moving
            else
            {
                isMoving = false;
            }

            //only play moving sound when on ground, moving and game isnt paused
            if (isMoving && m_grounded && Time.timeScale == 1)
            {
                //check sound already playing
                if (!movementSrc.isPlaying)
                {
                    movementSrc.PlayScheduled(2.0f);
                }
            }
            else //stop sound
            {
                movementSrc.Stop();
            }
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        //Attack
        //*****was 1f to slow attacks
        if (Input.GetKeyDown("w") && m_timeSinceAttack > 0.25f)
        {
            ManagingAudio.PlaySound("Melee");
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;

            //attack enemy
            Attack();

        }
        // Block
        else if (Input.GetKeyDown("e"))
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            ManagingAudio.PlaySound("ESkill");
        }
        //stop blocking
        else if (Input.GetKeyUp("e"))
        {
            m_animator.SetBool("IdleBlock", false);
        }
        // Roll *Sprint 2 potentially
        //else if (Input.GetKeyDown("left shift") && !m_rolling)
        //{
        //    m_rolling = true;
        //    m_animator.SetTrigger("Roll");
        //    m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        //}

        //Jump
        else if ((Input.GetKeyDown("space") && m_grounded) || (Input.GetKeyDown("up") && m_grounded))
        {
            ManagingAudio.PlaySound("Jump");
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        //Idle
        else
        {
            // Prevents flickering transitions to idle
            if (currentHealth > 0)
            {
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
            //character dies
            Death();

        }
        Death();
        // Animation Events
        // Called in end of roll animation.
        //void AE_ResetRoll()
        //{
        //    m_rolling = false;
        //}

        // Called in slide animation.
        void AE_SlideDust()
        {
            Vector3 spawnPosition;

            if (m_facingDirection == 1)
                spawnPosition = m_wallSensorR2.transform.position;
            else
                spawnPosition = m_wallSensorL2.transform.position;

            if (m_slideDust != null)
            {
                // Set correct arrow spawn position
                GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
                // Turn arrow in correct direction
                dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
            }
        }
    }

    //negate damage from health, check if player is dead
    public void TakeDamage(int damage)
    {
        Debug.Log("Damage received :o");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            m_animator.SetTrigger("Hurt");
        }
        //check if player is dead
        
    }

    //Character loses all health
    public void Death()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            m_animator.SetTrigger("Death");
            //tell LevelCanvas to pull up GameOver screen
            levelCanvas.SendMessage("InvokeMenuAfterDeath");

            //deactivate enemies so health bars dont take up menu space
            //??
        }
    }

    //attack enemies
    void Attack()
    {
        //GetComponent<Animation>()["attack 1"].speed = 1;
        m_animator.SetTrigger("Attack1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        //check each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BanditAI>().takeDamage(Damage);
        }
    }

    //for circle collider on sword
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}