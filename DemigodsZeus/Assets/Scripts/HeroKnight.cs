using UnityEngine;
using System.Collections;
using System;

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
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //public Transform FirePoint;
    //public GameObject slashPrefab;
    //public Boolean test = false;

    public GameObject AttackPtL;
    public GameObject AttackPtR;
    public Transform AttackPointLeft;
    public Transform AttackPointRight;

    public GameObject           pauseMenu;
    public GameObject           pauseButton;
    public GameObject           levelCanvas;    //contains pause menu and win/lose panels

    AudioSource                 movementSrc;
    public bool                 isMoving = false;
    private bool                isBlocking = false;

    private ReaperDialogue      Reaper;
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
        AttackPtL = GameObject.FindGameObjectWithTag("AttackPtL").gameObject;
        AttackPtR = GameObject.FindGameObjectWithTag("AttackPtR").gameObject;
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

        //facing right
        if (m_facingDirection == 1)
        {
            AttackPtL.SetActive(false);
            AttackPtR.SetActive(true);
        }
        else //moving left
        {
            AttackPtL.SetActive(true);
            AttackPtR.SetActive(false);
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

        //Testing projectile
        /*if (Input.GetButtonDown("Fire1"))
        {
            test = true;
            m_animator.SetTrigger("Charge");
            m_animator.SetBool("hold", true);
            if (test)
            {
                Shoot();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            m_animator.SetBool("hold", false);
            //test = false;
        }*/

        // Block
        else if (Input.GetKeyDown("e"))
        {
            isBlocking = true;
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
        //stop blocking
        else if (Input.GetKeyUp("e"))
        {
            isBlocking = false;
            m_animator.SetBool("IdleBlock", false);
        }

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
            //Death();

        }

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
        Debug.Log("Damage received: " + damage);

        //check player is not blocking
        if (!isBlocking)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            if (currentHealth > 0)
            {
                m_animator.SetTrigger("Hurt");
            }
            //player has no health left; DEAD
            else
            {
                Debug.Log("Dead");
                m_animator.SetTrigger("Death");
                //tell levelCanvas to pull up GameOver screen
                levelCanvas.SendMessage("InvokeMenuAfterDeath");

                //deactiveate enemy health bars so dont take up menu space??
            }
        }
        else //player blocked attack
        {
            ManagingAudio.PlaySound("ESkill");
        }

    }

    //attack enemies
    void Attack()
    {
        Collider2D[] hitEnemies;
        //GetComponent<Animation>()["attack 1"].speed = 1;
        m_animator.SetTrigger("Attack1");

        //facing right
        if (m_facingDirection == 1)
        {
            //AttackPtL.SetActive(false);
            //AttackPtR.SetActive(true);
            hitEnemies = Physics2D.OverlapCircleAll(AttackPtR.transform.position, attackRange, enemyLayers);
        }
        else //moving left
        {
            hitEnemies = Physics2D.OverlapCircleAll(AttackPtL.transform.position, attackRange, enemyLayers);
        }

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointR.position, attackRange, enemyLayers);

        //check each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<BanditAI>().takeDamage(Damage);
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<BanditAI>().takeDamage(Damage);
            }
            else if (enemy.CompareTag("Boss"))
            {
                enemy.GetComponent<Minotaur>().TakeDamage(Damage);
            }
        }
    }

    //for circle collider on sword
    void OnDrawGizmosSelected()
    {
        if (AttackPtL.transform == null || AttackPtR.transform == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPtL.transform.position, attackRange);
        Gizmos.DrawWireSphere(AttackPtR.transform.position, attackRange);
    }

    //collides with GameObject set as a Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //falls off map
        if (collision.CompareTag("OffMap"))
        {
            TakeDamage(150);
            //fall off map (doesnt work naturally on lvl 2 lol
            //change y pos
            Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y - 200);
            Vector3 newPos = Vector3.MoveTowards(m_body2d.position, targetPosition, m_speed * Time.fixedDeltaTime);
            m_body2d.MovePosition(newPos);
        }
    }

    /*void Shoot()
    {
        Instantiate(slashPrefab, FirePoint.position, FirePoint.rotation);
        test = false;


    }*/

    /*void project()
    {
        Projectile test = GetComponent<Projectile>();
        test.Shoot();
    }*/
}