using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform FirePointL;
    public Transform FirePointR;
    public GameObject slashPrefab;
    private float nextAttackTime;
    private float attackDelay = 2f;
    private bool shootRight;


    void start()
    {
        nextAttackTime = 0f;
    }

    void Shoot()
    {
        GameObject test;
        if(shootRight)
        {
            test = Instantiate(slashPrefab, FirePointR.position, FirePointR.rotation);
        }
        else
        {
            Debug.Log("in else to instantiate");
            test = Instantiate(slashPrefab, FirePointL.position, FirePointL.rotation);
        }
        Destroy(test, 2f);
    }

    void FacingRight(bool facingRight)
    {
        Debug.Log(facingRight);
        if(!facingRight)
        {
            if (Time.time >= nextAttackTime)
            {
                Debug.Log("bailey smells");
                if (Input.GetButtonDown("Fire1"))
                {
                    //Shoot();
                    shootRight = false;
                    Debug.Log(shootRight + " left");
                    FirePointL.transform.eulerAngles = new Vector3(0, -180, 0);
                    Shoot();
                    nextAttackTime = Time.time + attackDelay;
                }
            }
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                Debug.Log("brendan smells");
                if (Input.GetButtonDown("Fire1"))
                {
                    //Shoot();
                    Debug.Log(shootRight + " Right");
                    shootRight = true;
                    FirePointR.transform.eulerAngles = new Vector3(0, 0, 0);
                    Shoot();
                    nextAttackTime = Time.time + attackDelay;
                }
            }
        }
    }
}


