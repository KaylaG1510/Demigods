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
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    private int health;

    //Attack vars
    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    //Player target
    //public Transform playerTarget;
    public GameObject playerTarget;

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        //playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        attackDelay = 2f;
        damage = 30;
        attackRange = 180;
        //set animator, rigidbody and ground sensor components
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    private void Update()
    {
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
        //get information on whether ground is detected at character edge
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 100f);

        //no ground found/platform end or bounds reached
        if (groundInfo.collider == false)
        {
            changeDirection();
        }
        ////Combat Idle
        //if (m_combatIdle)
        //    m_animator.SetInteger("AnimState", 1);
        ////Idle
        //else
        //    m_animator.SetInteger("AnimState", 0);

        //TEST enemy attack
        if (Input.GetKeyDown("b"))
            m_animator.SetTrigger("Attack");

        //Attack AI

        //check distance between self and player, is player close enough to trigger melee attack?
        //float distToPlayer = Vector3.Distance(transform.position, playerTarget.position);
        float distToPlayer = Vector3.Distance(transform.position, playerTarget.transform.position);

        //Debug.Log(attackRange);
        if(distToPlayer < attackRange)
        {
            Debug.Log("in distance");
            //Check enough time passed since last attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                Debug.Log("Read to attack");
                //play attack animation
                m_animator.SetTrigger("Attack");
                //stop moving while attacking ***might have to do for when Attack Trigger is active??
                m_body2d.velocity = new Vector2(0, 0);

                //send message to player to take damage
                playerTarget.SendMessage("TakeDamage", damage, SendMessageOptions.RequireReceiver);

                //record time attacked ***Time.time;
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

    //reached left or right bounds, change direction
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

    //public void TakeDamage(int damage)
    //{
    //    Debug.Log("enemy taking damage");
    //    m_animator.SetTrigger("Hurt");

    //    health -= damage;

    //    //check enemy
    //    if (health <= 0)
    //    {
    //        Debug.Log("Enemy Dead");
    //        m_animator.SetTrigger("Death");
    //        Destroy(this);
    //    }

    //}
}
