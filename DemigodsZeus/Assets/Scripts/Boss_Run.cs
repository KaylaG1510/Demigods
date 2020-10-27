using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Run : StateMachineBehaviour
{
    public float speed;
    Transform playerTarget;
    Rigidbody2D m_body2d;
    string m_Scene;
    Boss boss;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); ;
        m_body2d = animator.GetComponentInParent<Rigidbody2D>();
        m_Scene = SceneManager.GetActiveScene().name;
        boss = animator.GetComponent<Boss>();

        if (m_Scene.CompareTo("LevelThree") == 0)
        {
            speed = 100f;
        }
        else
        {
            speed = 100f;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //??only move towards player if y value is bottom level and/or within distance??
        //do we want to add platforms to bottom layer??
        //otherwise idle??
        boss.LookAtPlayer();

        //Debug.Log("StateUpdate taking place");
        Vector2 targetPosition = new Vector2(playerTarget.position.x, m_body2d.position.y);
        //Debug.Log(targetPosition.x + ", " + targetPosition.y);
        Vector3 newPos = Vector3.MoveTowards(m_body2d.position, targetPosition, speed * Time.fixedDeltaTime);
        m_body2d.MovePosition(newPos);

        //if (Vector2.Distance(playerTarget.position, m_body2d.position) <= )
        if (newPos.x > m_body2d.position.x)
        {
            m_body2d.SendMessageUpwards("MovingRight");
        }
        else if (newPos.x < m_body2d.position.x)
        {
            m_body2d.SendMessageUpwards("MovingLeft");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
