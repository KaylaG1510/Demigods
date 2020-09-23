using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMovement : MonoBehaviour
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

    private void Start()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBound"))
        {
            changeDirection();
        }
    }

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
}
