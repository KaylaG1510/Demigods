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
        //Animator an = gameObject.GetComponent<Animator>();
        //an.SetTrigger("Charge");
        //an.SetBool("hold", true);
        nextAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Shoot();
                
                nextAttackTime = Time.time + attackDelay;
            }  
        }
    }

    void Shoot()
    {
        //GameObject test = Instantiate(slashPrefab, FirePoint.position, FirePoint.rotation);
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
            shootRight = false;
            Debug.Log(shootRight + "left");
            //initialise both firepoints, then fire from correct one??
            //FirePoint = GameObject.FindGameObjectWithTag("FirePtR").transform;
            FirePointL.transform.eulerAngles = new Vector3(0, -180, 0);
            Shoot();
            //slashPrefab.GetComponent<Slash>().rb.velocity *= -1;
            //Debug.Log(slashPrefab.GetComponent<Slash>().rb.velocity);
        }
        else
        {
            Debug.Log("ShootRight");
            shootRight = true;
            //FirePoint = GameObject.FindGameObjectWithTag("FirePtL").transform;
            FirePointR.transform.eulerAngles = new Vector3(0, 0, 0);
            Shoot();
        }
        //Debug.Log(slashPrefab.GetComponent<Slash>().rb.velocity);
    }
}


